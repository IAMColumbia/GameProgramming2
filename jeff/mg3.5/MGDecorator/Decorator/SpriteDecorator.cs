using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGDecorator.Decorator
{

    public interface ISpriteDecorator
    {
        void Initialize();

        void Update(GameTime gameTime);
    }

    public abstract class SpriteDecorator : DrawableGameComponent, ISpriteDecorator
    {
        public SpriteDecorator decorator;
        
        public SpriteDecorator(Game game) : base(game)
        {
            if (this is EmptySpriteDecorator)
                this.decorator = null; //Hack maybeit better to just use null object pattern
            else
                decorator = new EmptySpriteDecorator(game);
            
        }

        public override void Initialize()
        {
            base.Initialize();
            if (decorator is EmptySpriteDecorator)
            {
                return; //end nothing else to do
            }
            decorator.Initialize();
        }

        internal virtual void AddDecorator(SpriteDecorator spriteDecorator)
        {
            if (this.decorator is EmptySpriteDecorator)
            {
                this.decorator = spriteDecorator;
                //this.decorator.Initialize();
            }
            else
            {
                this.decorator.AddDecorator(spriteDecorator);
            }
        }

        internal virtual void RemoveDecorator(SpriteDecorator spriteDecorator)
        {
            if(this.decorator is EmptySpriteDecorator)
            {
                //nothing
            }
            else
            {
                if(this.decorator.GetType() == spriteDecorator.GetType())
                {
                    this.decorator = this.decorator.decorator;
                }
                else
                {
                    this.decorator.RemoveDecorator(spriteDecorator);
                }
                
            }
        }

        internal virtual bool HasDecorator(SpriteDecorator spriteDecorator)
        {
            if (this is EmptySpriteDecorator) return false;
            if (this.GetType() == spriteDecorator.GetType())
                return true;
            return this.decorator.HasDecorator(spriteDecorator);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (decorator is EmptySpriteDecorator)
            {
                return; //end nothing else to do
            }
            decorator.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (decorator is EmptySpriteDecorator)
            {
                base.Draw(gameTime);
                return; //end nothing else to do
            }
            decorator.Draw(gameTime); //Draw behind base
            base.Draw(gameTime);
        }

    }
}
