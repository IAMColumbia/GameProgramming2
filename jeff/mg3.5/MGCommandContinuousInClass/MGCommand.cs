using ConsoleCommandWUndo;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCommand
{
    /// <summary>
    /// Monogame command that logs to the GameConsole rather than the 
    /// standard
    /// </summary>
    public class MGCommand : Command
    {

        GameConsole console;    //GameConsole to lof to rather then stanr output console

        public MGCommand(Game game)
        {
            //Get game console form game or create a new one
            console = (GameConsole)game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(game);
                game.Components.Add(console);
            }
        }

        protected override string Log()
        {
            
            string LogString = base.Log();
#if DEBUG
            //Only Log to GameConsole if run in debug
            console.GameConsoleWrite(LogString);
#endif
            return LogString;
        }
    }
}
