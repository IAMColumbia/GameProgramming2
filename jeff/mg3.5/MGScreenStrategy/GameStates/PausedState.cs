using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.State;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework.Input;

namespace Screenz
{
    public interface IPausedState : IGameState { }

    class PausedState : BaseGameState, IPausedState
    {
        
        private Texture2D pausedTexture;
        private SpriteFont font;

        public PausedState(Game game, IGameStateManager manager)
            : base(game, manager)
        {
            game.Services.AddService(typeof(IPausedState), this);
        }
        protected override void LoadContent()
        {
            this.pausedTexture = this.Game.Content.Load<Texture2D>(@"Transparent25Percent");
            this.font = this.Game.Content.Load<SpriteFont>("Arial");
            base.LoadContent();

        }

         public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                GameManager.PopState();            
            //TODO add pausedstate
            
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Rectangle fullscreen = new Rectangle(0, 0, this.Game.Window.ClientBounds.Width, this.Game.Window.ClientBounds.Height);
            SpriteBatch.Begin();
            this.SpriteBatch.Draw(pausedTexture, fullscreen, Color.Black);
            this.SpriteBatch.DrawString(font, "Paused Press Esc to resume", new Vector2(100, 150), Color.BlanchedAlmond);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
