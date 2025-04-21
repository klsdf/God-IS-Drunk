using UnityEngine;

/// <summary>
/// 移动命令接口
/// </summary>
public interface IMovementCommand
{
    /// <summary>
    /// 执行移动命令
    /// </summary>
    /// <param name="enemy">要移动的敌人对象</param>
    void Execute(Enemy enemy);
}

/// <summary>
/// 圆周运动命令
/// </summary>
/// <remarks>
/// 敌人围绕一个固定点做圆周运动，使用三角函数计算位置。
/// </remarks>
public class CircularMovementCommand : IMovementCommand
{
    public void Execute(Enemy enemy)
    {
        // 更新角度
        enemy.angle += enemy.rotationSpeed * Time.deltaTime;

        // 计算新的位置
        float x = enemy.radius * Mathf.Cos(enemy.angle);
        float y = enemy.radius * Mathf.Sin(enemy.angle);

        // 更新敌人的位置
        enemy.transform.position = new Vector3(x, y, enemy.currentZ);
    }
}

/// <summary>
/// 静止不动命令
/// </summary>
/// <remarks>
/// 敌人保持在 X 和 Y 轴上的位置不变，仅在 Z 轴上移动。
/// </remarks>
public class NoMovementCommand : IMovementCommand
{
    public void Execute(Enemy enemy)
    {
        // 仅更新 Z 轴的值，保持 X 和 Y 不变
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.currentZ);
    }
}

/// <summary>
/// 直线运动命令
/// </summary>
/// <remarks>
/// 敌人沿着一个固定方向以恒定速度移动。
/// </remarks>
public class LinearMovementCommand : IMovementCommand
{
    private Vector3 direction; // 移动方向

    /// <summary>
    /// 构造函数，初始化移动方向
    /// </summary>
    /// <param name="direction">移动方向向量</param>
    public LinearMovementCommand(Vector3 direction)
    {
        this.direction = direction.normalized; // 确保方向是单位向量
    }

    public void Execute(Enemy enemy)
    {
        // 沿着指定方向移动
        enemy.transform.position += direction * enemy.shrinkSpeed * Time.deltaTime;
    }
}

/// <summary>
/// 锯齿运动命令
/// </summary>
/// <remarks>
/// 敌人沿着锯齿形路径移动，通常在一个轴上来回摆动。
/// </remarks>
public class ZigzagMovementCommand : IMovementCommand
{
    private float frequency; // 摆动频率
    private float amplitude; // 摆动幅度

    /// <summary>
    /// 构造函数，初始化频率和幅度
    /// </summary>
    /// <param name="frequency">摆动频率</param>
    /// <param name="amplitude">摆动幅度</param>
    public ZigzagMovementCommand(float frequency, float amplitude)
    {
        this.frequency = frequency;
        this.amplitude = amplitude;
    }

    public void Execute(Enemy enemy)
    {
        // 计算新的 X 位置
        float x = Mathf.Sin(Time.time * frequency) * amplitude;
        // 更新敌人的位置
        enemy.transform.position = new Vector3(x, enemy.transform.position.y, enemy.currentZ);
    }
}

/// <summary>
/// 螺旋运动命令
/// </summary>
/// <remarks>
/// 敌人沿着螺旋路径移动，通常是围绕一个中心点逐渐向外或向内。
/// </remarks>
public class SpiralMovementCommand : IMovementCommand
{
    private float spiralSpeed; // 螺旋速度

    /// <summary>
    /// 构造函数，初始化螺旋速度
    /// </summary>
    /// <param name="spiralSpeed">螺旋速度</param>
    public SpiralMovementCommand(float spiralSpeed)
    {
        this.spiralSpeed = spiralSpeed;
    }

    public void Execute(Enemy enemy)
    {
        // 更新角度
        enemy.angle += enemy.rotationSpeed * Time.deltaTime;

        // 计算新的位置
        float x = enemy.radius * Mathf.Cos(enemy.angle) * Mathf.Exp(spiralSpeed * Time.time);
        float y = enemy.radius * Mathf.Sin(enemy.angle) * Mathf.Exp(spiralSpeed * Time.time);

        // 更新敌人的位置
        enemy.transform.position = new Vector3(x, y, enemy.currentZ);
    }
}

/// <summary>
/// 弹力球运动命令
/// </summary>
/// <remarks>
/// 敌人像弹力球一样在边界内反弹。
/// </remarks>
public class BouncingBallMovementCommand : IMovementCommand
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
    public BouncingBallMovementCommand(Vector3 direction, float speed, EnemyCreator enemyCreator)
    {
        this.direction = direction.normalized; // 确保方向是单位向量
        this.speed = speed;
        this.enemyCreator = enemyCreator;
    }

    public void Execute(Enemy enemy)
    {
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
        enemy.transform.position = new Vector3(newPosition.x, newPosition.y, enemy.currentZ);
    }
}