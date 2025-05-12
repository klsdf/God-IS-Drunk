using UnityEngine;

/// <summary>
/// 螺旋运动命令
/// </summary>
/// <remarks>
/// 敌人沿着螺旋路径移动，通常是围绕一个中心点逐渐向外或向内。
/// </remarks>
public class SpiralMovementCommand : ZMovementCommand
{
    private float radius; // 螺旋半径
    private float spiralSpeed; // 螺旋速度
    private float angle = 0; // 螺旋角度

    /// <summary>
    /// 构造函数，初始化螺旋速度
    /// </summary>
    /// <param name="spiralSpeed">螺旋速度</param>
    public SpiralMovementCommand(float radius, float spiralSpeed, float zSpeed)
    {
        this.radius = radius;
        this.spiralSpeed = spiralSpeed;
        this.zSpeed = zSpeed;
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        // 更新角度
        angle += spiralSpeed * Time.deltaTime;

        // 计算新的位置
        float x = radius * Mathf.Cos(angle) * Mathf.Exp(spiralSpeed * Time.time);
        float y = radius * Mathf.Sin(angle) * Mathf.Exp(spiralSpeed * Time.time);

        // 更新敌人的位置
        enemy.transform.position = new Vector3(x, y, enemy.transform.position.z);
    }
}
