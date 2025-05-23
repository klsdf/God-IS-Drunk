public static class DataConfig{
    /// <summary>
    /// 最大血量
    /// </summary>
    public static float maxHP = 1000;

    /// <summary>
    /// 目标时间
    /// </summary>
    public static float targetTime = 300;

    /// <summary>
    /// 每隔多少秒减少一次血量
    /// </summary>
    public static float hpDecreaseInterval = 1;


    /// <summary>
    /// 每次减少的血量
    /// </summary>
    public static float loseHPByTime = 1;

    /// <summary>
    /// 遇到Boss时的进度
    /// </summary>
    public static float meetBossProgress = 0.99f;


    /// <summary>
    /// 遇到小Boss时的进度
    /// </summary>
    public static float meetSmallBossProgress = 0.50f;

    /// <summary>
    /// 小boss的时长，也就是占总时长的百分之多少
    /// </summary>
    public static float smallBossProgress = 0.10f;



    /// <summary>
    /// Boss战时长
    /// </summary>
    public static float bossBattleTargetTime = 100;


    /// <summary>
    /// 碰撞障碍物时损失的Fever
    /// </summary>
    public static float loseFever = 3;

    /// <summary>
    /// 碰撞酒时增加的Fever
    /// </summary>
    public static float gainFever = 4;


    /// <summary>
    /// 碰撞障碍物时损失的血量
    /// </summary>
    public static float loseHP = 3;

    /// <summary>
    /// 碰撞酒时增加的血量
    /// </summary>
    public static float gainHP = 4;



    /// <summary>
    /// 射线检测的最大距离
    /// </summary>
    public static float raycastDistance = 30f;

}


