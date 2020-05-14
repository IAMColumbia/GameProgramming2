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
        //var shot = new ShotSprite() { Speed = 5, Direction = Direction };
        //Shots.Add((ShotSprite)Instantiate(prefab, Position, Quaternion.identity));
        //shot.State = ShotSprite.ShotState.Shooting;
        //Shots.Add(shot);
    }
}
