using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Util;
using MonoGameLibrary.State;
using MGScreenStrategy.GameStates;

namespace Screenz
{
    public interface IStartMenuState : IGameState { }
    
    public sealed class StartMenuState : BaseGameState, IStartMenuState
    {
        private SpriteFont font;

        public StartMenuState(Game game, IGameStateManager manager)
            : base(game, manager)
        {
            game.Services.AddService(typeof(IStartMenuState), this);
        }

        public override void Update(GameTime gameTime)
        {
            
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                GameManager.ChangeState(((ScreenStrategyGameStateManager)(this.GamestateManager)).TitleIntroState.Value); //go back to title / intro screen

            if (Input.WasPressed(0, InputHandler.ButtonType.Start, Keys.Enter))
            {
                GameManager.PopState(); //got here from our playing state, just pop myself off the stack
                GameManager.ChangeState(((ScreenStrategyGameStateManager)(this.GamestateManager)).PlayingState.Value); //go back to title / intro screen
               
                
               
            }

            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.DrawString(font, "Move PacMan with the arrow keys.", new Vector2(100,100),Color.BlanchedAlmond);
            this.SpriteBatch.DrawString(font, "Avoid the Ghosts.", new Vector2(100, 150), Color.BlanchedAlmond);
            this.SpriteBatch.DrawString(font, "Press ESC to pause", new Vector2(100, 200), Color.BlanchedAlmond);
            this.SpriteBatch.DrawString(font, "Press enter to start", new Vector2(100, 500), Color.BlanchedAlmond);
            this.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);

            if (GameManager.State != this.Value)
                Visible = true;
        }

        protected override void LoadContent()
        {
            font = this.Game.Content.Load<SpriteFont>("Arial");
            base.LoadContent();
        }
    }
}
