using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public GameObject SpawnObject;
    public bool Enabled = true;

    protected List<GameObject> gameObjects;
    protected List<GameObject> objectsToRemove;
    // Use this for initialization 
    void Start () {
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
            Object.Destroy(go);
        }
    }

    public void Spawn()
    {
        if (Enabled)
        {
            GameObject spawn = (GameObject)Instantiate(SpawnObject, this.transform.position, Quaternion.identity);
            setupSpawnObject(spawn); //virtual hook for setting up game object
            this.addGameObject(spawn);
        }
    }

    protected virtual void addGameObject(GameObject spawn)
    {
        gameObjects.Add(spawn);
    }

    protected virtual void setupSpawnObject(GameObject go)
    {
        //Nothing todo should override with specific type
    }
}
