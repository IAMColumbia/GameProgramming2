using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Spawner
{
    public interface ISpawner
    {

        bool SpawnerEnabled { get; set; }

        void Spawn();
        void AddGameObject(GameObject spawn);
        void SetupSpawnObject(GameObject go);
    }
}
