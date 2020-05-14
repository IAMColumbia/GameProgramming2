using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 direction = new Vector2();
    private Vector2 keyDirection;
    private Vector2 padDirection;
	public bool hasInputForMoverment {
		get {
            //Debug.Log(direction.sqrMagnitude);
            if (direction.magnitude == 0) return false;
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
		
        keyDirection.x = keyDirection.y = 0;
        //padDirection.x = padDirection.y = 0;
		
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
        direction = keyDirection;

        //Gamepad
        //padDirection.x = Input.GetAxis("Horizontal");
        //padDirection.y = Input.GetAxis("Vertical");

        if (padDirection.magnitude > 0)
        {
            //Debug.Log(padDirection + " " + padDirection.magnitude + " " + (padDirection.magnitude > 0));
            direction += padDirection;
        }

        //normalize
        direction.Normalize();
	}
}
