using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPacManComponents.Ghost
{
    /// <summary>
    /// One Day I'll refaco ghost like the pacman class
    /// </summary>
    public class GameConsoleGhost : Ghost
    {

        public GameConsole gameConsole;
        public GameConsoleGhost()
        {
            this.gameConsole = null;
        }

        public GameConsoleGhost(GameConsole console)
        {
            this.gameConsole = console;
        }

        public override void Log(string s)
        {
            if (gameConsole != null)
            {
                gameConsole.GameConsoleWrite("Ghost:" +  s);
            }
            else
            {
                base.Log(s);
            }
        }
    }
    
}
