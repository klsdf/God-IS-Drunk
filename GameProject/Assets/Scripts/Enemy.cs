using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minShrinkSpeed = 0.5f; // 最小的缩小速度
    public float maxShrinkSpeed = 2f;   // 最大的缩小速度
    private float shrinkSpeed = 1f; // 每秒减少的 z 轴速度
    private float currentZ; // 当前的 Z 坐标

    // Start is called before the first frame update
    void Start()
    {
        // 在给定的区间内生成一个随机速度
        shrinkSpeed = Random.Range(minShrinkSpeed, maxShrinkSpeed);
        currentZ = transform.position.z; // 获取当前的 Z 坐标
    }

    // Update is called once per frame
    void Update()
    {
        // 更新 Z 轴的值
        currentZ -= shrinkSpeed * Time.deltaTime;

        // 更新敌人的位置
        transform.position = new Vector3(transform.position.x, transform.position.y, currentZ);

        // 如果 Z 坐标小于 -10，就销毁该敌人
        if (currentZ < -10f)
        {
            Destroy(gameObject); // 销毁敌人对象
        }
    }
}
