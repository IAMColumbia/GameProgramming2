using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGDecorator
{
    class DecoDemoPacMan :  DecoMonogamePacMan
    {

        ScaleDecorator scaleDeco;
        SineScaleDecorator sineDeco;
        DropShadowDecorator smallDrop;
        DropShadowDecorator bigDrop;

        GameConsole console;

        public DecoDemoPacMan(Game game) : base(game)
        {
            scaleDeco = new ScaleDecorator(game, this, 2);
            sineDeco = new SineScaleDecorator(game, this, 2);
            smallDrop = new DropShadowDecorator(game, this, new Vector2(1, 1));
            bigDrop = new DropShadowDecorator(game, this, new Vector2(-3, 3));

            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(this.Game);
                this.Game.Components.Add(console);
            }
                
        }

        public override void Initialize()
        {
            scaleDeco.Initialize();
            sineDeco.Initialize();
            smallDrop.Initialize();
            bigDrop.Initialize();

           
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            this.console.Log("Deco press a", "to add scale" + this.HasDecorator(scaleDeco));
            this.console.Log("Deco press s", "to add  a small drop" );
            this.console.Log("Deco press f", "to add sine" + this.HasDecorator(sineDeco));
            this.console.Log("Deco press drop:",  this.HasDecorator(smallDrop).ToString());
            
            this.console.Log("message1", "scale and sine work together");
            this.console.Log("message2", "the other two dont since they are the same");

            //A key
            if (controller.Input.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.A))
            {
                if(this.HasDecorator(scaleDeco)) { this.RemoveDecorator(scaleDeco); }
                else { this.AddDecorator(scaleDeco); }

            }

            //S key
            if (controller.Input.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.S))
            {
                if (this.HasDecorator(smallDrop)) { this.RemoveDecorator(smallDrop); }
                else { this.AddDecorator(smallDrop); }

            }

            //D key
            if (controller.Input.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.D))
            {
                if (this.HasDecorator(bigDrop)) { this.RemoveDecorator(bigDrop); }
                else { this.AddDecorator(bigDrop); }

            }

            //F key
            if (controller.Input.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.F))
            {
                if (this.HasDecorator(sineDeco)) { this.RemoveDecorator(sineDeco); }
                else { this.AddDecorator(sineDeco); }

            }

            base.Update(gameTime);

        }

        protected override void LoadContent()
        {
            
            base.LoadContent();

        }
    }
}
