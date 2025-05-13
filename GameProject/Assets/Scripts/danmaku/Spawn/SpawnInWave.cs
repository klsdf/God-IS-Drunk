using UnityEngine;
using System.Collections.Generic;

public class SpawnInWaveParameters : SpawnParameters
{
    public int waveCount = 1;
    public int enemiesPerWave = 10;
    public float radiusIncrement = 1f;


    /// <summary>
    /// 同心圆生成参数
    /// </summary>
    /// <param name="waveCount">同心圆的数量</param>
    /// <param name="enemiesPerWave">每个同心圆的敌人数量</param>
    /// <param name="radiusIncrement">每个同心圆的半径增量</param>
    public SpawnInWaveParameters(int waveCount = 5, int enemiesPerWave = 10, float radiusIncrement = 2f)
    {
        this.waveCount = waveCount;
        this.enemiesPerWave = enemiesPerWave;
        this.radiusIncrement = radiusIncrement;
    }
}


/// <summary>
/// 同心圆生成模式
/// </summary>
public class SpawnInWave : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnInWaveParameters parameters = (SpawnInWaveParameters)spawnParameters;

        float zPosition = EnemyCreator.Instance.zPosition;
        List<Vector3> spawnPositions = new List<Vector3>();


         for (int wave = 0; wave < parameters.waveCount; wave++)
        {
            float radius = wave * parameters.radiusIncrement;
            for (int i = 0; i < parameters.enemiesPerWave; i++)
            {
                float angle = i * Mathf.PI * 2 / parameters.enemiesPerWave;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                Vector3 spawnPosition = new Vector3(x, y, zPosition);
                spawnPositions.Add(spawnPosition);
            }
        }
        return spawnPositions.ToArray();
    }
}

