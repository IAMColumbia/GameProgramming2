using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyPacMan.weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManWeaponsStrategy
{
    class PacManWWeaponShoot : PacManWWeapon
    {
        
        public RateLimitedShotManager SM;
        protected Vector2 lastDirection; //Direction for shooting

        public PacManWWeaponShoot(Game game) : base(game)
        {
            SM = new RateLimitedShotManager(this.Game);
            SM.LimitShotRate = .1f;
            SM.MaxShots = 3;
            this.Game.Components.Add(SM);
            
        }

        public override void Update(GameTime gameTime)
        {
            //Shoot on Space or B
            if ((this.Controller.Input.KeyboardState.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.Space))
                || (this.Controller.Input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.B)))
            {
                AddShot();
            }
            

            if (this.Controller.Direction.Length() > 0)
            {
                this.lastDirection = this.Controller.Direction;
                //console.Log("PacMan.lastDirection", this.lastDirection.ToString());
            }

            base.Update(gameTime);
        }

        private void AddShot()
        {
            Shot s = new Shot(this.Game);
            s.Location = this.Location;
            s.Direction = this.lastDirection;
            s.Speed = 600;
            SM.Shoot(s);
        }

        public override void Draw(SpriteBatch sb)
        {
            SM.Draw(sb);
            base.Draw(sb);
        }

        internal override void GiveWeapon(foodWeapon foodWeapon)
        {
            base.GiveWeapon(foodWeapon);
            
            //Change Max shoots based on the weapon picked up
            switch (foodWeapon.Name)
            {
                case "no weapon":
                    this.SM.MaxShots = 1;
                    
                    break;
                case "red weapon":
                    this.SM.MaxShots = 3;

                    break;
                case "teal weapon":
                    this.SM.MaxShots = 15;
                    break;
            }
        }

    }
}
