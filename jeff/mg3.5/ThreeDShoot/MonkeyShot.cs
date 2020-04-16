using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.ThreeD;

namespace ThreeDShoot
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MonkeyShot : Mesh
    {   
        public MonkeyShot(Game game)
            : base(game, "monkey")
        {
            // TODO: Construct any child components here
            
        }   
    }
}