using UnityEngine;
using System.Collections;

public class GhostSpawner : Spawner
{

    public GameObject PacMan;

    protected override void setupSpawnObject(GameObject go)
    {
        base.setupSpawnObject(go);
        if(go.GetComponent<GhostSprite>() != null)
        {
            GhostSprite gs = go.GetComponent<GhostSprite>();
            gs.Speed = 5;
            gs.PacMan = PacMan;
            gs.SetupGhost();
           
        }
    }

    void Update()
    {
        base.removeObjectInListToRemove();
        addDeadGhostsToRemoveList();
    }

    protected void addDeadGhostsToRemoveList()
    {
        GhostSprite gs;
        foreach (GameObject go in this.gameObjects)
        {
            gs = go.GetComponent<GhostSprite>();
            if (go.GetComponent<GhostSprite>() != null)
            {
                if (gs.State == GhostState.Dead)
                {
                    //remove dead ghosts
                    this.objectsToRemove.Add(gs.gameObject);
                    gs.DetachFromPacMan();
                }
            }
        }
    }
}
