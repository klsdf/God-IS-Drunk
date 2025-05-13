using UnityEngine;


public class SpawnRandomPositionParameters : SpawnParameters
{

}



/// <summary>
/// 随机位置生成
/// </summary>
public class SpawnRandomPosition : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnRandomPositionParameters parameters = (SpawnRandomPositionParameters)spawnParameters;

        float minX = EnemyCreator.Instance.minX;
        float maxX = EnemyCreator.Instance.maxX;
        float minY = EnemyCreator.Instance.minY;
        float maxY = EnemyCreator.Instance.maxY;
        float zPosition = EnemyCreator.Instance.zPosition;
        // 在指定范围内随机生成 X 坐标
        float randomX = Random.Range(minX, maxX);

        float randomY = Random.Range(minY, maxY);
        // 在 Y 位置生成敌人（根据需要，可以增加高度变化或其他随机性）
        Vector3 spawnPosition = new Vector3(randomX, randomY, zPosition);
        return new Vector3[] { spawnPosition };

    }
}