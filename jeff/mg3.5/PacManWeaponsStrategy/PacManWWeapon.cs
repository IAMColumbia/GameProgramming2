using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGPacManComponents.Pac;
using StrategyPacMan.weapons;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacManWeaponsStrategy
{
    public class PacManWWeapon : MonogamePacMan
    {
        foodWeapon pacWeapon;

        public PacManWWeapon(Game game)
            : base(game)
        {
            this.pacWeapon = new NoWeapon(game);
        }


        public override void Draw(SpriteBatch sb)
        {
            //base.Draw(sb);

            sb.Draw(spriteTexture,
                new Rectangle(
                    (int)Location.X,
                    (int)Location.Y,
                    (int)(spriteTexture.Width * this.Scale),
                    (int)(spriteTexture.Height * this.Scale)),
                null,
                this.pacWeapon.color,
                MathHelper.ToRadians(Rotate),
                this.Origin,
                SpriteEffects,
                0);


            DrawMarkers(sb);
        }

        

        internal virtual void GiveWeapon(foodWeapon foodWeapon)
        {
            this.pacWeapon = foodWeapon;
            
        }
    }
}
