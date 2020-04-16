using MGDecorator.Decorator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGDecorator
{

    

    class DropShadowDecorator : SpriteDecorator
    {

        SpriteBatch sb;
        Sprite sprite;

        Vector2 direction;

        public DropShadowDecorator(Game game, Sprite sprite, Vector2 direction) : base(game)
        {
            this.sprite = sprite;
            //this.direction = new Vector2(-1, 1);
            this.direction = direction;
        }

        

        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            base.LoadContent();
        }

        internal override void AddDecorator(SpriteDecorator spriteDecorator)
        {
            this.LoadContent();
            base.AddDecorator(spriteDecorator);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            this.DrawShadow(sb, this.sprite);
            sb.End();
            base.Draw(gameTime);
        }

        void DrawShadow(SpriteBatch sb, Sprite sprite)
        {
            sb.Draw(sprite.SpriteTexture,
               new Rectangle(sprite.Rectagle.X + ((int)direction.X * 2), sprite.Rectagle.Y + ((int)direction.Y * 2), sprite.Rectagle.Width, sprite.Rectagle.Height),
               null,
               Color.FromNonPremultiplied(112, 112, 112, 50),
               MathHelper.ToRadians(sprite.Rotate),
               sprite.Origin,
               sprite.SpriteEffects,
               0);

            sb.Draw(sprite.SpriteTexture,
               new Rectangle(sprite.Rectagle.X + ((int)direction.X * 1), sprite.Rectagle.Y + ((int)direction.Y * 1), sprite.Rectagle.Width, sprite.Rectagle.Height),
               null,
               Color.FromNonPremultiplied(35, 35, 35, 75),
               MathHelper.ToRadians(sprite.Rotate),
               sprite.Origin,
               sprite.SpriteEffects,
               0);



        }
    }
}
