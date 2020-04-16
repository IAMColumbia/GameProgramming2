using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGCommand;
using UnityEngine;

namespace ConsoleCommandWUndo
{
    public class CommandWithUndo : Command, ICommandWithUndo
    {
        GameObject gc;                   //Refernece to game component
        public UndoCommand UndoCommand { get; set; }

        public CommandWithUndo() : base ()
        {
            
        }

        public override void Execute(GameObject gc)
        {
            this.gc = gc;   //Hold a refernce to the game componet this command was excuted on
            base.Execute(gc);
        }
        public void UnExecute()
        {
            this.UndoCommand.Execute(gc);
        }
    }
}
