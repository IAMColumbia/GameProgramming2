using UnityEngine;
using System.Collections;

namespace Spawner
{
    public class GhostSpawner : Spawner
    {

        public GameObject PacMan;

        public override void SetupSpawnObject(GameObject go)
        {
            base.SetupSpawnObject(go);
            if (go.GetComponent<GhostSprite>() != null)
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

                    }
                }
            }
        }
    }
}
