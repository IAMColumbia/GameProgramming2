using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConsoleCommandWUndo
{
    public class Command : ICommand
    {
        public string CommandName;       //Name For logging

        public Command()
        {

        }

        public virtual void Execute(GameObject gc)
        {
            this.Log();     //Log that command happened;
        }

        protected virtual string Log()
        {

            string LogString = string.Format("{0} executed.", CommandName);
#if DEBUG
            //Only write to console if run in Debug
            Debug.Log(LogString);
#endif
            return LogString;
        }
    }
}
