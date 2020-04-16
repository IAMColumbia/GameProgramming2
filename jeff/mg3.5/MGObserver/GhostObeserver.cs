using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGPacManComponents.Ghost;
using Microsoft.Xna.Framework;
using MGPacManComponents.Pac;
using Observer;

namespace MGObserver
{
    public class GhostObeserver : MonogameGhost, IPacmanObserver
    {
        public GhostObeserver(Game game, PacManSubject pac) : base (game, pac)
        {
            pac.Attach(this);
        }

        public void ObserverUpdate()
        {
            //Doens't support update by design
            throw new NotImplementedException();
        }

        public void ObserverUpdate(PacManState p)
        {
            
                switch (p)
                {
                    case PacManState.SuperPacMan:
                        this.Ghost.State = GhostState.Evading;
                        break;
                    case PacManState.EndSuperPacMan:
                        this.Ghost.State = GhostState.Roving;
                        break;
                    case PacManState.Chomping:
                    case PacManState.Still:
                    if (this.Ghost.State != GhostState.Evading
                       || this.Ghost.State != GhostState.Dead)
                    {
                        this.Ghost.State = GhostState.Chasing;
                    }
                    this.Ghost.State = GhostState.Chasing;
                        break;
                    case PacManState.Dying:
                    case PacManState.Spawning:
                        this.Ghost.State = GhostState.Roving;
                        break;

                }
                this.ghost.gameConsole.GameConsoleWrite(this + " notified " + p);
            
#if DEBUG
            
#endif
        }

        protected override void UpdateGhostDeadState()
        {
           
            //Base states sets back to roving
            // base.UpdateGhostDeadState();
        }


    }
}
