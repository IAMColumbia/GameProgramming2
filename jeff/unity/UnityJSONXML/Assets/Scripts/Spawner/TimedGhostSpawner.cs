using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Spawner
{
    public class TimedGhostSpawner : GhostSpawner
    {
        public float SpawnTime;
        protected float lastSpawnTime;
        protected bool spawned;

        void Update()
        {
            base.addDeadGhostsToRemoveList();
            base.removeObjectInListToRemove();

            lastSpawnTime += Time.deltaTime;
            if ((lastSpawnTime > SpawnTime) && (spawned == false))
            {
                //Only spawns once
                this.Spawn();
                this.spawned = true;
            }

        }
    }
}
