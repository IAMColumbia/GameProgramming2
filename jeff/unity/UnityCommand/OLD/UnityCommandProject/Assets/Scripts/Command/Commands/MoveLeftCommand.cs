using ConsoleCommandWUndo;
using MGCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveLeftCommand : CommandWithUndo
    {
        public MoveLeftCommand(): base ()
        {
            this.CommandName = "Move Left";
            this.UndoCommand = new UndoMoveLeftCommand(this);
        }

        public override void Execute(GameObject gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            var target = gc.GetComponent<Player>();
            if (target is Player)
            {
                target.MoveLeft();
            }
            base.Execute(gc);
        }
    }

    public class UndoMoveLeftCommand : UndoCommand
    {

        public UndoMoveLeftCommand(CommandWithUndo command) : base (command)
        {

        }

        public override void Execute(GameObject gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            var target = gc.GetComponent<Player>();
            if (target is Player)
            {
                target.MoveRight();
            }
            base.Execute(gc);
        }
    }
}
