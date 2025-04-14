using UnityEngine;
using YanGameFrameWork.CoreCodes;

[System.Serializable]
public struct GameData
{
    public float hp;
}

public class GameManager : Singleton<GameManager>
{


    [SerializeField]
    private GameData gameData;


    public float MaxHP { get; private set; } = 3000f;
    public float MinHP { get; private set; } = 0f;


    [Header("目标时间，单位是秒")]
    public float targetTime = 0;

    private float currentTime = 0;

    //每隔多少秒减少1点血
    private float hpDecreaseInterval = 1.0f;
    private float hpDecreaseTimer = 0;



    private void Start()
    {
        gameData.hp = MaxHP;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= targetTime)
        {
            currentTime = 0;
            Pause();
            GameWin();
        }
        UIController.Instance.UpdateTime(currentTime, targetTime);

        // 每秒减少1点血
        hpDecreaseTimer += Time.deltaTime;
        if (hpDecreaseTimer >= hpDecreaseInterval)
        {
            LoseHPByTime();
            if (gameData.hp <= 0)
            {
                GameLose();
            }
            hpDecreaseTimer = 0;
        }
    }


    private float LoseHPByTime()
    {
        gameData.hp = Mathf.Max(gameData.hp - 1, MinHP);
        UIController.Instance.UpdateHP(gameData.hp, MaxHP);
        return gameData.hp;
    }

    public float LoseHP(float amount)
    {
        gameData.hp = Mathf.Max(gameData.hp - amount, MinHP);
        UIController.Instance.UpdateHP(gameData.hp, MaxHP);
        PlayerController.Instance.TakeDamage();
        return gameData.hp;
    }


    public float GainHP(float amount)
    {
        gameData.hp = Mathf.Min(gameData.hp + amount, MaxHP);
        UIController.Instance.UpdateHP(gameData.hp, MaxHP);
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
