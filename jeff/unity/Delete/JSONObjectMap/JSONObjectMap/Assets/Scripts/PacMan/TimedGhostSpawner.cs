using UnityEngine;
using System.Collections;

public class TimedGhostSpawner : GhostSpawner
{
    public float SpawnTime;
    private float lastSpawnTime;

    void Update()
    {
        base.addDeadGhostsToRemoveList();
        base.removeObjectInListToRemove();

        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime > SpawnTime)
        {
            lastSpawnTime = 0.0f;
            this.Spawn();
        }
        
    }
	
}
