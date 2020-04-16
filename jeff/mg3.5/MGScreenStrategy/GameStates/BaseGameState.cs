
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using MonoGameLibrary;
using MonoGameLibrary.State;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;
#endregion

namespace Screenz
{
    public partial class BaseGameState : GameState
    {

        public SpriteBatch SpriteBatch;
        public IGameStateManager GamestateManager;

        protected InputHandler input;
        protected GameConsole console;

        public BaseGameState(Game game, IGameStateManager manager)
            : base(game)
        {
            this.GamestateManager = manager;
            input = (InputHandler)game.Services.GetService<IInputHandler>();
            if(input == null)
            {
                input = new InputHandler(game);
                game.Components.Add(input);
            }

            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(game);
                this.Game.Components.Add(console);
            }
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            base.LoadContent();
        }
    }
}


