using UnityEngine;
using YanGameFrameWork.CoreCodes;
using YanGameFrameWork.Editor;




public class GameManager : Singleton<GameManager>
{


    [SerializeField]
    private GameData gameData;


    private void Start()
    {
        gameData = YanGF.Model.RegisterModule(new GameData(
            maxHP: 3000f, 
            targetTime: 1000f, 
            hpDecreaseInterval: 1f
            )
        );
        AudioController.PlayBGM();
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
        AudioController.PlayDamageAudio();
        PostEffectController.Instance.SetBloomIntensity(gameData.hp / gameData.MaxHP);
        return gameData.hp;
    }


    public float GainHP(float amount)
    {
        gameData.hp = Mathf.Min(gameData.hp + amount, gameData.MaxHP);
        UIController.Instance.UpdateHP(gameData.hp, gameData.MaxHP);
        PlayerController.Instance.GainHP();
        AudioController.PlayDrinkAudio();
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
        AudioController.PlayWinAudio();
        var panel = YanGF.UI.PushPanel<GameWinPanel>();
        print("游戏胜利");
    }

    [Button("测试游戏失败")]
    public void GameLose()
    {
        AudioController.PlayLoseAudio();
        var panel = YanGF.UI.PushPanel<GameOverPanel>();
        print("游戏失败");
    }




}
