using UnityEngine;
using System.Collections.Generic;


public class SpawnEnemiesInCircleParameters : SpawnParameters
{
    public float radius = 5f;
    public int enemyCount = 20;

    public SpawnEnemiesInCircleParameters(float radius = 5f, int enemyCount = 20)
    {
        this.radius = radius;
        this.enemyCount = enemyCount;
    }
}

/// <summary>
/// 生成圆形形状的敌人
/// </summary>
public class SpawnEnemiesInCircle : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnEnemiesInCircleParameters parameters = (SpawnEnemiesInCircleParameters)spawnParameters;
        float zPosition = EnemyCreator.Instance.zPosition;
        List<Vector3> spawnPositions = new List<Vector3>();



        for (int i = 0; i < parameters.enemyCount; i++)
        {
            // 计算每个敌人的角度
            float angle = i * Mathf.PI * 2 / parameters.enemyCount;

            // 计算敌人的位置
            float x = Mathf.Cos(angle) * parameters.radius;
            float y = Mathf.Sin(angle) * parameters.radius;

            // 使用当前对象的 Z 坐标
            Vector3 spawnPosition = new Vector3(x, y, zPosition);

            // 在指定位置实例化敌人，并将其设置为当前对象的子节点

            spawnPositions.Add(spawnPosition);
        }
        return spawnPositions.ToArray();
    }
}