using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Util;
using MonoGameLibrary.State;
using MGScreenStrategy.GameStates;

namespace Screenz
{
    public interface ITitleIntroState : IGameState { }
    
    public sealed class TitleIntroState : BaseGameState, ITitleIntroState
    {
        private Texture2D texture;

        public TitleIntroState(Game game, IGameStateManager manager)
            : base(game, manager)
        {
            game.Services.AddService(typeof(ITitleIntroState), this);
        }

        public override void Update(GameTime gameTime)
        {
            if (input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                this.Game.Exit();

            //Startbutton or enter
            if (input.WasPressed(0, InputHandler.ButtonType.Start, Keys.Enter))
            {
                // push our start menu onto the stack
                GameManager.PushState(((ScreenStrategyGameStateManager)(this.GamestateManager)).StartMenuState.Value);
            }

            //Start with spacebar
            if(input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                // push our start menu onto the stack
                GameManager.PushState(((ScreenStrategyGameStateManager)(this.GamestateManager)).StartMenuState.Value);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.Game.GraphicsDevice.Clear(Color.Black);
            Vector2 pos = new Vector2(100, 100);
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(texture, pos, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
            
        }

        protected override void LoadContent()
        {
            texture = this.Game.Content.Load<Texture2D>(@"titleIntro");

            base.LoadContent();
        }
    }
}
