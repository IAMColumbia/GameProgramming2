using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;

namespace MGCommand
{
    class CommandPacMan : MonogamePacMan , ICommandComponent
    {

        Vector2 moveOnNextUpdate;
        //float updateMoveTime;

        public CommandPacMan(Game game) : base(game)
        {
            moveOnNextUpdate = Vector2.Zero;
            //updateMoveTime = 300;
        }

        public override void Update(GameTime gameTime)
        {
            //updateMoveTime -= gameTime.ElapsedGameTime.Milliseconds;
            //if (updateMoveTime < 0) //allow move if time is below threhold for move
            //{
                UpdatePacLocation(gameTime);
            //}
            base.Update(gameTime);
        }

        protected override void UpdatePacManWithController(GameTime gameTime, float time)
        {
            //Don't update pacman this one uses a controller
            //base.UpdatePacManWithController(gameTime, time);
            if (moveOnNextUpdate != Vector2.Zero) return; //Already have move leave 
            //this.controller.Update(gameTime);
            
            
        }

        private void UpdatePacLocation(GameTime gameTime)
        {
            if (moveOnNextUpdate == Vector2.Zero) return; //No move we are out of here
            //We need to move
            //updateMoveTime = 300; //reset move time
            //TODO Should use aniamtion to go to a new point
            this.Location += (moveOnNextUpdate * this.spriteTexture.Width); // Move the width of one sprite for cell based games this should be the size of the cell

            //rotate
            UpdateRotateBasedOnDirecton(this.moveOnNextUpdate);

            //reset moveOnNextUpdae
            moveOnNextUpdate = Vector2.Zero;

            
        }

        private void UpdateRotateBasedOnDirecton(Vector2 direction)
        {
            if (direction.Length() > 0)
            {
                //calculate angle in radians
                float rotationAngle = (float)Math.Atan2(
                        direction.X,
                        direction.Y * -1);

                //This converts angle back to degree and uses art facing left as 0 degreees
                //Art that start sfacing left = rotationAngle - (float)(Math.PI / 2)
                //right = 
                //TODO add rotations in radians
                this.Rotate = (float)MathHelper.ToDegrees(rotationAngle - (float)(Math.PI / 2));
                
            }
        }

        public void MoveDown()
        {
            moveOnNextUpdate = new Vector2(0, 1);
        }

        public void MoveUp()
        {
            moveOnNextUpdate = new Vector2(0, -1);

        }

        public void MoveLeft()
        {
            moveOnNextUpdate = new Vector2(-1, 0);
        }

        public void MoveRight()
        {
            moveOnNextUpdate = new Vector2(1, 0);
        }
    }
}
