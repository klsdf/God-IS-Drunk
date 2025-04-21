using UnityEngine;
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

    public readonly float MaxHP;

    public readonly float MinHP;
   




    [Header("目标时间，单位是秒")]
    public float targetTime;

    public float currentTime;

    //每隔多少秒减少1点血
    public float hpDecreaseInterval = 1.0f;
    public float hpDecreaseTimer = 0;

    public GameData(float maxHP, float targetTime, float hpDecreaseInterval)
    {
        MaxHP = maxHP;
        MinHP = 0;
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