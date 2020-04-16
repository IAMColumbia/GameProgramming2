using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGPacManComponents.Food;
using MGPacManComponents.Pac;

namespace MGObserver
{
    class FoodManager : DrawableGameComponent
    {

        List<Food> Foods;
        List<SuperFood> SuperFoods;

        List<Food> foodsToRemove;

        MonogamePacMan pac;

        public FoodManager(Game game, MonogamePacMan pac) : base(game)
        {
            this.Foods = new List<Food>();
            this.SuperFoods = new List<SuperFood>();
            this.foodsToRemove = new List<Food>();
            this.pac = pac;
        }

        public override void Initialize()
        {
            base.Initialize();
            Food f = new Food(this.Game);
            f.Initialize();
            f.Location = new Vector2(150, 100);
            Foods.Add(f);
            

            SuperFood sf = new SuperFood(this.Game);
            sf.Initialize();
            sf.Location = new Vector2(400, 400);
            SuperFoods.Add(sf);
            
        }

        public override void Update(GameTime gameTime)
        {

            UpdateFoodAndSupeFoodCollision();
            UpdateFoodAndSuperFood(gameTime);
            
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            DrawFoodAndSuperFood(gameTime);
            base.Draw(gameTime);
        }

        private void DrawFoodAndSuperFood(GameTime gameTime)
        {
            foreach (SuperFood f in SuperFoods)
            {
                if (f.Visible)
                {
                    f.Draw(gameTime);
                }
            }
            foreach (Food f in Foods)
            {
                if (f.Visible)
                {
                    f.Draw(gameTime);
                }
            }
        }

        private void UpdateFoodAndSuperFood(GameTime gameTime)
        {
            foreach (SuperFood f in SuperFoods)
            {
                if(f.Enabled)
                {
                    f.Update(gameTime);
                }
            }
            foreach(Food f in Foods)
            {
                if (f.Enabled)
                {
                    f.Update(gameTime);
                }
            }
        }

        private void UpdateFoodAndSupeFoodCollision()
        {
            this.foodsToRemove.Clear();
            UpdateCollisionFood();
            UpdateCollisiaonSuperFood();
            if(this.foodsToRemove.Count > 0)
            {
                foreach(Food f in foodsToRemove)
                {
                    if (f is SuperFood)
                        SuperFoods.Remove((SuperFood)f);
                    else
                        Foods.Remove(f);
                }
            }
        }

        private void UpdateCollisiaonSuperFood()
        {
            foreach(SuperFood f in SuperFoods)
            {
                //Only collisde with unactivated super foods
                if (f.Intersects(pac) && f.State == SuperFoodState.Normal)
                {
                    if(f.PerPixelCollision(pac))
                    {
                        //hit SuperFood
                        f.Hit();
                        f.FoodHitTimeOut += F_FoodHitTimeOut;
                        //For this test super foods come back
                        pac.PowerUp();
                        
                    }
                }
            }
        }

        private void F_FoodHitTimeOut(object sender, EventArgs e)
        {
            pac.PacState = PacManState.EndSuperPacMan;
            ((SuperFood)sender).FoodHitTimeOut -= F_FoodHitTimeOut;
        }

        private void UpdateCollisionFood()
        {
            foreach (Food f in Foods)
            {
                if (f.Intersects(pac))
                {
                    if (f.PerPixelCollision(pac))
                    {
                        //hit Food
                        f.Hit();
                        this.Game.Components.Remove(f);
                        this.foodsToRemove.Add(f);
                    }
                }
            }
        }
    }
}
