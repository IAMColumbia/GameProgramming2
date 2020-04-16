using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;

namespace MGTouch
{
    internal class VirtualThumbstick : DrawableGameComponent
    {

        Texture2D startTex, deltaTex;
        SpriteBatch sb;

        GameConsole console;

        public VirtualThumbstick(Game game) : base(game)
        {
            points = new Vector2[0];
            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(this.Game);
                this.Game.Components.Add(console);
            }
        }

        public Vector2 RawDirection { get; set; }
        public Vector2 Direction { get; set; }

        public int Threshold { get; set; }

        private Vector2 locationStart;
        public Vector2 LocationStart
        {
            get { return locationStart; }
            set
            {
                locationStart = value;
                if (locationStart != null && locationStart != Vector2.Zero)
                {
                    this.Enabled = true;
                    this.Visible = true;
                }
                else
                {
                    //Clear location start
                    this.Enabled = false;
                    this.Visible = false;
                    this.LocationDelta = Vector2.Zero;
                    points = new Vector2[0];
                }
            }
        }
        public Vector2 LocationDelta { get; set; }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            startTex = this.Game.Content.Load<Texture2D>("SpriteMarker");
            deltaTex = startTex;
            this.Threshold = 50; //default threshold
            base.LoadContent();
        }

        int pointArraySize;
        float dist;
        const int pointDist = 25;
        Vector2[] points;
        Vector2 nextPoint;
        public override void Update(GameTime gameTime)
        {
            
            if (LocationDelta != Vector2.Zero)
            {
                dist = Vector2.Distance(locationStart, LocationDelta);
                //console.Log($"start:", locationStart.ToString());
                //console.Log($"Delta:", LocationDelta.ToString());
                //console.Log($"dist:", dist.ToString());
                
                pointArraySize = (int)dist / pointDist;
                //console.Log($"pointArraySize:", pointArraySize.ToString());
                points = new Vector2[pointArraySize];
                for (int i = 0; i < points.Length - 1; i++)
                {
                    nextPoint = Vector2.Lerp(LocationStart, LocationDelta, (float)i / (float)pointArraySize);
                    //console.Log($"lerp {i}:", (pointArraySize / (i + 1));
                    points[i] = nextPoint;
                    //console.Log($"pointArraySize {i} {pointArraySize}:", ((float)i / (float)pointArraySize).ToString()); 
                }
            }
            this.RawDirection = locationStart - LocationDelta;
            this.Direction = Vector2.Normalize(RawDirection);
            console.Log($"direction:", this.Direction.ToString());
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            if (LocationStart != null)
            {
                sb.Draw(startTex, LocationStart, Color.White);
            }
            if (LocationDelta != null)
            {
                sb.Draw(deltaTex, LocationDelta, Color.Blue);
                for (int i = 0; i < points.Length - 1; i++)
                {
                    sb.Draw(deltaTex, points[i], Color.Red);
                }
            }
            sb.End();
            base.Draw(gameTime);
        }
    }
}