using UnityEngine;

/// <summary>
/// 敌人，或者说碰到后会掉血的障碍物
/// </summary>
public class Enemy : MonoBehaviour
{
    public float minShrinkSpeed = 0.5f; // 最小的缩小速度
    public float maxShrinkSpeed = 2f;   // 最大的缩小速度
    private float shrinkSpeed = 1f; // 每秒减少的 z 轴速度
    private float currentZ; // 当前的 Z 坐标


    void Start()
    {
        // 在给定的区间内生成一个随机速度
        shrinkSpeed = Random.Range(minShrinkSpeed, maxShrinkSpeed);
        currentZ = transform.position.z; // 获取当前的 Z 坐标

        // 5秒后销毁敌人
        Destroy(gameObject, 5f);
    }


    void Update()
    {
        // 更新 Z 轴的值
        currentZ += shrinkSpeed * Time.deltaTime;

        // 更新敌人的位置
        transform.position = new Vector3(transform.position.x, transform.position.y, currentZ);

    }
}
