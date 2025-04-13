using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 单例实例
    public static GameManager Instance { get; private set; }

    private float hp;
    public float MaxHP { get; private set; } = 3000f;
    public float MinHP { get; private set; } = 0f;


    [Header("目标时间，单位是秒")]
    public float targetTime = 0;

    private float currentTime = 0;

    //每隔多少秒减少1点血
    private float hpDecreaseInterval = 1.0f;
    private float hpDecreaseTimer = 0;

    private void Awake()
    {
        // 检查是否已经有一个实例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经有一个实例，销毁这个新的
        }
    }


    private void Start()
    {
        hp = MaxHP;
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
        UIController.Instance.UpdateTime(currentTime,targetTime);

        // 每秒减少1点血
        hpDecreaseTimer += Time.deltaTime;
        if (hpDecreaseTimer >= hpDecreaseInterval)
        {
            LoseHPByTime();
            if(hp<=0)
            {
                GameLose();
            }
            hpDecreaseTimer = 0;
        }
    }


    private float LoseHPByTime()
    {
        hp = Mathf.Max(hp - 1, MinHP);
        UIController.Instance.UpdateHP(hp,MaxHP);
        return hp;
    }

    public float LoseHP(float amount)
    {
        hp = Mathf.Max(hp - amount, MinHP);
        UIController.Instance.UpdateHP(hp,MaxHP);
        PlayerController.Instance.TakeDamage();
        return hp;
    }

    
    public float GainHP(float amount)
    {
        hp = Mathf.Min(hp + amount, MaxHP);
        UIController.Instance.UpdateHP(hp,MaxHP);
        PlayerController.Instance.GainHP();
        return hp;
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
        print("游戏胜利");
    }

    public void GameLose()
    {
        print("游戏失败");
    }
}
