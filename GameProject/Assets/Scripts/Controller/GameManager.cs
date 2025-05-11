using UnityEngine;
using YanGameFrameWork.Singleton;
using YanGameFrameWork.Editor;




public class GameManager : Singleton<GameManager>
{


    [SerializeField]
    private GameData gameData;

    private bool isGameOver = false; // 游戏是否结束的标志

    public bool IsGamePause = false;

    private bool isBossBattle = false;




 

    private void Start()
    {
        InitDatas();
        AudioController.PlayBGM();

        IsGamePause = true;
   
    }


    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame(){
       FunDialogController.Instance.ShowGameStartDialog();
    }



    public void InitDatas()
    {
        gameData = YanGF.Model.RegisterModule(new GameData(
            maxHP: DataConfig.maxHP,
            targetTime: DataConfig.targetTime,
            hpDecreaseInterval: DataConfig.hpDecreaseInterval
            )
        );

        YanGF.Model.RegisterModule(new ScoreManager());

    }

    private void Update()
    {
        GameProcessCheck();

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
    /// 检查游戏进程，看看有没有通关，或者进入boss战
    /// </summary>
    private void GameProcessCheck(){
        if (isBossBattle) return; // 如果已经进入Boss战，停止进度更新

        gameData.currentTime += Time.deltaTime;
        float progress = gameData.currentTime / gameData.targetTime;

        if (progress >= DataConfig.meetBossProgress) {
            gameData.currentTime = gameData.targetTime * DataConfig.meetBossProgress; // 将进度卡在99%
            EnterBossBattle(); // 进入Boss战模式
            isBossBattle = true;
        }

        UIController.Instance.UpdateTime(gameData.currentTime, gameData.targetTime);
    }

    /// <summary>
    /// 进入Boss战模式
    /// </summary>
    private void EnterBossBattle() {
        // 触发进入Boss战的逻辑
        Debug.Log("进入Boss战模式");
        // 这里可以添加进入Boss战的具体实现
    }

    /// <summary>
    /// Boss战胜利后调用此方法
    /// </summary>
    public void OnBossBattleWin() {
        gameData.currentTime = gameData.targetTime; // 将进度设置为100%
        isBossBattle = false;
        GameWin(); // 调用游戏胜利逻辑
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



    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
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
