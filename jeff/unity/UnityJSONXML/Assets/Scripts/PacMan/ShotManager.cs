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
        var shot = new ShotSprite() { Speed = 5, Direction = Direction };
        Shots.Add((ShotSprite)Instantiate(shot, Position, Quaternion.identity));
        shot.State = ShotSprite.ShotState.Shooting;
        Shots.Add(shot);
    }

    private void RemoveDisabledShots()
    {
        shotsToRemove.Clear(); //get rid of old shots to remove
        //Find disabled shots andf add them to the list to be removed
        foreach (ShotSprite s in Shots)
        {
            if (s.enabled == false) 
                shotsToRemove.Add(s);
        }
        //remove the disavled shots
        foreach (var shotToRemove in shotsToRemove)
        {
            this.Shots.Remove(shotToRemove);
        }
    }
}
