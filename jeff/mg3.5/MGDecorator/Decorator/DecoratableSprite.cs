using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGDecorator.Decorator
{
    class DecoratableSprite : DrawableSprite
    {

        SpriteDecorator decorator;

        public DecoratableSprite(Game game) : base(game)
        {
            decorator = new EmptySpriteDecorator(game);
        }

        public virtual void AddDecorator(SpriteDecorator spriteDecorator)
        {
            if(this.decorator is EmptySpriteDecorator)
            {
                this.decorator = spriteDecorator;
                
                
            }
            else
            {
                this.decorator.AddDecorator(spriteDecorator);
            }
        }

        public virtual void RemoveDecorator(SpriteDecorator spriteDecorator)
        {
            if (this.decorator is EmptySpriteDecorator)
            {
                //nothing
            }
            else
            {
                if (this.decorator.GetType() == spriteDecorator.GetType())
                {
                    this.decorator = this.decorator.decorator;
                }
                else
                {
                    this.decorator.RemoveDecorator(spriteDecorator);
                }

            }
        }

        public virtual bool HasDecorator(SpriteDecorator spriteDecorator)
        {
            return this.decorator.HasDecorator(spriteDecorator);
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

        public virtual void PreUpdate(GameTime gameTime)
        {

        }

        public virtual void Init(GameTime gameTime)
        {

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
