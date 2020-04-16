using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Particle;
using MonoGameLibrary.Util;

namespace ObjectPoolPartcles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game Sevices
        InputHandler input;
        GameConsole console;
        FPS fps;

        Vector2 mouseLoc; //mouse location

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            input = new InputHandler(this);
            this.Components.Add(input);

            console = new GameConsole(this);
            this.Components.Add(console);

            fps = new FPS(this);
            this.Components.Add(fps);
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

            // TODO: use this.Content to load your game content here

            Texture2D ghostHit = Content.Load<Texture2D>("GhostHit");

            //Mouse Particles
            ParticleManager.Instance().ParticleSystems.Add("mouse",
                new ParticleSystem(10, 20,//min max num particles
                    ghostHit,
                    1, 2,  //Initial Speed min,max
                    1, 3,  //Inital Accel min,max
                    1, 10, //Inital Rotation min, max
                    5.5f, 10.0f, //min max Life in seconds
                    0.2f, 0.7f, //starting Scale ending scale
                    10  //max spawn effects
                 )
            );
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

            //Mouse Particles
            mouseLoc = new Vector2(input.MouseState.X, input.MouseState.Y);
            console.Log("mouseLoc", mouseLoc.ToString() + " " + GraphicsDevice.Viewport.Width + " " + GraphicsDevice.Viewport.Height);
            //if mouse is on screen
            if (mouseLoc.X >= 0 
                && mouseLoc.X <= GraphicsDevice.Viewport.Width
                && mouseLoc.Y >= 0
                && mouseLoc.Y <= GraphicsDevice.Viewport.Height
                && input.MouseState.LeftButton == ButtonState.Pressed //Left mouse button down
            )
            {

                ParticleManager.Instance().ParticleSystems["mouse"].AddParticles(
                                  mouseLoc);
                                  //Vector2.Zero);      //same and not setting direction but slower
                                   //input.MouseDelta *-1); //shots out behind or in front by mouse direction
            }
            ParticleManager.Instance().ParticleSystems["mouse"].Update(gameTime);
            console.Log("Particle Max Count", ParticleManager.GetMaxCount("mouse").ToString());
            console.Log("Particle Active Count", ParticleManager.GetActiveCount("mouse").ToString());
            console.Log("Particle Systems", ParticleManager.Instance().ParticleSystems.Count.ToString());
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            ParticleManager.Instance().Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
