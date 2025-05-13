

using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemiesInTriangleParameters : SpawnParameters
{
    public float sideLength = 5f;
    public int enemyCount = 10;

    /// <summary>
    /// 生成等边三角形形状的敌人
    /// </summary>
    /// <param name="sideLength">三角形的边长</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    public SpawnEnemiesInTriangleParameters(float sideLength = 5f, int enemyCount = 10)
    {
        this.sideLength = sideLength;
        this.enemyCount = enemyCount;
    }
}



/// <summary>
/// 生成等边三角形形状的敌人
/// </summary>
public class SpawnEnemiesInTriangle : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnEnemiesInTriangleParameters parameters = (SpawnEnemiesInTriangleParameters)spawnParameters;
        int rows = Mathf.CeilToInt(Mathf.Sqrt(2 * parameters.enemyCount)); // 计算需要的行数
        float zPosition = EnemyCreator.Instance.zPosition;
        List<Vector3> spawnPositions = new List<Vector3>();

        for (int row = 0; row < rows; row++)
        {
            int enemiesInRow = row + 1; // 每行的敌人数量逐渐增加
            float yOffset = row * (parameters.sideLength / rows); // 计算每行的 Y 偏移

            for (int i = 0; i < enemiesInRow; i++)
            {
                float xOffset = (i - row / 2f) * (parameters.sideLength / rows); // 计算每个敌人的 X 偏移

                // 使用当前对象的 Z 坐标
                Vector3 spawnPosition = new Vector3(xOffset, yOffset, zPosition);

                spawnPositions.Add(spawnPosition);
            }
        }
        return spawnPositions.ToArray();
    }

}

