using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPacManComponents.Pac
{
    public class GameConsolePacMan : PacMan
    {
        GameConsole console;
        public GameConsolePacMan()
        {
            this.console = null;
        }

        public GameConsolePacMan(GameConsole console)
        {
            this.console = console;
        }

        public override void Log(string s)
        {
            if (console != null)
            {
                console.GameConsoleWrite(s);
            }
            else
            {
                base.Log(s);
            }
        }

        public virtual void Log(string DebugKey, string DebugValue)
        {
            if (console != null)
            {
                console.Log(DebugKey, DebugValue);
            }
            else
            {
                base.Log(DebugKey + ":" + DebugValue);
            }
        }
    }
}
