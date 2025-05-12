using UnityEngine;

/// <summary>
/// 直线运动命令
/// </summary>
/// <remarks>
/// 敌人沿着一个固定方向以恒定速度移动。
/// </remarks>
public class LinearMovementCommand : ZMovementCommand
{
    private Vector3 direction; // 移动方向
    private float speed; // 移动速度
    /// <summary>
    /// 构造函数，初始化移动方向
    /// </summary>
    /// <param name="direction">移动方向向量</param>
    public LinearMovementCommand(Vector3 direction, float speed,float zSpeed = 10f)
    {
        this.direction = direction.normalized; // 确保方向是单位向量
        this.speed = speed;
        this.zSpeed = zSpeed;
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        // 沿着指定方向移动
        enemy.transform.position += direction * speed * Time.deltaTime;
    }
}
