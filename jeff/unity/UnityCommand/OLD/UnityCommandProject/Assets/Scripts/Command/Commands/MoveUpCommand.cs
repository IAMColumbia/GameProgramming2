using ConsoleCommandWUndo;
using MGCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveUpCommand : CommandWithUndo
    {
        public MoveUpCommand() : base ()
        {
            this.CommandName = "Move Up";
            this.UndoCommand = new UndoMoveUpCommand(this);
        }

        public override void Execute(GameObject gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            var target = gc.GetComponent<Player>();
            if (target is Player)
            {
                target.MoveUp();
            }
            base.Execute(gc);
        }
    }

    public class UndoMoveUpCommand : UndoCommand
    {

        public UndoMoveUpCommand(CommandWithUndo command) : base(command)
        {

        }

        public override void Execute(GameObject gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            var target = gc.GetComponent<Player>();
            if (target is Player)
            {
                target.MoveDown();
            }
            base.Execute(gc);
        }
    }
}
