using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShotManager : MonoBehaviour {


    List<ShotSprite> Shots;
    private List<ShotSprite> shotsToRemove;
    public GameObject prefab;

    // Use this for initialization
    void Start () {
        Shots = new List<ShotSprite>();
        shotsToRemove = new List<ShotSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
        //Offscreen remove shot
	}

    public void AddShot(Vector2 Position, Vector2 Direction)
    {
        ShotSprite shot = new ShotSprite(); 
        Instantiate(shot, Position, Quaternion.identity);
        shot.Speed = 5;
        shot.Direction = Direction;

        Shots.Add(shot);
        shot.State = ShotSprite.ShotState.Shooting;
        Shots.Add(shot);
    }
}
