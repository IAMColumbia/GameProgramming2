using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPacManComponents.Pac
{
    //PacMan States
    public enum PacManState { Spawning, Still, Chomping, SuperPacMan, EndSuperPacMan, Dying }

    public class PacMan
    {
        protected PacManState _state;   //Pravate instance data member
        public PacManState State
        {
            get { return _state; }
            set
            {
                //If the new value is different from the old value
                if (_state != value)
                {
                    //log the change
                    this.Log(string.Format("{0} was: {1} now {2}",
                         this.ToString(), _state, value));

                    _state = value;
                }
            }
        }

        public PacMan()
        {
            //Set default state will call notify so make sure this.Ghosts is intitialized first
            this.State = PacManState.Still;
        }
        /// <summary>
        /// Kills the pacman
        /// </summary>
        public virtual void Die()
        {
            if(this.State != PacManState.Dying)
                this.State = PacManState.Dying;
        }

        //Extra method for logging state change
        public virtual void Log(string s)
        {
            //Default log goes to system console
            Console.WriteLine(s);
        }
    }
}
