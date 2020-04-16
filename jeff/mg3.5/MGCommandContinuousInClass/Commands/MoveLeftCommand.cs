using ConsoleCommandWUndo;
using MGCommand;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveLeftCommand : CommandWithUndo
    {
        public MoveLeftCommand(Game game): base (game)
        {
            this.CommandName = CommName.MoveLeft;
            this.UndoCommand = new UndoMoveLeftCommand(this);
        }

        public override void Execute(GameComponent gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            if (gc is CommandPacMan)
            {
                ((CommandPacMan)gc).MoveLeft();
            }
            base.Execute(gc);
        }
    }

    public class UndoMoveLeftCommand : UndoCommand
    {

        public UndoMoveLeftCommand(CommandWithUndo command) : base (command)
        {

        }

        public override void Execute(GameComponent gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            if (gc is CommandPacMan)
            {
                ((CommandPacMan)gc).MoveRight();
            }
            base.Execute(gc);
        }
    }
}
