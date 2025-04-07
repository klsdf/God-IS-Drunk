
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人预制体
    public float minX = -10f; // 最小 X 坐标（生成范围）
    public float maxX = 10f; // 最大 X 坐标（生成范围）

    
    public float minY = -10f; // 最小 X 坐标（生成范围）
    public float maxY = 10f; // 最大 X 坐标（生成范围）
    public float zPosition = 0f;
    public float spawnInterval = 2f; // 敌人生成间隔时间

    private float nextSpawnTime; // 下一次生成敌人的时间

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // 初始化第一次生成时间
    }

    // Update is called once per frame
    void Update()
    {
        // 如果到了生成敌人的时间
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy(); // 生成敌人
            nextSpawnTime = Time.time + spawnInterval; // 更新下一次生成时间
        }
    }

    // 生成敌人的方法
    void SpawnEnemy()
    {
        // 在指定范围内随机生成 X 坐标
        float randomX = Random.Range(minX, maxX);

        float randomY = Random.Range(minY, maxY);
        // 在 Y 位置生成敌人（根据需要，可以增加高度变化或其他随机性）
        Vector3 spawnPosition = new Vector3(randomX, randomY, zPosition);
        
        // 在指定位置实例化敌人
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
