using UnityEngine;
using YanGameFrameWork.ModelControlSystem;


[System.Serializable]

/// <summary>
/// 游戏的数据
/// </summary>
public class GameData : YanModelBase
{
    [SerializeField]
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

    [SerializeField]
    public  float MaxHP;

    [SerializeField]
    public  float MinHP;
   




    [Header("目标时间，单位是秒")]
    public float targetTime;

    public float currentTime;

    //每隔多少秒减少1点血
    public float hpDecreaseInterval = 1.0f;
    public float hpDecreaseTimer = 0;


    [Header("Boss战时长")]
    public float bossBattleTargetTime = 30;

    [Header("Boss战当前时间")]
    public float bossBattleCurrentTime = 0;

    public GameData(float maxHP, float targetTime, float hpDecreaseInterval,float bossBattleTargetTime)
    {
        MaxHP = maxHP;
        MinHP = 0;
        hp = MaxHP;
        this.targetTime = targetTime;
        this.hpDecreaseInterval = hpDecreaseInterval;
        this.bossBattleTargetTime = bossBattleTargetTime;
        this.bossBattleCurrentTime = 0;
    }
      
    public GameData()
    {
        MaxHP = 1000;
        MinHP = 0;
        hp = MaxHP;

        this.targetTime = 500;
        this.hpDecreaseInterval = 1;
    }

    public override YanModelBase Clone(YanModelBase model)
    {
        GameData newModel = model as GameData;
        newModel.hp = hp;
        newModel.MaxHP = MaxHP;
        newModel.MinHP = MinHP;
        newModel.targetTime = targetTime;
        newModel.hpDecreaseInterval = hpDecreaseInterval;
        newModel.hpDecreaseTimer = hpDecreaseTimer;
        newModel.currentTime = currentTime;
        return newModel;
    }
}