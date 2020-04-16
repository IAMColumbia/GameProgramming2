using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
using MonoGameLibrary.ThreeD;
using MonoGameLibrary.Util;

namespace ThreeDShoot
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MonkeyShots : Microsoft.Xna.Framework.GameComponent
    {

        List<MonkeyShot> Shots;
        InputHandler input;
        Game game;
        Camera camera;
        
        public MonkeyShots(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.Shots = new List<MonkeyShot>();
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            camera = (Camera)Game.Services.GetService(typeof(ICamera));
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            
            //remove shots

            //add shots
            if(input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                MonkeyShot ms = new MonkeyShot(game);
                ms.Location = camera.Target;
                ms.Direction = Vector3.Normalize(camera.TransformedReference);

                ms.Direction = Vector3.Multiply(ms.Direction, 10.0f);
                ms.Initialize();
                
                Shots.Add(ms);
            }

            //update shots
            foreach (MonkeyShot m in Shots)
            {
                m.Update(gameTime);
            }
            
            base.Update(gameTime);
        }

        public void DrawShots(GameTime gameTime)
        {
            foreach (MonkeyShot m in Shots)
            {
                m.Draw(gameTime);
            }
        }
    }
}