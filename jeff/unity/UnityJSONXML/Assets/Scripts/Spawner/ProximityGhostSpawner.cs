using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Spawner
{
    public enum ProximityGhostSpawnerOptions {  SingleSpawnWhenEnter, ResetWhenLeave };

    class ProximityGhostSpawner : GhostSpawner
    {

        public float SpawnDistance = 2.0f;
        protected bool spawned;

        public ProximityGhostSpawnerOptions Options = ProximityGhostSpawnerOptions.SingleSpawnWhenEnter;

        void Update()
        {
            base.removeObjectInListToRemove();
            base.addDeadGhostsToRemoveList();

            //Spawn if close to PacMan
            float distance = Vector3.Distance(this.PacMan.transform.position, this.transform.position);
            if((distance < this.SpawnDistance) && (spawned == false))
            {
                this.Spawn();
                this.spawned = true;
            }
            //reset spawner
            if(Options == ProximityGhostSpawnerOptions.ResetWhenLeave)
            {
                if(distance > this.SpawnDistance)
                {
                    this.spawned = false;
                }
            }
        }
    }
}
