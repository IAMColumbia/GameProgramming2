using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCommandWUndo;
using MGCommand;
using UnityEngine;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveDownCommand : CommandWithUndo
    {
        public MoveDownCommand() : base()
        {
            this.CommandName = "Move Up";
            this.UndoCommand = new UndoMoveDownCommand(this);
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

    public class UndoMoveDownCommand : UndoCommand
    {

        public UndoMoveDownCommand(CommandWithUndo command) : base(command)
        {

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
}
