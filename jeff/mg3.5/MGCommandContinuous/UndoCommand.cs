using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo
{
    public class UndoCommand : Command
    {
        public UndoCommand(CommandWithUndo command)
        {
            this.CommandName = "Undo " + command.CommandName;
        }
    }
}
