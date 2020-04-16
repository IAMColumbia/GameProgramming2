using ConsoleCommandWUndo;
using MGCommand;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo.Commands
{
    public class MoveRightCommand : CommandWithUndo
    {

        public MoveRightCommand(Game game) : base (game)
        {
          
            this.CommandName = "Move Right";
            this.UndoCommand = new UndoMoveRightCommand(this);
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

    public class UndoMoveRightCommand : UndoCommand
    {

        public UndoMoveRightCommand(CommandWithUndo command) : base(command)
        {

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
}
