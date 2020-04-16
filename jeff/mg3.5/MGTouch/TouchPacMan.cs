using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;
using MonoGameLibrary.GameComponents.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input.Touch;

namespace MGTouch
{
    class TouchPacMan : MonogamePacMan
    {
        public TouchPacMan(Game game) : base(game)
        {
            ((TouchInputHandler)this.Controller.Input).TouchUpdate += TouchPacMan_TouchUpdate;
        }

        private void TouchPacMan_TouchUpdate(TouchLocation touch)
        {
            if(this.LocationRect.Contains(touch.Position))
            {
                this.Location = touch.Position;
            }
        }

        protected override void SetupIPlayerController(Game game)
        {
            //base.SetupIPlayerController(game);
            this.Controller = new PlayerController(game);
        }
    }
}
