using UnityEngine;

/// <summary>
/// 跟随运动命令
/// </summary>
/// <remarks>
/// 敌人跟随玩家移动，通常是围绕一个中心点逐渐向外或向内。
/// </remarks>
public class PlayerFollowMovementCommand : ZMovementCommand
{


    private float speed = 2f;
    private Transform playerTransform; // 玩家位置

    /// <summary>
    /// 构造函数，初始化螺旋速度
    /// </summary>
    /// <param name="spiralSpeed">螺旋速度</param>
    public PlayerFollowMovementCommand(Transform playerTransform,float speed = 10f,float zSpeed = 15f)
    {
        this.speed = speed;
        this.zSpeed = zSpeed;
        this.playerTransform = playerTransform;
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);

        // 计算朝向玩家的方向
        Vector2 directionToPlayer = (playerTransform.position - enemy.transform.position).normalized;


        // 计算新的位置，朝向玩家
        Vector3 newPosition = enemy.transform.position + new Vector3(directionToPlayer.x, directionToPlayer.y, 0) * speed * Time.deltaTime;

        // 更新敌人的位置
        enemy.transform.position = newPosition;
    }
}
