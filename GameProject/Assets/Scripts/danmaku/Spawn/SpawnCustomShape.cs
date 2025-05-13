
using UnityEngine;
using System.Collections.Generic;


public enum SpawnCustomShapeType
{
    夜,
    露,
    死,
    苦
}

public class SpawnCustomShapeParameters : SpawnParameters
{
    public SpawnCustomShapeType spawnCustomShapeType;


    public SpawnCustomShapeParameters(SpawnCustomShapeType spawnCustomShapeType)
    {
        this.spawnCustomShapeType = spawnCustomShapeType;
    }
}

public class SpawnCustomShape : ISpawnMode
{
    public Vector3[] SpawnPosition(SpawnParameters spawnParameters)
    {
        SpawnCustomShapeParameters parameters = (SpawnCustomShapeParameters)spawnParameters;

        List<Vector3> spawnPositions = new List<Vector3>();
        float zPosition = EnemyCreator.Instance.zPosition;


        Transform textParent = null;

        switch (parameters.spawnCustomShapeType)
        {
            case SpawnCustomShapeType.夜:
                textParent = EnemyCreator.Instance.夜;
                break;
            case SpawnCustomShapeType.露:
                textParent = EnemyCreator.Instance.露;
                break;
            case SpawnCustomShapeType.死:
                textParent = EnemyCreator.Instance.死;
                break;
            case SpawnCustomShapeType.苦:
                textParent = EnemyCreator.Instance.苦;
                break;
        }

        Transform[] positions = textParent.GetComponentsInChildren<Transform>();
        foreach (var position in positions)
        {
            Vector3 spawnPosition = new Vector3(position.position.x, position.position.y, zPosition);
            spawnPositions.Add(spawnPosition);
        }
        return spawnPositions.ToArray();
    }
}
