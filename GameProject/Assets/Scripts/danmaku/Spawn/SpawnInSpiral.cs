using System.Collections.Generic;
using UnityEngine;
public class SpawnInSpiralParameters : SpawnParameters
{
    public int spiralTurns = 1;
    public int enemyCount = 20;
    public float radiusIncrement = 1f;



    /// <summary>
    /// 螺旋生成参数
    /// </summary>
    /// <param name="spiralTurns">螺旋圈数</param>
    /// <param name="enemyCount">生成数量</param>
    /// <param name="radiusIncrement">半径增量</param>
    public SpawnInSpiralParameters(int spiralTurns = 4, int enemyCount = 150, float radiusIncrement = 2f)
    {
        this.spiralTurns = spiralTurns;
        this.enemyCount = enemyCount;
        this.radiusIncrement = radiusIncrement;
    }
}




public class SpawnInSpiral : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnInSpiralParameters parameters = (SpawnInSpiralParameters)spawnParameters;

        float zPosition = EnemyCreator.Instance.zPosition;
        List<Vector3> spawnPositions = new List<Vector3>();

       for (int i = 0; i < parameters.enemyCount; i++)
        {
            // 计算每个敌人的角度
            float angle = i * Mathf.PI * 2 * parameters.spiralTurns / parameters.enemyCount;

            // 计算当前敌人的半径
            float radius = parameters.radiusIncrement * angle / (Mathf.PI * 2);

            // 计算敌人的位置
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // 使用当前对象的 Z 坐标
            Vector3 spawnPosition = new Vector3(x, y, zPosition);
            spawnPositions.Add(spawnPosition);

        }
        return spawnPositions.ToArray();

    }
}

