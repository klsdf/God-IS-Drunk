using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpawnInSquareParameters : SpawnParameters
{
    public float sideLength = 5f;
    public int enemyCount = 10;
    public float squareOffsetX = 4f;
    public float squareOffsetY = 4f;


    /// <summary>
    /// 生成正方形形状的敌人
    /// </summary>
    /// <param name="sideLength">正方形的边长</param>
    /// <param name="enemyCount">要生成的敌人数量</param>
    /// <param name="squareOffsetX">正方形的偏移X</param>
    /// <param name="squareOffsetY">正方形的偏移Y</param>
    public SpawnInSquareParameters(
        float sideLength = 6f, 
        int enemyCount = 100, 
        float squareOffsetX = 4f,
        float squareOffsetY = 4f)
    {

        this.sideLength = sideLength;
        this.enemyCount = enemyCount;
        this.squareOffsetX = squareOffsetX;
        this.squareOffsetY = squareOffsetY;
    }
}




/// <summary>
/// 生成正方形形状的敌人
/// </summary>
public class SpawnInSquare : ISpawnMode
{

    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnInSquareParameters parameters = (SpawnInSquareParameters)spawnParameters;
        int enemiesPerSide = parameters.enemyCount / 4; // 每条边上的敌人数量
        List<Vector3> spawnPositions = new List<Vector3>();
        float zPosition = EnemyCreator.Instance.zPosition;

        // 为整个正方形计算一个随机偏移，使其在 (3, 3) 范围内
        float randomXOffset = Random.Range(-parameters.squareOffsetX, parameters.squareOffsetX);
        float randomYOffset = Random.Range(-parameters.squareOffsetY, parameters.squareOffsetY);
        Vector3 squareOffset = new Vector3(randomXOffset, randomYOffset, 0);

        for (int i = 0; i < enemiesPerSide; i++)
        {
            float t = (float)i / (enemiesPerSide - 1); // 计算每个敌人的位置比例

            // 计算每条边上的敌人位置
            Vector3[] positions = new Vector3[]
            {
                new Vector3(-parameters.sideLength / 2 + t * parameters.sideLength, parameters.sideLength / 2, zPosition), // 上边
                new Vector3(parameters.sideLength / 2, parameters.sideLength / 2 - t * parameters.sideLength, zPosition), // 右边
                new Vector3(parameters.sideLength / 2 - t * parameters.sideLength, -parameters.sideLength / 2, zPosition), // 下边
                new Vector3(-parameters.sideLength / 2, -parameters.sideLength / 2 + t * parameters.sideLength, zPosition) // 左边
            };

            // 在每个计算的位置实例化敌人
            foreach (var pos in positions)
            {
                // 应用正方形的随机偏移
                Vector3 spawnPosition = pos + squareOffset;
                spawnPositions.Add(spawnPosition);
            }
        }
        return spawnPositions.ToArray();
    }
}