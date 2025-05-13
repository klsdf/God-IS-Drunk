using UnityEngine;


public abstract class SpawnParameters
{
    
}

public interface ISpawnMode
{
    Vector3[] SpawnPosition(SpawnParameters spawnParameters);
}