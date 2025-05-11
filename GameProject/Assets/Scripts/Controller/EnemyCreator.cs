using UnityEngine;
using YanGameFrameWork.Singleton;
using System.Collections; // 引入命名空间

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


    [Header("敌人生成概率")]
    public float enemySpawnRate = 0.8f;

    [Header("调试模式")]
    public bool debugMode = false; // 是否启用调试模式

    [Header("调试生成方式")]
    public int debugSpawnMethod = 0; // 调试时选择的生成方式



    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // 初始化第一次生成时间
        zPosition = transform.position.z;
    }


    


    void Update()
    {
        if (GameManager.Instance.IsGamePause)
        {
            return;
        }
        // 如果到了生成敌人的时间
        if (Time.time >= nextSpawnTime)
        {
            if (debugMode)
            {
                // 调试模式下，使用指定的生成方式
                TestSpawnMethod(debugSpawnMethod);
            }
            else
            {
                // 正常模式下，随机生成
                SpawnSomething();
            }
            nextSpawnTime = Time.time + spawnInterval; // 更新下一次生成时间
        }
    }




      /// <summary>
    /// 随机生成酒或敌人
    /// </summary>
    void SpawnSomething()
    {
        // 生成一个0到1之间的随机浮点数
        float randomValue = Random.Range(0f, 1f);

        // 如果随机值小于0.8，则生成敌人
        if (randomValue < enemySpawnRate)
        {
            // 随机选择一种敌人生成方式
            int enemySpawnMethod = Random.Range(0, 5); // 更新上限为5

            switch (enemySpawnMethod)
            {
                case 0:
                    // 生成单个敌人
                    SpawnEnemy();
                    break;
                case 1:
                    // 生成螺旋形敌人
                    SpawnEnemiesInSpiral(4, 15, 2f, 1.5f);
                    break;
                case 2:
                    // 生成圆形敌人
                    SpawnEnemiesInCircle(5f, 20);
                    break;
                case 3:
                    // 生成波纹形敌人
                    SpawnEnemiesInWave(5, 10, 2f, 1f);
                    break;
                case 4:
                    // 生成三角形敌人
                    SpawnEnemiesInTriangle(5f, 10);
                    break;
                case 5:
                    // 生成正方形敌人
                    SpawnEnemiesInSquare(5f, 8);
                    break;
                case 6:
                    // 生成圆形敌人
                    SpawnEnemiesInCircleIndividually(5f, 20, 0.5f);
                    break;
            }
        }
        else
        {
            // 否则生成酒
            SpawnWine();
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
        CreateEnemy(spawnPosition, new ZMovementCommand(20f));
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
            CreateEnemy(spawnPosition, new ZMovementCommand(20f));
        }
    }

    /// <summary>
    /// 生成螺旋形状的敌人
    /// </summary>
    /// <param name="spiralTurns">螺旋的圈数</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    /// <param name="radiusIncrement">每圈半径的增量</param>
    /// <param name="spawnDelay">每个敌人生成的延迟时间</param>
    public void SpawnEnemiesInSpiral(int spiralTurns, int enemyCount, float radiusIncrement, float spawnDelay)
    {
        StartCoroutine(SpawnEnemiesInSpiralCoroutine(spiralTurns, enemyCount, radiusIncrement, spawnDelay));
    }

    /// <summary>
    /// 协程：逐个生成螺旋形状的敌人
    /// </summary>
    private IEnumerator SpawnEnemiesInSpiralCoroutine(int spiralTurns, int enemyCount, float radiusIncrement, float spawnDelay)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 计算每个敌人的角度
            float angle = i * Mathf.PI * 2 * spiralTurns / enemyCount;

            // 计算当前敌人的半径
            float radius = radiusIncrement * angle / (Mathf.PI * 2);

            // 计算敌人的位置
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // 使用当前对象的 Z 坐标
            Vector3 spawnPosition = new Vector3(x, y, transform.position.z);

            // 在指定位置实例化敌人，并将其设置为当前对象的子节点
            CreateEnemy(spawnPosition, new ZMovementCommand(20f));

            // 等待一段时间后再生成下一个敌人
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    /// <summary>
    /// 生成圆形波纹形状的敌人
    /// </summary>
    /// <param name="waveCount">波纹的数量</param>
    /// <param name="enemiesPerWave">每个波纹的敌人数量</param>
    /// <param name="radiusIncrement">每个波纹的半径增量</param>
    /// <param name="spawnDelay">每个波纹生成的延迟时间</param>
    public void SpawnEnemiesInWave(int waveCount, int enemiesPerWave, float radiusIncrement, float spawnDelay)
    {
        StartCoroutine(SpawnEnemiesInWaveCoroutine(waveCount, enemiesPerWave, radiusIncrement, spawnDelay));
    }

    /// <summary>
    /// 协程：逐个生成圆形波纹形状的敌人
    /// </summary>
    private IEnumerator SpawnEnemiesInWaveCoroutine(int waveCount, int enemiesPerWave, float radiusIncrement, float spawnDelay)
    {
        for (int wave = 0; wave < waveCount; wave++)
        {
            float radius = wave * radiusIncrement;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                float angle = i * Mathf.PI * 2 / enemiesPerWave;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                Vector3 spawnPosition = new Vector3(x, y, transform.position.z);
                CreateEnemy(spawnPosition, new ZMovementCommand(20f));
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    /// <summary>
    /// 生成等边三角形形状的敌人
    /// </summary>
    /// <param name="sideLength">三角形的边长</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    public void SpawnEnemiesInTriangle(float sideLength, int enemyCount)
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(2 * enemyCount)); // 计算需要的行数

        for (int row = 0; row < rows; row++)
        {
            int enemiesInRow = row + 1; // 每行的敌人数量逐渐增加
            float yOffset = row * (sideLength / rows); // 计算每行的 Y 偏移

            for (int i = 0; i < enemiesInRow; i++)
            {
                float xOffset = (i - row / 2f) * (sideLength / rows); // 计算每个敌人的 X 偏移

                // 使用当前对象的 Z 坐标
                Vector3 spawnPosition = new Vector3(xOffset, yOffset, transform.position.z);

                // 在指定位置实例化敌人，并将其设置为当前对象的子节点
                CreateEnemy(spawnPosition, new ZMovementCommand(20f));
            }
        }
    }

    /// <summary>
    /// 生成正方形形状的敌人
    /// </summary>
    /// <param name="sideLength">正方形的边长</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    public void SpawnEnemiesInSquare(float sideLength, int enemyCount)
    {
        int enemiesPerSide = enemyCount / 4; // 每条边上的敌人数量

        for (int i = 0; i < enemiesPerSide; i++)
        {
            float t = (float)i / (enemiesPerSide - 1); // 计算每个敌人的位置比例

            // 计算每条边上的敌人位置
            Vector3[] positions = new Vector3[]
            {
                new Vector3(-sideLength / 2 + t * sideLength, sideLength / 2, transform.position.z), // 上边
                new Vector3(sideLength / 2, sideLength / 2 - t * sideLength, transform.position.z), // 右边
                new Vector3(sideLength / 2 - t * sideLength, -sideLength / 2, transform.position.z), // 下边
                new Vector3(-sideLength / 2, -sideLength / 2 + t * sideLength, transform.position.z) // 左边
            };

            // 在每个计算的位置实例化敌人
            foreach (var pos in positions)
            {
                CreateEnemy(pos, new ZMovementCommand(20f));
            }
        }
    }

    /// <summary>
    /// 测试特定的生成方式
    /// </summary>
    /// <param name="method">生成方式的编号</param>
    void TestSpawnMethod(int method)
    {
        switch (method)
        {
            case 0:
                // 生成单个敌人
                SpawnEnemy();
                break;
            case 1:
                // 生成螺旋形敌人
                SpawnEnemiesInSpiral(4, 15, 2f, 1.5f);
                break;
            case 2:
                // 生成圆形敌人
                SpawnEnemiesInCircle(5f, 20);
                break;
            case 3:
                // 生成波纹形敌人
                SpawnEnemiesInWave(5, 10, 2f, 1f);
                break;
            case 4:
                // 生成三角形敌人
                SpawnEnemiesInTriangle(5f, 10);
                break;
            case 5:
                // 生成正方形敌人
                SpawnEnemiesInSquare(5f, 30);
                break;
            case 6:
                // 生成圆形敌人
                SpawnEnemiesInCircleIndividually(5f, 100, 0.2f);
                break;
            default:
                Debug.LogWarning("无效的生成方式编号");
                break;
        }
    }

    /// <summary>
    /// 逐个生成圆形形状的敌人
    /// </summary>
    /// <param name="radius">圆的半径</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    /// <param name="spawnDelay">每个敌人生成的延迟时间</param>
    public void SpawnEnemiesInCircleIndividually(float radius, int enemyCount, float spawnDelay)
    {
        StartCoroutine(SpawnEnemiesInCircleIndividuallyCoroutine(radius, enemyCount, spawnDelay));
    }

    /// <summary>
    /// 协程：逐个生成圆形形状的敌人
    /// </summary>
    private IEnumerator SpawnEnemiesInCircleIndividuallyCoroutine(float radius, int enemyCount, float spawnDelay)
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
            CreateEnemy(spawnPosition, new ZMovementCommand(20f));

            // 等待一段时间后再生成下一个敌人
            yield return new WaitForSeconds(spawnDelay);
        }
    }




    private void CreateEnemy(Vector3 spawnPosition, IMovementCommand movementCommand)
    {
        // 在指定位置实例化敌人，并将其设置为当前对象的子节点
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        enemy.GetComponent<Enemy>().Init(movementCommand);
    }
}
