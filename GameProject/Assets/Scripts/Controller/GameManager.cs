using UnityEngine;
using YanGameFrameWork.Singleton;
using YanGameFrameWork.Editor;




public class GameManager : Singleton<GameManager>
{


    [SerializeField]
    private GameData gameData;

    private bool isGameOver = false; // 游戏是否结束的标志



    public bool IsGamePause = false;


    [SerializeField]
    private float _fever = 0;
    public float Fever
    {
        get
        {
            return _fever;
        }
        set
        {
            _fever = value;
            UIController.Instance.UpdateFever(_fever, FeverMax);

            if (_fever >= FeverMax * 0.7f){
                YanGF.Event.TriggerEvent(GameEventType.OnFever.ToString());
            }else{
                YanGF.Event.TriggerEvent(GameEventType.OnNotFever.ToString());
            }
        }
    }


    private bool _isFever = false;


 
    
    public float FeverMax = 1000;

    private void Start()
    {
        InitDatas();
        AudioController.PlayBGM();

        IsGamePause = true;
        YanGF.Event.AddListener(GameEventType.OnFever.ToString(), ()=>{
            if(_isFever == true) return;
            OnFever();
            _isFever = true;
        });
        YanGF.Event.AddListener(GameEventType.OnNotFever.ToString(), ()=>{
            if(_isFever == false) return;
            OnNotFever();
            _isFever = false;
        });
    }


    public void StartGame(){
       FunDialogController.Instance.ShowGameStartDialog();
    }




    private void OnFever(){
        Debug.Log("触发fever事件");
    }

    private void OnNotFever(){
        Debug.Log("触发notfever事件");
    }



    public void InitDatas()
    {
        gameData = YanGF.Model.RegisterModule(new GameData(
            maxHP: 3000f,
            targetTime: 1000f,
            hpDecreaseInterval: 1f
            )
        );

        YanGF.Model.RegisterModule(new ScoreManager());

    }

    private void Update()
    {
        gameData.currentTime += Time.deltaTime;
        if (gameData.currentTime >= gameData.targetTime)
        {
            gameData.currentTime = 0;
            Pause();
            GameWin();
        }
        UIController.Instance.UpdateTime(gameData.currentTime, gameData.targetTime);

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

        // Fever值每秒增加10
        Fever = Mathf.Min(_fever + 10 * Time.deltaTime, FeverMax);
    }


    private float LoseHPByTime()
    {
        gameData.hp = Mathf.Max(gameData.hp - 1, gameData.MinHP);
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

        // Fever值减少1
        Fever = Mathf.Max(_fever - 3, 0);

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
        Fever = Mathf.Min(_fever + 2, FeverMax);

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
