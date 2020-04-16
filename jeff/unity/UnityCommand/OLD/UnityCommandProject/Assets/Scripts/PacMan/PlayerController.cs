using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 direction = new Vector2();
    public Vector2 keyDirection;
	public bool IsKeyDown {
		get {
			if(keyDirection.sqrMagnitude == 0) return false;
			return true;}
	}

    public PlayerController()
    {
        keyDirection = new Vector2();
    }
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//direction.x = direction.y = 0;
        keyDirection.x = keyDirection.y = 0;
		
        //Keyboard
        if (Input.GetKey("right"))
        {
            keyDirection.x += 1;
        }
		if (Input.GetKey ("left")) {
            keyDirection.x += -1;
		}

        if (Input.GetKey("up"))
        {
            keyDirection.y += 1;
        }
		if (Input.GetKey ("down")) {
            keyDirection.y += -1;
		}

        //direction += keyDirection;
        //direction.Normalize();
		
	}
}
