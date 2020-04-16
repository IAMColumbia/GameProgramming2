using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo
{
    public class Command : ICommand
    {
        public string CommandName;       //Name For logging

        public Command()
        {

        }

        public virtual void Execute(GameComponent gc)
        {
            this.Log();     //Log that command happened;
        }

        protected virtual string Log()
        {

            string LogString = string.Format("{0} executed.", CommandName);
#if DEBUG
            //Only write to console if run in Debug
            Console.WriteLine(LogString);
#endif
            return LogString;
        }
    }
}
