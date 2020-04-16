using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spawner
{
    public class Spawner : MonoBehaviour, ISpawner
    {

        public bool SpawnerEnabled { get; set; }

        public GameObject SpawnObject;
        public bool Enabled = true;

        protected List<GameObject> gameObjects;
        protected List<GameObject> objectsToRemove;
        // Use this for initialization 
        void Start()
        {
            this.gameObjects = new List<GameObject>();
            this.objectsToRemove = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            removeObjectInListToRemove();


            //Test spawn new object with the G key
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (SpawnObject != null)
                {
                    Spawn();

                }
            }

        }

        protected void removeObjectInListToRemove()
        {
            //remove objects in Object to remove list
            foreach (GameObject go in this.objectsToRemove)
            {
                this.gameObjects.Remove(go);
                //DestroyObject(go);
                Object.Destroy(go);
            }
        }

        public void Spawn()
        {
            if (SpawnerEnabled)
            {
                GameObject spawn = (GameObject)Instantiate(SpawnObject, this.transform.position, Quaternion.identity);
                SetupSpawnObject(spawn); //virtual hook for setting up game object
                this.AddGameObject(spawn);
            }
        }

        public virtual void AddGameObject(GameObject spawn)
        {
            gameObjects.Add(spawn);
        }

        public virtual void SetupSpawnObject(GameObject go)
        {
            //Nothing todo should override with specific type
        }
    }
}
