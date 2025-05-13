using UnityEngine;
using YanGameFrameWork.Singleton;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic; // 引入命名空间



public class EnemyCreator : Singleton<EnemyCreator>
{
    [Header("敌人预制体")]
    public GameObject enemyPrefab; // 敌人预制体

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

    public float zPosition = 0f;









    [Header("敌人材质")]
    public Material enemyMaterial;

    [Header("酒材质")]
    public Material wineMaterial;




    [Header("敌人列表")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("酒列表")]
    public List<Enemy> wines = new List<Enemy>();


    public Transform enemyContainer;
    public Transform wineContainer;



    void Start()
    {
        zPosition = transform.position.z;

        YanGF.ObjectPool.RegisterPool(enemyPrefab);
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



    #region 生成



    /// <summary>
    /// 生成酒
    /// </summary>
    /// 
    [Button("生成酒")]
    public void SpawnWine(Sprite enemySprite, ISpawnMode spawnMode, SpawnParameters spawnParameters, IMovementCommand movementCommand)
    {
        Vector3[] spawnPositions = spawnMode.SpawnPosition(spawnParameters);

        foreach (var spawnPosition in spawnPositions)
        {
            CreateWine(spawnPosition, enemySprite, movementCommand);
        }
    }



    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="enemySprite"></param>
    /// <param name="spawnMode"></param>
    /// <param name="spawnParameters"></param>
    /// <param name="movementCommand"></param>
    public void SpawnEnemy(Sprite enemySprite, ISpawnMode spawnMode, SpawnParameters spawnParameters, IMovementCommand movementCommand)
    {
        Vector3[] spawnPositions = spawnMode.SpawnPosition(spawnParameters);
        foreach (var spawnPosition in spawnPositions)
        {
            // 在指定位置实例化敌人，并将其设置为当前对象的子节点
            CreateEnemy(spawnPosition, enemySprite, movementCommand);
        }

    }


    /// <summary>
    /// 以间隔生成敌人
    /// </summary>
    /// <param name="enemySprite"></param>
    /// <param name="spawnMode"></param>
    /// <param name="spawnParameters"></param>
    /// <param name="movementCommand"></param>
    /// <param name="interval"></param>
    public void SpawnEnemiesWithInterval(Sprite enemySprite, ISpawnMode spawnMode, SpawnParameters spawnParameters, IMovementCommand movementCommand, float interval)
    {
        StartCoroutine(SpawnEnemiesWithIntervalCoroutine(enemySprite, spawnMode, spawnParameters, movementCommand, interval));
    }


    /// <summary>
    /// 协程：以间隔生成敌人
    /// </summary>
    /// <param name="movementMethod">敌人的移动方式</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    /// <param name="interval">每个敌人生成的间隔时间</param>
    private IEnumerator SpawnEnemiesWithIntervalCoroutine(
        Sprite enemySprite,
        ISpawnMode spawnMode,
        SpawnParameters spawnParameters,
        IMovementCommand movementCommand,
        float interval)
    {
        Vector3[] spawnPositions = spawnMode.SpawnPosition(spawnParameters);
        foreach (var spawnPosition in spawnPositions)
        {
            // 在指定位置实例化敌人，并将其设置为当前对象的子节点
            CreateEnemy(spawnPosition, enemySprite, movementCommand);
            yield return new WaitForSeconds(interval);
        }
    }

    #endregion





    // /// <summary>
    // /// 生成任意形状的敌人
    // /// </summary>
    // /// <param name="positions">敌人生成位置的Transform数组</param>
    // [Button("生成任意形状的敌人")]
    // public void SpawnEnemiesInCustomShape(Sprite enemySprite,IMovementCommand movementCommand)
    // {
    //     Transform[] positions = GameObject.Find("文字预制体").GetComponentsInChildren<Transform>();
    //     foreach (var position in positions)
    //     {

    //         Vector3 spawnPosition = new Vector3(position.position.x, position.position.y, zPosition);
    //         // 在每个Transform的位置生成敌人
    //         CreateEnemy(spawnPosition, enemySprite, movementCommand);
    //     }
    // }


    // /// <summary>
    // /// 生成围绕 (0, 0) 点的敌人
    // /// </summary>
    // /// <param name="radius">圆周的半径</param>
    // /// <param name="enemyCount">要生成的敌人数量</param>
    // [Button("生成圆形敌人")]
    // public void SpawnEnemiesInCircle(Sprite enemySprite,float radius = 5f, int enemyCount = 20,IMovementCommand movementCommand)
    // {
    //     for (int i = 0; i < enemyCount; i++)
    //     {
    //         // 计算每个敌人的角度
    //         float angle = i * Mathf.PI * 2 / enemyCount;

    //         // 计算敌人的位置
    //         float x = Mathf.Cos(angle) * radius;
    //         float y = Mathf.Sin(angle) * radius;

    //         // 使用当前对象的 Z 坐标
    //         Vector3 spawnPosition = new Vector3(x, y, transform.position.z);

    //         // 在指定位置实例化敌人，并将其设置为当前对象的子节点
    //         CreateEnemy(spawnPosition, enemySprite, movementCommand);
    //     }
    // }

    // /// <summary>
    // /// 生成螺旋形状的敌人
    // /// </summary>
    // /// <param name="spiralTurns">螺旋的圈数</param>
    // /// <param name="enemyCount">要生成的敌人数量</param>
    // /// <param name="radiusIncrement">每圈半径的增量</param>
    // /// <param name="spawnDelay">每个敌人生成的延迟时间</param>
    // [Button("生成螺旋形状的敌人")]
    // public void SpawnEnemiesInSpiral(int spiralTurns = 4, int enemyCount = 150, float radiusIncrement = 2f, float spawnDelay = 0.1f)
    // {
    //     StartCoroutine(SpawnEnemiesInSpiralCoroutine(spiralTurns, enemyCount, radiusIncrement, spawnDelay));
    // }

    // /// <summary>
    // /// 生成圆形波纹形状的敌人
    // /// </summary>
    // /// <param name="waveCount">波纹的数量</param>
    // /// <param name="enemiesPerWave">每个波纹的敌人数量</param>
    // /// <param name="radiusIncrement">每个波纹的半径增量</param>
    // /// <param name="spawnDelay">每个波纹生成的延迟时间</param>
    // [Button("生成圆形波纹形状的敌人")]
    // public void SpawnEnemiesInWave(int waveCount = 5, int enemiesPerWave = 10, float radiusIncrement = 2f, float spawnDelay = 1f)
    // {
    //     StartCoroutine(SpawnEnemiesInWaveCoroutine(waveCount, enemiesPerWave, radiusIncrement, spawnDelay));
    // }



    // /// <summary>
    // /// 生成等边三角形形状的敌人
    // /// </summary>
    // /// <param name="sideLength">三角形的边长</param>
    // /// <param name="enemyCount">要生成的敌人数量</param>
    // [Button("生成等边三角形形状的敌人")]
    // public void SpawnEnemiesInTriangle(Sprite enemySprite,float sideLength = 5f, int enemyCount = 10,IMovementCommand movementCommand)
    // {
    //     int rows = Mathf.CeilToInt(Mathf.Sqrt(2 * enemyCount)); // 计算需要的行数

    //     for (int row = 0; row < rows; row++)
    //     {
    //         int enemiesInRow = row + 1; // 每行的敌人数量逐渐增加
    //         float yOffset = row * (sideLength / rows); // 计算每行的 Y 偏移

    //         for (int i = 0; i < enemiesInRow; i++)
    //         {
    //             float xOffset = (i - row / 2f) * (sideLength / rows); // 计算每个敌人的 X 偏移

    //             // 使用当前对象的 Z 坐标
    //             Vector3 spawnPosition = new Vector3(xOffset, yOffset, transform.position.z);

    //             // 在指定位置实例化敌人，并将其设置为当前对象的子节点
    //             CreateEnemy(spawnPosition, enemySprite, movementCommand);
    //         }
    //     }
    // }



    // /// <summary>
    // /// 生成正方形形状的敌人
    // /// </summary>
    // /// <param name="sideLength">正方形的边长</param>
    // /// <param name="enemyCount">要生成的敌人数量</param>
    // [Button("生成正方形形状的敌人")]
    // public void SpawnEnemiesInSquare(Sprite enemySprite,ISpawnMode spawnMode,SpawnParameters spawnParameters,IMovementCommand movementCommand)
    // {
    //     Vector3[] spawnPositions = spawnMode.SpawnPosition(spawnParameters);
    //     foreach (var spawnPosition in spawnPositions)
    //     {
    //         CreateEnemy(spawnPosition, enemySprite, movementCommand);
    //     }

    // }

    // /// <summary>
    // /// 生成动态变化的正方形形状的敌人
    // /// </summary>
    // /// <param name="initialSideLength">初始正方形的边长</param>
    // /// <param name="enemyCount">每批次生成的敌人数量</param>
    // /// <param name="batchCount">生成的批次数量</param>
    // /// <param name="interval">每批次生成的时间间隔</param>
    // /// <param name="growthRate">每批次边长的增长率</param>
    // /// 
    // [Button("生成动态变化的正方形形状的敌人")]
    // public void SpawnEnemiesInGrowingSquare(float initialSideLength = 6f, int enemyCount = 100, int batchCount = 10, float interval = 1f, float growthRate = 0.5f, float squareOffsetX = 4f, float squareOffsetY = 4f)
    // {
    //     StartCoroutine(SpawnEnemiesInGrowingSquareCoroutine(initialSideLength, enemyCount, batchCount, interval, growthRate, squareOffsetX, squareOffsetY));
    // }




    // /// <summary>
    // /// 逐个生成圆形形状的敌人
    // /// </summary>
    // /// <param name="radius">圆的半径</param>
    // /// <param name="enemyCount">要生成的敌人数量</param>
    // /// <param name="spawnDelay">每个敌人生成的延迟时间</param>
    // [Button("逐个生成圆形形状的敌人")]
    // public void SpawnEnemiesInCircleIndividually(float radius = 5f, int enemyCount = 30, float spawnDelay = 0.1f)
    // {
    //     StartCoroutine(SpawnEnemiesInCircleIndividuallyCoroutine(radius, enemyCount, spawnDelay));
    // }



    // /// <summary>
    // /// 协程：逐个生成螺旋形状的敌人
    // /// </summary>
    // private IEnumerator SpawnEnemiesInSpiralCoroutine(int spiralTurns, int enemyCount, float radiusIncrement, float spawnDelay)
    // {
    //     for (int i = 0; i < enemyCount; i++)
    //     {
    //         // 计算每个敌人的角度
    //         float angle = i * Mathf.PI * 2 * spiralTurns / enemyCount;

    //         // 计算当前敌人的半径
    //         float radius = radiusIncrement * angle / (Mathf.PI * 2);

    //         // 计算敌人的位置
    //         float x = Mathf.Cos(angle) * radius;
    //         float y = Mathf.Sin(angle) * radius;

    //         // 使用当前对象的 Z 坐标
    //         Vector3 spawnPosition = new Vector3(x, y, transform.position.z);

    //         // 在指定位置实例化敌人，并将其设置为当前对象的子节点
    //         CreateEnemy(spawnPosition, new ZMovementCommand(20f));

    //         // 等待一段时间后再生成下一个敌人
    //         yield return new WaitForSeconds(spawnDelay);
    //     }
    // }



    // /// <summary>
    // /// 协程：逐个生成圆形波纹形状的敌人
    // /// </summary>
    // private IEnumerator SpawnEnemiesInWaveCoroutine(int waveCount, int enemiesPerWave, float radiusIncrement, float spawnDelay)
    // {
    //     for (int wave = 0; wave < waveCount; wave++)
    //     {
    //         float radius = wave * radiusIncrement;
    //         for (int i = 0; i < enemiesPerWave; i++)
    //         {
    //             float angle = i * Mathf.PI * 2 / enemiesPerWave;
    //             float x = Mathf.Cos(angle) * radius;
    //             float y = Mathf.Sin(angle) * radius;
    //             Vector3 spawnPosition = new Vector3(x, y, transform.position.z);
    //             CreateEnemy(spawnPosition, new ZMovementCommand(20f));
    //         }
    //         yield return new WaitForSeconds(spawnDelay);
    //     }
    // }






    // /// <summary>
    // /// 协程：逐个生成圆形形状的敌人
    // /// </summary>
    // private IEnumerator SpawnEnemiesInCircleIndividuallyCoroutine(float radius, int enemyCount, float spawnDelay)
    // {
    //     for (int i = 0; i < enemyCount; i++)
    //     {
    //         // 计算每个敌人的角度
    //         float angle = i * Mathf.PI * 2 / enemyCount;

    //         // 计算敌人的位置
    //         float x = Mathf.Cos(angle) * radius;
    //         float y = Mathf.Sin(angle) * radius;

    //         // 使用当前对象的 Z 坐标
    //         Vector3 spawnPosition = new Vector3(x, y, transform.position.z);

    //         // 在指定位置实例化敌人，并将其设置为当前对象的子节点
    //         CreateEnemy(spawnPosition, new ZMovementCommand(20f));

    //         // 等待一段时间后再生成下一个敌人
    //         yield return new WaitForSeconds(spawnDelay);
    //     }
    // }




    private void CreateEnemy(Vector3 spawnPosition, Sprite enemySprite, IMovementCommand movementCommand)
    {
        // 从对象池中获取敌人对象
        GameObject enemy = YanGF.ObjectPool.GetItem(enemyPrefab);
        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.identity;
        enemy.transform.SetParent(transform);

        enemy.tag = "障碍物";
        EnemyParameters enemyParameters = new EnemyParameters(enemyMaterial, enemySprite, 5f, () =>
        {
            // 碰撞处理
            print("碰撞了" + enemy.name);

            GameManager.Instance.LoseHP(DataConfig.loseHP);

            FunDialogController.Instance.ShowOnCollisionEnemyDialogLimited();
        });
        enemy.GetComponent<Enemy>().Init(movementCommand, enemyParameters);
        enemy.name = "敌人" + enemies.Count;
        enemy.transform.SetParent(enemyContainer);
        enemies.Add(enemy.GetComponent<Enemy>());
    }

    private void CreateWine(Vector3 spawnPosition, Sprite wineSprite, IMovementCommand movementCommand)
    {
        // 从对象池中获取酒对象
        GameObject wine = YanGF.ObjectPool.GetItem(enemyPrefab);
        wine.transform.position = spawnPosition;
        wine.transform.rotation = Quaternion.identity;
        wine.transform.SetParent(transform);

        wine.tag = "酒";
        EnemyParameters enemyParameters = new EnemyParameters(wineMaterial, wineSprite, 2f, () =>
        {
            // 碰撞处理
            print("碰撞了" + wine.name);

            GameManager.Instance.LoseHP(DataConfig.loseHP);
        });
        wine.GetComponent<Enemy>().Init(movementCommand, enemyParameters);
        wine.name = "酒" + wines.Count;
        wine.transform.SetParent(wineContainer);
        wines.Add(wine.GetComponent<Enemy>());
    }



    public void DestroyItem(Enemy enemy)
    {
        if (enemy == null) return; // 检查敌人是否为 null

        if (enemy.tag == "障碍物")
        {
            enemies.Remove(enemy);
            // 将敌人返回到对象池
            YanGF.ObjectPool.ReturnItem(enemy.gameObject, enemyPrefab);
        }
        else if (enemy.tag == "酒")
        {
            wines.Remove(enemy);
            // 将酒返回到对象池
            YanGF.ObjectPool.ReturnItem(enemy.gameObject, enemyPrefab);
        }
    }




    /// <summary>
    /// 消除所有敌人并停止所有协程
    /// </summary>
    [Button("消除所有敌人")]
    public void ClearAllEnemies()
    {
        // 停止所有协程
        StopAllCoroutines();
        // 遍历并销毁每个敌人
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        Debug.Log("所有敌人已被消除");
        enemies.Clear();
    }

    // /// <summary>
    // /// 协程生成动态变化的正方形形状的敌人
    // /// </summary>
    // /// <param name="initialSideLength">初始正方形的边长</param>
    // /// <param name="enemyCount">每批次生成的敌人数量</param>
    // /// <param name="batchCount">生成的批次数量</param>
    // /// <param name="interval">每批次生成的时间间隔</param>
    // /// <param name="growthRate">每批次边长的增长率</param>
    // /// <param name="squareOffsetX">正方形的偏移X</param>
    // /// <param name="squareOffsetY">正方形的偏移Y</param>
    // public IEnumerator SpawnEnemiesInGrowingSquareCoroutine(float initialSideLength, int enemyCount, int batchCount, float interval, float growthRate, float squareOffsetX, float squareOffsetY)
    // {
    //     float currentSideLength = initialSideLength;

    //     for (int batch = 0; batch < batchCount; batch++)
    //     {
    //         SpawnEnemiesInSquare(currentSideLength, enemyCount, squareOffsetX, squareOffsetY);
    //         currentSideLength += growthRate; // 增加边长
    //         yield return new WaitForSeconds(interval); // 等待指定的时间间隔
    //     }
    // }

}
