using UnityEngine;



/// <summary>
/// 用于创建关卡的常驻生成
/// </summary>
public static class LevelCreator
{

    [Header("敌人生成概率")]
    [Range(0.1f, 0.8f)]
    public static float enemySpawnRate = 0.8f;

    public static void SpanNoramlItems(Sprite[] enemySprites)
    {

        ISpawnMode spawnMode = new SpawnRandomPosition();
        SpawnRandomPositionParameters spawnParameters = new SpawnRandomPositionParameters();
        IMovementCommand movementCommand = new ZMovementCommand(20f);

        Vector3[] spawnPositions = spawnMode.SpawnPosition(spawnParameters);

        Sprite chooseSprite = enemySprites[Random.Range(0, enemySprites.Length)];
        foreach (var spawnPosition in spawnPositions)
        {
            // 在指定位置实例化敌人，并将其设置为当前对象的子节点

            float random = Random.Range(0f, 1f); // 生成一个0到1之间的随机数

            // 使用enemySpawnRate作为生成敌人的概率
            if (random < enemySpawnRate)
            {
                EnemyCreator.Instance.SpawnEnemy(chooseSprite, spawnMode, spawnParameters, movementCommand);
            }
            else
            {
                EnemyCreator.Instance.SpawnWine(chooseSprite, spawnMode, spawnParameters, movementCommand);
            }
        }
    }
}



