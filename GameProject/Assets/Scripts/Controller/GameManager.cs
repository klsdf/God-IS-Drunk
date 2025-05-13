using UnityEngine;
using YanGameFrameWork.Singleton;
using YanGameFrameWork.Editor;



[System.Serializable]
public abstract class BaseState
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

[System.Serializable]
public class NormalState : BaseState
{

    private bool hasShowSmallBoss = false;

    private bool hasShowBoss = false;

    [Header("敌人生成间隔时间")]
    public float spawnInterval = 2f; // 敌人生成间隔时间
    private float timer; // 下一次生成敌人的时间

    public override void OnEnter()
    {
        timer = spawnInterval;
    }

    private void PrepareSpanNoramlItems()
    {
        if (timer >= spawnInterval)
        {
            EnemyCreator.Instance.SpanNoramlItems(GameManager.Instance.障碍物们);
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    /// <summary>
    /// 检查游戏进程，看看有没有通关，或者进入boss战
    /// </summary>
    public override void OnUpdate()
    {

        GameData gameData = YanGF.Model.GetModel<GameData>();

        gameData.currentTime += Time.deltaTime;
        float progress = gameData.currentTime / gameData.targetTime;


        if (progress <= DataConfig.meetSmallBossProgress)
        {
            PrepareSpanNoramlItems();
        }



        if (progress > DataConfig.meetSmallBossProgress && progress < DataConfig.meetSmallBossProgress + DataConfig.smallBossProgress)
        {
            if (hasShowSmallBoss == false)
            {
                GameManager.Instance.smallBossEnemy.gameObject.SetActive(true);
                GameManager.Instance.smallBossEnemy.Show();
                hasShowSmallBoss = true;
            }
        }


        if (progress >= DataConfig.meetSmallBossProgress + DataConfig.smallBossProgress && progress < DataConfig.meetBossProgress)
        {
            PrepareSpanNoramlItems();
        }



        if (progress >= DataConfig.meetBossProgress && GameManager.Instance.isGameOver == false)
        {
            gameData.currentTime = gameData.targetTime * DataConfig.meetBossProgress; // 将进度卡在99%
            if (hasShowBoss == false)
            {


                GameManager.Instance.bossEnemy.gameObject.SetActive(true);
                GameManager.Instance.bossEnemy.Show();
                hasShowBoss = true;
            }
        }

        UIController.Instance.UpdateTime(gameData.currentTime, gameData.targetTime);
    }

    public override void OnExit()
    {

    }
}






public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameData gameData;

    public bool isGameOver = false; // 游戏是否结束的标志


    [SerializeField]
    private bool isGamePause = false;

    public bool IsGamePause
    {
        get { return isGamePause; }
    }

    [SerializeField]
    public BaseState currentState;



    public string debugCurrentState;

    [Header("小Boss")]
    public SmallBossEnemy smallBossEnemy;

    [Header("Boss")]
    public BossEnemy bossEnemy;


    public Transform playerTransform;


    public Sprite[] 障碍物们;

    public Sprite[] 酒们;

    private void Start()
    {
        InitDatas();
        AudioController.PlayBGM();

        PauseGame();
        ChangeState(new NormalState());

        playerTransform = FindObjectOfType<PlayerController>().transform;

    }

    public void ChangeState(BaseState state)
    {
        debugCurrentState = state.GetType().Name;
        currentState?.OnExit();
        currentState = state;
        currentState.OnEnter();
    }


    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        FunDialogController.Instance.ShowGameStartDialog();
    }



    public void InitDatas()
    {
        gameData = YanGF.Model.RegisterModule(new GameData(
            maxHP: DataConfig.maxHP,
            targetTime: DataConfig.targetTime,
            hpDecreaseInterval: DataConfig.hpDecreaseInterval,
            bossBattleTargetTime: DataConfig.bossBattleTargetTime
            )
        );

        YanGF.Model.RegisterModule(new ScoreManager());

    }



    private void Update()
    {

        if (isGamePause) return;

        if (currentState != null)
        {
            currentState.OnUpdate();
        }

        // 每秒减少1点血
        gameData.hpDecreaseTimer += Time.deltaTime;
        if (gameData.hpDecreaseTimer >= gameData.hpDecreaseInterval)
        {
            LoseHPByTime();

            // 每秒增加1分
            YanGF.Model.GetModel<ScoreManager>().AddScore(1);
            if (gameData.hp <= 0)
            {
                GameLose();
            }
            gameData.hpDecreaseTimer = 0;
        }
    }









    /// <summary>
    /// Boss战胜利后调用此方法
    /// </summary>
    public void OnBossBattleWin()
    {
        gameData.currentTime = gameData.targetTime; // 将进度设置为100%
        GameWin(); // 调用游戏胜利逻辑
        PauseGame();
    }


    private float LoseHPByTime()
    {
        gameData.hp = Mathf.Max(gameData.hp - DataConfig.loseHPByTime, gameData.MinHP);
        UIController.Instance.UpdateHP(gameData.hp, gameData.MaxHP);
        return gameData.hp;
    }

    public float LoseHP(float amount)
    {
        gameData.hp = Mathf.Max(gameData.hp - amount, gameData.MinHP);
        UIController.Instance.UpdateHP(gameData.hp, gameData.MaxHP);
        PlayerController.Instance.TakeDamage();
        AudioController.PlayDamageAudio();
        PostEffectController.Instance.SetBloomIntensity(gameData.hp / gameData.MaxHP);

        // 减少分数
        YanGF.Model.GetModel<ScoreManager>().LoseScore((int)amount);
        FeverController.Instance.LoseFever(DataConfig.loseFever);

        return gameData.hp;
    }


    public float GainHP(float amount)
    {
        gameData.hp = Mathf.Min(gameData.hp + amount, gameData.MaxHP);
        UIController.Instance.UpdateHP(gameData.hp, gameData.MaxHP);
        PlayerController.Instance.GainHP();
        AudioController.PlayDrinkAudio();

        // 增加分数
        YanGF.Model.GetModel<ScoreManager>().AddScore((int)amount);

        // Fever值增加2
        FeverController.Instance.GainFever(DataConfig.gainFever);

        return gameData.hp;
    }



    public void PauseGame()
    {
        isGamePause = true;
    }

    public void ResumeGame()
    {
        isGamePause = false;
    }



    [Button("测试游戏胜利")]
    public void GameWin()
    {
        if (isGameOver) return; // 如果游戏已经结束，直接返回

        isGameOver = true; // 设置游戏结束标志
        AudioController.PlayWinAudio();
        var panel = YanGF.UI.PushPanel<GameWinPanel>();
        print("游戏胜利");
    }

    [Button("测试游戏失败")]
    public void GameLose()
    {
        if (isGameOver) return; // 如果游戏已经结束，直接返回

        isGameOver = true; // 设置游戏结束标志
        AudioController.PlayLoseAudio();
        var panel = YanGF.UI.PushPanel<GameOverPanel>();
        print("游戏失败");
    }





}
