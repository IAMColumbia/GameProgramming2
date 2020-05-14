using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Food started");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Food OnCollisionEnter2D");
        if (coll.gameObject.tag == "Player")
        {
            this.Hit(coll.gameObject);
            
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Food OnTriggerEnter2D");
        if (coll.gameObject.tag == "Player")
        {
            this.Hit(coll.gameObject);
            
        }
    }

    public virtual void Hit(GameObject p)
    {
        Destroy(this.gameObject);
    }
}
