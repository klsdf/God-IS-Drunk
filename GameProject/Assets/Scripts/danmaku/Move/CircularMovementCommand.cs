using UnityEngine;

/// <summary>
/// 圆周运动命令
/// </summary>
/// <remarks>
/// 敌人围绕一个固定点做圆周运动，使用三角函数计算位置。
/// </remarks>
public class CircularMovementCommand : ZMovementCommand
{
    private float radius;
    private float speed;
    private float angle = 0;

    public CircularMovementCommand(float radius = 6, float speed = 2,float zSpeed = 10f)
    {
        this.radius = radius;
        this.speed = speed;
        this.zSpeed = zSpeed;
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        // 更新角度
        angle += speed * Time.deltaTime;

        // 计算新的位置
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        // 更新敌人的位置
        enemy.transform.position = new Vector3(x, y, enemy.transform.position.z);
    }
}