using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleCommandWUndo;
using Microsoft.Xna.Framework;
using MGCommand;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveDownCommand : CommandWithUndo
    {
        public MoveDownCommand(Game game) : base(game)
        {
            this.CommandName = CommName.MoveDown;
            this.UndoCommand = new UndoMoveDownCommand(this);
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

    public class UndoMoveDownCommand : UndoCommand
    {

        public UndoMoveDownCommand(CommandWithUndo command) : base(command)
        {

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
}
