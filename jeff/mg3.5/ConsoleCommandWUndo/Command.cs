using ConsoleCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo
{
    public abstract class Command : ICommand
    {
        public string CommandName;       //Name For logging

        public Command()
        {

        }

        public virtual void Execute(GameComponent gc)
        {
            this.Log();     //Log that command happened;
        }

        protected virtual void Log()
        {
            Console.WriteLine(string.Format("{0} executed.", CommandName));
        }
    }
}
