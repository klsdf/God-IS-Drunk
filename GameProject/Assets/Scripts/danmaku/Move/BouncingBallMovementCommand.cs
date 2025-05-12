using UnityEngine;

/// <summary>
/// 弹力球运动命令
/// </summary>
/// <remarks>
/// 敌人像弹力球一样在边界内反弹。
/// </remarks>
public class BouncingBallMovementCommand : ZMovementCommand
{
    private Vector3 direction; // 移动方向
    private float speed; // 移动速度
    private EnemyCreator enemyCreator; // 用于获取边界信息

    /// <summary>
    /// 构造函数，初始化方向和速度
    /// </summary>
    /// <param name="direction">初始移动方向</param>
    /// <param name="speed">移动速度</param>
    /// <param name="enemyCreator">用于获取边界信息的 EnemyCreator 实例</param>
    public BouncingBallMovementCommand(Vector2 direction,EnemyCreator enemyCreator,float speed = 3, float zSpeed = 15)
    {
        this.direction = direction.normalized; // 确保方向是单位向量
        this.speed = speed;
        this.enemyCreator = enemyCreator;
        this.zSpeed = zSpeed;
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        // 计算新的位置
        Vector3 newPosition = enemy.transform.position + direction * speed * Time.deltaTime;

        // 检查边界并反弹
        if (newPosition.x < enemyCreator.minX || newPosition.x > enemyCreator.maxX)
        {
            direction.x = -direction.x; // 反转 X 方向
        }
        if (newPosition.y < enemyCreator.minY || newPosition.y > enemyCreator.maxY)
        {
            direction.y = -direction.y; // 反转 Y 方向
        }
        // 更新敌人的位置
        enemy.transform.position = new Vector3(newPosition.x, newPosition.y, enemy.transform.position.z);
    }
}