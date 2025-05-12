using UnityEngine;

/// <summary>
///敌人保持在 X 和 Y 轴上的位置不变，仅在 Z 轴上移动。
/// </summary>
public class ZMovementCommand : IMovementCommand
{

    public float zSpeed;

    public ZMovementCommand(float zSpeed = 15f)
    {
        this.zSpeed = zSpeed;
    }

    public virtual void Execute(Enemy enemy)
    {
        float newZ = enemy.transform.position.z -zSpeed * Time.deltaTime;
        // 仅更新 Z 轴的值，保持 X 和 Y 不变
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, newZ);
    }
}