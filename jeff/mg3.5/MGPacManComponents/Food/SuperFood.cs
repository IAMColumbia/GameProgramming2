using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPacManComponents.Food
{

    // A delegate type for hooking up change notifications.
    public delegate void FoodHitEventHandler(object sender, EventArgs e);

    public enum SuperFoodState { Normal, Activated }

    public class SuperFood : Food
    {

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event FoodHitEventHandler FoodHitTimeOut;
        System.Timers.Timer foodTimer = new System.Timers.Timer(5000);



        protected SuperFoodState state;
        public SuperFoodState State
        {
            protected set { state = value; }
            get { return state; }
        }
        
        
        public SuperFood(Game game) : base(game)
        {
            //TODO remove system timer use game loop to time
            // This creates a new timer that will fire every second (1000 milliseconds)
            foodTimer.Elapsed += new System.Timers.ElapsedEventHandler(foodTimer_Elapsed);
            this.state = SuperFoodState.Normal;
        }

        // Invoke the FoodHitHit event;
        public virtual void OnFoodHitTimeOut(EventArgs e)
        {
            if (FoodHitTimeOut != null)
                FoodHitTimeOut(this, e);
        }

        void foodTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Visible = true;
            this.Enabled = true;
            this.state = SuperFoodState.Normal;
            Food.EatenCount--;
            //No more powerfoods eaten
            //if (EatenCount == 0)
            //{
                this.OnFoodHitTimeOut(EventArgs.Empty);
            //}
        }

        public override void Initialize()
        {
            base.Initialize();
            this.Scale = 1.0f;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteTexture = this.Game.Content.Load<Texture2D>("20px_1trans");
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }

        public override void Hit()
        {
            foodTimer.Start();
            base.Hit();
            this.Enabled = true; //Food disables but we need superfoods to stay
            this.state = SuperFoodState.Activated;
        }
    }
}
