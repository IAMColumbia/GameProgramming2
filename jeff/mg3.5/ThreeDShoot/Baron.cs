#region File Description
//-----------------------------------------------------------------------------
// Dog.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.ThreeD;
#endregion

namespace ThreeDShoot
{
    class Baron : QuadSprite 
    {
        #region Fields

        // How long until we should start or stop the sound.
        TimeSpan timeDelay = TimeSpan.Zero;

        // The sound which is currently playing, if any.
        //Cue activeCue = null;

        #endregion


        public Baron(Game game)
            : base(game)
        {
            this.Scale = 5;
            
            
        }

        protected override void LoadContent()
        {
            this.Texture = content.Load<Texture2D>("baronWalkFront01");
            base.LoadContent();
        }


        /// <summary>
        /// Updates the position of the dog, and plays sounds.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Set the entity to a fixed position.
            Position = new Vector3(0, 5, -50);
            Forward = Vector3.Forward;
            Up = Vector3.Up;
            Velocity = Vector3.Zero;

           
            timeDelay -= gameTime.ElapsedGameTime;
            base.Update(gameTime);
        }
    }
}
