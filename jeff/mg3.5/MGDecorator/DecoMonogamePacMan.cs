using MGDecorator.Decorator;
using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.GameComponents.Player;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGDecorator
{
    class DecoMonogamePacMan : DecoratableSprite
    {
        protected PlayerController controller { get; private set; }
        internal GameConsolePacMan PacMan
        {
            get;
            private set;
        }
        protected PacManState pacState;
        public PacManState PacState
        {
            get { return this.pacState; }
            //Change pacman state also
            set
            {
                if (this.pacState != value)
                {
                    this.pacState = this.PacMan.State = value; //change PacMan state that is encasulated
                    this.pacStateChanged();
                }
            }
        }

        /// <summary>
        /// Hook to allow child classes to recieve state change calls
        /// </summary>
        protected virtual void pacStateChanged()
        {
            //nothing yet
        }

        public DecoMonogamePacMan(Game game)
            : base(game)
        {
            this.controller = new PlayerController(game);
            PacMan = new GameConsolePacMan((GameConsole)game.Services.GetService<IGameConsole>());
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("pacManSingle");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            this.Location = new Microsoft.Xna.Framework.Vector2(100, 100);
            this.Speed = 200;
            this.pacState = PacManState.Chomping;
        }

        public override void Update(GameTime gameTime)
        {
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            UpdatePacManWithController(gameTime, time);

            UpdateKeepPacManOnScreen();



            base.Update(gameTime);
        }

        public virtual void Die()
        {
            this.PacMan.Die();
        }

        protected virtual void UpdatePacManWithController(GameTime gameTime, float time)
        {
            this.controller.Update(gameTime);

            this.Location += ((this.controller.Direction * (time / 1000)) * Speed);      //Simple Move 
            this.Rotate = this.controller.Rotate;

            //Change State based on movement
            if (this.controller.HasInputForMoverment)
            {
                if (pacState != PacManState.Spawning && pacState != PacManState.SuperPacMan)
                    this.PacState = PacManState.Chomping;
            }
            else
            {
                if (pacState != PacManState.Spawning && pacState != PacManState.SuperPacMan)
                    this.PacState = PacManState.Still;
            }
        }

        protected void UpdateKeepPacManOnScreen()
        {
            //Keep PacMan On Screen
            if (this.Location.X > Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2))
            {
                this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2);
            }
            if (this.Location.X < (this.spriteTexture.Width / 2))
                this.Location.X = (this.spriteTexture.Width / 2);

            if (this.Location.Y > Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2))
                this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2);

            if (this.Location.Y < (this.spriteTexture.Height / 2))
                this.Location.Y = (this.spriteTexture.Height / 2);
        }

        public virtual void PowerUp()
        {
            this.PacState = PacManState.SuperPacMan;
        }

        public virtual void EndPowerUp()
        {
            this.PacState = PacManState.EndSuperPacMan;
        }
    }
}
