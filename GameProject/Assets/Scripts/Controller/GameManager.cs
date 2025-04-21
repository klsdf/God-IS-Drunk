using UnityEngine;
using YanGameFrameWork.CoreCodes;
using YanGameFrameWork.ModelControlSystem;


[System.Serializable]
public class GameData : YanModelBase
{
    public float hp;

    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Mathf.Clamp(value, MinHP, MaxHP);
        }
    }

    public readonly float MaxHP = 3000f;

    public readonly float MinHP = 0f;
   




    [Header("目标时间，单位是秒")]
    public float targetTime = 0;

    public float currentTime = 0;

    //每隔多少秒减少1点血
    public float hpDecreaseInterval = 1.0f;
    public float hpDecreaseTimer = 0;

    public GameData(float maxHP, float minHP, float targetTime, float hpDecreaseInterval)
    {
        MaxHP = maxHP;
        MinHP = minHP;
        hp = MaxHP;

        this.targetTime = targetTime;
        this.hpDecreaseInterval = hpDecreaseInterval;
    }
      public GameData()
    {
        MaxHP = 1000;
        MinHP = 0;
        hp = MaxHP;

        this.targetTime = 1000;
        this.hpDecreaseInterval = 1;
    }

}

public class GameManager : Singleton<GameManager>
{


    [SerializeField]
    private GameData gameData;


    private void Start()
    {
        gameData = YanGF.Model.RegisterModule(new GameData(3000f, 0f, 10f, 1f));
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
            if (gameData.hp <= 0)
            {
                GameLose();
            }
            gameData.hpDecreaseTimer = 0;
        }
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
        return gameData.hp;
    }


    public float GainHP(float amount)
    {
        gameData.hp = Mathf.Min(gameData.hp + amount, gameData.MaxHP);
        UIController.Instance.UpdateHP(gameData.hp, gameData.MaxHP);
        PlayerController.Instance.GainHP();
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

    public void GameWin()
    {
        var panel = YanGF.UI.PushPanel<GameOverPanel>();
        panel.GetComponent<GameOverPanel>().OnInit("游戏胜利");
        print("游戏胜利");
    }

    public void GameLose()
    {
        var panel = YanGF.UI.PushPanel<GameOverPanel>();
        panel.GetComponent<GameOverPanel>().OnInit("游戏失败");
        print("游戏失败");
    }




}
