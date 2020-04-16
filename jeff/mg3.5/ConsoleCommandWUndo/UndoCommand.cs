using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCommand;

namespace ConsoleCommandWUndo
{
    public abstract class UndoCommand : Command
    {
        public UndoCommand(CommandWithUndo command)
        {
            this.CommandName = "Undo " + command.CommandName;
        }
    }
}
