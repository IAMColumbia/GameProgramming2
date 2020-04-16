using System;

namespace ConsoleCommand
{
    /// <summary>
    /// Fake class for GameComponent it fae moves by having a X and a Y int
    /// </summary>
    public class GameComponent
    {

        protected int _X, _Y;

        public int X { get { return _X; } protected set { _X = value; } }
        public int Y { get { return _Y; } protected set { _Y = value; } }

        internal void MoveRight()
        {
            //move right
            X++;
        }

        internal void MoveLeft()
        {
            //move left
            X--;
        }

        internal void MoveUp()
        {
            //Move up
            Y++;
        }

        internal void MoveDown()
        {
            //Move down
            Y--;
        }

        public string About()
        {
            string about = string.Format("location: {0}:{1}", X, Y);
            return about;
        }
    }
    }