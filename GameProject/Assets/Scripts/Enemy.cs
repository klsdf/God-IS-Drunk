using UnityEngine;

/// <summary>
/// 敌人，或者说碰到后会掉血的障碍物
/// </summary>
public class Enemy : MonoBehaviour
{

    public float currentZ; // 当前的 Z 坐标

    // 新增变量
    public float angle; // 当前的角度
    public float radius = 5f; // 圆周运动的半径
    public float rotationSpeed = 1f; // 旋转速度

    private IMovementCommand movementCommand; // 移动命令

    void Start()
    {
        // 在给定的区间内生成一个随机速度

        currentZ = transform.position.z; // 获取当前的 Z 坐标

        // 初始化角度
        angle = 0f;


        movementCommand = new NoMovementCommand();

        // 初始化弹力球运动命令
        // Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        // movementCommand = new BouncingBallMovementCommand(randomDirection, 1f, EnemyCreator.Instance);

        // 5秒后销毁敌人
        Destroy(gameObject, 40f);
    }

    void Update()
    {
        // 执行移动命令
        movementCommand.Execute(this);

        // 更新 Z 轴的值
        currentZ -= shrinkSpeed * Time.deltaTime;
    }
}
