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
    public override void OnEnter()
    {

    }
    /// <summary>
    /// 检查游戏进程，看看有没有通关，或者进入boss战
    /// </summary>
    public override void OnUpdate()
    {

        GameData gameData = YanGF.Model.GetModel<GameData>();

        gameData.currentTime += Time.deltaTime;
        float progress = gameData.currentTime / gameData.targetTime;

        if (progress >= DataConfig.meetSmallBossProgress)
        {
            if (hasShowSmallBoss == false)
            {
                GameManager.Instance.smallBossEnemy.Show();
                hasShowSmallBoss = true;
            }
        }


        if (progress >= DataConfig.meetBossProgress)
        {
            gameData.currentTime = gameData.targetTime * DataConfig.meetBossProgress; // 将进度卡在99%
            GameManager.Instance.ChangeState(new BossBattleState()); // 进入Boss战模式
        }

        UIController.Instance.UpdateTime(gameData.currentTime, gameData.targetTime);
    }

    public override void OnExit()
    {

    }
}

[System.Serializable]
public class BossBattleState : BaseState
{

    /// <summary>
    /// 进入Boss战模式
    /// </summary>
    public override void OnEnter()
    {

        // 触发进入Boss战的逻辑
        Debug.Log("进入Boss战模式");

        GameData gameData = YanGF.Model.GetModel<GameData>();
        gameData.bossBattleCurrentTime = 0.0f; // 初始化Boss战计时器

        FunDialogController.Instance.ShowBossDialog();
        GameManager.Instance.PauseGame();


    }


    public override void OnUpdate()
    {
        GameData gameData = YanGF.Model.GetModel<GameData>();
        gameData.bossBattleCurrentTime += Time.deltaTime;
        if (gameData.bossBattleCurrentTime >= gameData.bossBattleTargetTime)
        {
            GameManager.Instance.OnBossBattleWin();
            gameData.bossBattleCurrentTime = 0.0f; // 重置计时器
        }
    }

    public override void OnExit()
    {

    }
}





public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameData gameData;

    private bool isGameOver = false; // 游戏是否结束的标志


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
    public BossEnemy smallBossEnemy;

    [Header("Boss")]
    public BossEnemy bossEnemy;


    public Transform playerTransform;

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
