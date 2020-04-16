using UnityEngine;
using System.Collections;

public class Pool : MonoBehaviour {

    public GameObject GhostPrefab;
    
    public int IntialPoolSize = 1;
    public int MaxPoolSize = 5;

    // Use this for initialization
    void Start () {
        CreateObjectPools();
	}

    public void CreateObjectPools()
    {
        ObjectPoolingManager.Instance.PoolGameObject = this.gameObject;
        ObjectPoolingManager.Instance.CreatePool(GhostPrefab, this.IntialPoolSize, 5, false);
        
     }

    // Update is called once per frame
    void Update () {
	
	}
}
