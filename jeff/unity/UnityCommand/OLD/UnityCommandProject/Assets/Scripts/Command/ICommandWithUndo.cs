using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCommandWUndo
{
    /// <summary>
    /// Undo is a ICommand With and UndoCommand
    /// </summary>
    public interface ICommandWithUndo : ICommand
    {
        UndoCommand UndoCommand { get; set; }
    }
}
