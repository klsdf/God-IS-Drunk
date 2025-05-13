
using UnityEngine;
using System.Collections.Generic;

public class SpawnCustomShapeParameters : SpawnParameters
{
}

public class SpawnCustomShape : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnCustomShapeParameters parameters = (SpawnCustomShapeParameters)spawnParameters;

        List<Vector3> spawnPositions = new List<Vector3>();
        float zPosition = EnemyCreator.Instance.zPosition;
        Transform[] positions = GameObject.Find("文字预制体").GetComponentsInChildren<Transform>();
        foreach (var position in positions)
        {
            Vector3 spawnPosition = new Vector3(position.position.x, position.position.y, zPosition);
            spawnPositions.Add(spawnPosition);
        }
        return spawnPositions.ToArray();
    }
}
