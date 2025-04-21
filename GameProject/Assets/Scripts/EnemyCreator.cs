using UnityEngine;
using YanGameFrameWork.CoreCodes;

public class EnemyCreator : Singleton<EnemyCreator>
{
    [Header("敌人预制体")]
    public GameObject enemyPrefab; // 敌人预制体
    [Header("酒预制体")]
    public GameObject winePrefab; // 酒预制体

    [Header("最小的X坐标")]
    public float minX = -10f; // 最小 X 坐标（生成范围）

    [Header("最大的X坐标")]
    public float maxX = 10f; // 最大 X 坐标（生成范围）

    [Header("最小的Y坐标")]
    public float minY = -10f; // 最小 X 坐标（生成范围）

    [Header("最大的Y坐标")]
    public float maxY = 10f; // 最大 X 坐标（生成范围）

    [Header("最远的Z坐标")]
    public float maxZ = 10f; // 最大 Z 坐标（生成范围）

    private float zPosition = 0f;

    [Header("敌人生成间隔时间")]
    public float spawnInterval = 2f; // 敌人生成间隔时间

    private float nextSpawnTime; // 下一次生成敌人的时间


    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // 初始化第一次生成时间
        zPosition = transform.position.z;
    }


    void Update()
    {
        // 如果到了生成敌人的时间
        if (Time.time >= nextSpawnTime)
        {
            SpawnSomething();
            nextSpawnTime = Time.time + spawnInterval; // 更新下一次生成时间
        }
    }




      /// <summary>
    /// 随机生成酒或敌人
    /// </summary>
    void SpawnSomething()
    {
        // 生成一个0或1的随机数
        int randomChoice = Random.Range(0, 2);

        // 根据随机数选择生成对象
        if (randomChoice == 0)
        {
            // 生成酒
           SpawnWine();
        }
        else
        {
            // 生成敌人
            // SpawnEnemy();
            SpawnEnemiesInCircle(5f, 10);
        }
    }


    /// <summary>
    /// 生成敌人
    /// </summary>
    void SpawnEnemy()
    {
        // 在指定范围内随机生成 X 坐标
        float randomX = Random.Range(minX, maxX);

        float randomY = Random.Range(minY, maxY);
        // 在 Y 位置生成敌人（根据需要，可以增加高度变化或其他随机性）
        Vector3 spawnPosition = new Vector3(randomX, randomY, zPosition);
        
        // 在指定位置实例化敌人，并将其设置为当前对象的子节点
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
    }
    

    /// <summary>
    /// 生成酒
    /// </summary>
    void SpawnWine()
    {
        // 在指定范围内随机生成 X 坐标
        float randomX = Random.Range(minX, maxX);

        float randomY = Random.Range(minY, maxY);
        // 在 Y 位置生成敌人（根据需要，可以增加高度变化或其他随机性）
        Vector3 spawnPosition = new Vector3(randomX, randomY, zPosition);
        
        // 在指定位置实例化敌人，并将其设置为当前对象的子节点
        Instantiate(winePrefab, spawnPosition, Quaternion.identity, transform);
    }


    void OnDrawGizmos()
    {
        // 设置 Gizmos 的颜色
        Gizmos.color = Color.green;

        // 计算中心点和大小
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (zPosition + maxZ) / 2);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, maxZ - zPosition);

        // 绘制一个线框立方体来表示生成范围
        Gizmos.DrawWireCube(center, size);
    }

    /// <summary>
    /// 生成围绕 (0, 0) 点的敌人
    /// </summary>
    /// <param name="radius">圆周的半径</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    public void SpawnEnemiesInCircle(float radius, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 计算每个敌人的角度
            float angle = i * Mathf.PI * 2 / enemyCount;

            // 计算敌人的位置
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // 使用当前对象的 Z 坐标
            Vector3 spawnPosition = new Vector3(x, y, transform.position.z);

            // 在指定位置实例化敌人，并将其设置为当前对象的子节点
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}
