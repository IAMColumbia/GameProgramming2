using MGDecorator.Decorator;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGDecorator
{
    class SineScaleDecorator : ScaleDecorator
    {

        float sineTime;
        GameConsole console;

        public SineScaleDecorator(Game game, Sprite sprite, float scale) : base(game, sprite, scale)
        {
            this.sprite = sprite;
            this.scaleMultiplier = scale;
            console = (GameConsole)game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(game);
                game.Components.Add(console);
            }
        }

        public override void Update(GameTime gameTime)
        {
            sineTime += gameTime.ElapsedGameTime.Milliseconds;
            this.scaleMultiplier = this.scaleMultiplier + (float)Math.Cos(sineTime);
            //console.Log("sineTime", sineTime.ToString());
            //console.Log("scaleMultiplier", scaleMultiplier.ToString());
            base.Update(gameTime);
        }
    }
}
