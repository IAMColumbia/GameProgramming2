using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.GameFiles;
using System.IO;
using System;
using System.Collections.Generic;
using MonoGameLibrary.Util;
using MGPacManComponents.Pac;

namespace SingletonFilesystem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameConsole console;

        MonogamePacMan pac;
        FoodManagerLoadFromText fm;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            console = new GameConsole(this);
            this.Components.Add(console);

            pac = new MonogamePacMan(this);
            this.Components.Add(pac);

            FileSystem.Instance.Path = "";
            fm = new FoodManagerLoadFromText(this, pac, ".\\Content\\Level1.txt");
            this.Components.Add(fm);

            CreateKeyMap();
        }

        private void CreateKeyMap()
        {
            FileSystem.Instance.Path = "";
            FileSystem.Instance.CreateTextFile("KeyMap.txt", InitKeyMap());
        }

        private string InitKeyMap()
        {
            Dictionary<string, string> keyMap = new Dictionary<string, string>();
            keyMap.Add(Keys.W.ToString(), "Move Up");
            keyMap.Add(Keys.Up.ToString(), "Move Up");
            keyMap.Add(Keys.S.ToString(), "Move Down");
            keyMap.Add(Keys.Down.ToString(), "Move Down");
            keyMap.Add(Keys.A.ToString(), "Move Left");
            keyMap.Add(Keys.Left.ToString(), "Move Left");
            keyMap.Add(Keys.D.ToString(), "Move Right");
            keyMap.Add(Keys.Right.ToString(), "Move Right");
            keyMap.Add(Keys.Z.ToString(), "Undo");

            string txt = "";

            foreach(var pair in keyMap)
            {
                txt += string.Format("{0}\t{1}", pair.Key, pair.Value);
                txt += "\n";
            }
            return txt;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
