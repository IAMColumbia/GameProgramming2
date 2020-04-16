using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCommand
{
    public abstract class Command : ICommand
    {

        public string CommandName;

        public Command()
        {
            this.CommandName = "Base Command";
        }

        //Will Execute command when implemeted by child class
        public virtual void Execute(GameComponent gc)
        {
            this.Log();
        }

        protected virtual void Log()
        {
            //Basic Command logs to console
            Console.WriteLine(string.Format("{0} executed.",CommandName));
        }
    }
}
