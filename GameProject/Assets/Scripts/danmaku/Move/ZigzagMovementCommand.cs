using UnityEngine;

/// <summary>
/// 锯齿运动命令
/// </summary>
/// <remarks>
/// 敌人沿着锯齿形路径移动，通常在一个轴上来回摆动。
/// </remarks>
public class ZigzagMovementCommand : ZMovementCommand   
{
    private float frequency; // 摆动频率
    private float amplitude; // 摆动幅度

    /// <summary>
    /// 构造函数，初始化频率和幅度
    /// </summary>
    /// <param name="frequency">摆动频率</param>
    /// <param name="amplitude">摆动幅度</param>
    public ZigzagMovementCommand(float frequency = 2f, float amplitude = 2f,float zSpeed = 1.5f)
    {
        this.frequency = Mathf.Clamp(frequency, 0.1f, 10f);
        this.amplitude = Mathf.Clamp(amplitude, 0.1f, 10f);
        this.zSpeed = Mathf.Clamp(zSpeed, 0.1f, 10f);
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        // 计算新的 X 位置
        float x = Mathf.Sin(Time.time * frequency) * amplitude;
        // 更新敌人的位置
        enemy.transform.position = new Vector3(x, enemy.transform.position.y, enemy.transform.position.z);
    }
}