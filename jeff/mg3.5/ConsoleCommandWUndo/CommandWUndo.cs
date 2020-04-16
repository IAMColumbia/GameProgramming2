using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCommand;

namespace ConsoleCommandWUndo
{
    public abstract class CommandWithUndo : Command, ICommandWithUndo
    {
        GameComponent gc;                   //Refernece to game component
        public UndoCommand UndoCommand { get; set; }

        public CommandWithUndo()
        {
            //nothing
        }

        public override void Execute(GameComponent gc)
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
