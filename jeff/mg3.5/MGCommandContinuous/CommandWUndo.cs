using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MGCommand;
namespace ConsoleCommandWUndo
{
    public class CommandWithUndo : MGCommand.MGCommand, ICommandWithUndo
    {
        GameComponent gc;                   //Refernece to game component
        public UndoCommand UndoCommand { get; set; }

        public CommandWithUndo(Game game) : base (game)
        {
            
        }

        public override void Execute(GameComponent gc)
        {
            this.gc = gc;   //Hold a refernce to the game componet this command was excuted on
            base.Execute(gc);
        }
        public void UnExecute()
        {
            this.UndoCommand.Execute(gc);
        }
    }
}
