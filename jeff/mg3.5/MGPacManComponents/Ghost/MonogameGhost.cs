using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPacManComponents.Ghost
{
    /// <summary>
    /// One day I'll refactor ghost like the pacman class
    /// </summary>
    public class MonogameGhost : DrawableSprite
    {

        //Ghost class included via composition
        protected GameConsoleGhost ghost;
        public GameConsoleGhost Ghost
        {
            get { return this.ghost; }
            protected set { this.ghost = value; }
        }

        public Texture2D ghostHit;
        public Texture2D ghostTexture;
        public string strGhostTexture;

        MonogamePacMan pacMan;
        
        public Vector2 StartLoc;

        Random r;
        float turnAmount; //turn anount for chasing pacman

        bool initalized; //make sure we don't call initialize twice unless intended

        public MonogameGhost(Game game, MonogamePacMan pacman)
            : base(game)
        {
            // TODO: Construct any child components here
            r = new Random();
            this.pacMan = pacman;
            //pacMan.Attach(this);
            this.ghost = new GameConsoleGhost();
            this.ghost.gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            if (this.ghost.gameConsole == null)
            {
                this.ghost.gameConsole = new GameConsole(this.Game);
                this.Game.Components.Add(this.ghost.gameConsole);
            }
            
            strGhostTexture = "RedGhost";
            StartLoc = new Vector2(50, 50);
            this.ghost.State = GhostState.Roving;
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: Add your initialization code here
            if (!initalized)
            {
                this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
                this.Location = this.GetRandLocation();
                initalized = true;
            }
        }

        protected override void LoadContent()
        {

            this.ghostTexture = this.Game.Content.Load<Texture2D>(strGhostTexture);
            this.ghostHit = this.Game.Content.Load<Texture2D>("GhostHit");

            this.spriteTexture = ghostTexture;
            this.Direction = new Vector2(0, 1);
            this.Location = StartLoc;
            this.Speed = 50.0f;
            this.turnAmount = .04f;
            base.LoadContent();

            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);

        }


        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
       
            switch (this.ghost.State)
            {
                case GhostState.Dead:
                    UpdateGhostDeadState();
                    break;
                case GhostState.Evading:
                    ChangeGhostTectureToBlue();

                    if ((this.Location - pacMan.Location).Length() < 100)
                    {
                        UpdateGhostEvading();
                    }
                    else
                    {
                        //UpdateGhostRoving();
                    }
                    break;

                case GhostState.Chasing:
                    ChangeGhostTextureToNormal();
                    UpadateGhostChasingState(turnAmount);
                    break;
                
                case GhostState.Roving:
                    UpdateGhostRoving();

                    break;
            }

            UpdateGhostKeepOnScreen();

            Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move

            UpdateGhostCollision();

            base.Update(gameTime);
        }

        private void ChangeGhostTextureToNormal()
        {
            //Change texture if Chasing
            if (!(this.spriteTexture == this.ghostTexture))
            {
                //gameConsole.GameConsoleWrite(this.ToString() + " Roving changed texture to spriteTexture");
                this.spriteTexture = this.ghostTexture;
            }
        }

        private void ChangeGhostTectureToBlue()
        {
            //Change texture if evading
            if (this.spriteTexture != this.ghostHit)
            {
                //gameConsole.GameConsoleWrite(this.ToString() + " Evading changed texture to ghostHit");
                this.spriteTexture = this.ghostHit;
            }
        }


        Vector2 normD, p;
        private void UpdateGhostRoving()
        {
            //If Ghost is stopped move on
            if (this.Direction.Length() == 0) return;
            //check if ghost can see pacman
            normD = Vector2.Normalize(this.Direction);
            
            p = new Vector2(this.Location.X, this.Location.Y);
            while (p.X < this.Game.GraphicsDevice.Viewport.Width &&
                  p.X > 0 &&
                  p.Y < this.Game.GraphicsDevice.Viewport.Height &&
                  p.Y > 0)
            {
                if (pacMan.LocationRect.Contains(new Point((int)p.X, (int)p.Y)))
                {
                    this.ghost.State = GhostState.Chasing;
                    this.ghost.gameConsole.GameConsoleWrite(this.ToString() + " saw pacman");
                    break;
                }
                p += this.Direction;
            }
        }

        private void UpdateGhostEvading()
        {

            if (pacMan.Location.Y > this.Location.Y)
            {
                this.Direction.Y = -1;
            }
            else
            {
                this.Direction.Y = 1;
            }
            if (pacMan.Location.X > this.Location.X)
            {
                this.Direction.X = -1;
            }
            else
            {
                this.Direction.X = -1;
            }
        }

        private void UpadateGhostChasingState(float turnAmount)
        {
            //Change texture if Chasing
            if (!(this.spriteTexture == this.ghostTexture))
            {
                //gameConsole.GameConsoleWrite(this.ToString() + " Chasing changed texture to spriteTexture");
                this.spriteTexture = this.ghostTexture;
            }
            if (pacMan.Location.Y > this.Location.Y)
            {
                //this.Direction.Y = 1;
                this.Direction.Y = MathHelper.Clamp(this.Direction.Y += turnAmount, -1, 1);
            }
            else
            {
                //this.Direction.Y = -1;
                this.Direction.Y = MathHelper.Clamp(this.Direction.Y -= turnAmount, -1, 1);
            }
            if (pacMan.Location.X > this.Location.X)
            {
                //this.Direction.X = 1;
                this.Direction.X = MathHelper.Clamp(this.Direction.X += turnAmount, -1, 1);
            }
            else
            {
                //this.Direction.X = -1;
                this.Direction.X = MathHelper.Clamp(this.Direction.X -= turnAmount, -1, 1);
            }
        }

        /// <summary>
        /// Updates Ghost Dead State
        /// </summary>
        protected virtual void UpdateGhostDeadState()
        {
            //TODO Dead moving and dead animation.
            //Until then
            this.ghost.State = GhostState.Roving;
            //Pick random direction
            Random r = new Random();
            Vector2 v = new Vector2((float)r.NextDouble() - 0.5f, (float)r.NextDouble() - 0.5f);
            Vector2.Normalize(ref v, out v);    //Normalize
            this.Direction = v;                 //Assign random direction
        }

        protected virtual void UpdateGhostCollision()
        {
            //Collision
            if (this.Intersects(pacMan))
            {

                //PerPixel Collision
                if (this.PerPixelCollision(pacMan))
                {
                    if (this.ghost.State == GhostState.Evading)
                    {
                        //Ghost Die
                        this.Die();
                    }
                    else
                    {
                        if (pacMan.PacMan.State != PacManState.Dying)
                        {
                            pacMan.Die();
                            this.Location = new Vector2(100, 100);
                            this.ghost.State = GhostState.Roving;
                        }
                    }
                }
            }
        }

        protected virtual void Die()
        {
            //this.Visible = false;
            //this.Enabled = false;
            this.ghost.State = GhostState.Dead;
        }

        private void UpdateGhostKeepOnScreen()
        {
            //Borders
            if ((this.Location.Y + this.spriteTexture.Height / 2 > this.Game.GraphicsDevice.Viewport.Height)
                ||
                (this.Location.Y - this.spriteTexture.Height / 2 < 0)
                )
            {
                this.Direction.Y *= -1;
                //this.ghostState = GhostState.Roving;
            }
            if ((this.Location.X + this.spriteTexture.Width / 2 > this.Game.GraphicsDevice.Viewport.Width)
                ||
                (this.Location.X - this.spriteTexture.Width / 2 < 0)
                )
            {
                this.Direction.X *= -1;
                //this.ghostState = GhostState.Roving;
            }
        }

        public void Evade()
        {
            this.ghost.State = GhostState.Evading;
        }

        private Vector2 GetRandLocation()
        {
            Vector2 loc;
            loc.X = r.Next(Game.Window.ClientBounds.Width);
            loc.Y = r.Next(Game.Window.ClientBounds.Height);
            return loc;
        }

        


    }

    
}
