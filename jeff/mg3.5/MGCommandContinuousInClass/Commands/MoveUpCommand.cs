using ConsoleCommandWUndo;
using MGCommand;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveUpCommand : CommandWithUndo
    {
        public MoveUpCommand(Game game) : base (game)
        {
            this.CommandName = CommName.MoveUp;
            this.UndoCommand = new UndoMoveUpCommand(this);
        }

        public override void Execute(GameComponent gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            if (gc is CommandPacMan)
            {
                ((CommandPacMan)gc).MoveUp();
            }
            base.Execute(gc);
        }
    }

    public class UndoMoveUpCommand : UndoCommand
    {

        public UndoMoveUpCommand(CommandWithUndo command) : base(command)
        {

        }

        public override void Execute(GameComponent gc)
        {
            //Different Game Components may move differently check if the gc is a CommandPacMan
            if (gc is CommandPacMan)
            {
                ((CommandPacMan)gc).MoveDown();
            }
            base.Execute(gc);
        }
    }
}
