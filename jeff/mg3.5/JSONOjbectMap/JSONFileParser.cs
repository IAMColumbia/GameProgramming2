using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Util;
using MonoGameLibrary.GameFiles;
using Microsoft.Xna.Framework;

namespace JSONOjbectMap
{


    class JSONFileParser<T> : GameComponent
    {
        GameConsole console;
        public bool ShowDebugLog { get; set; }

        public JSONFileParser(Game game) : base(game)
        {
            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if (console == null)
            {
                console = new GameConsole(game);
                this.Game.Components.Add(console);
                console.GameConsoleWrite($"Game console added by {this}");
            }

            ShowDebugLog = true;

        }

        public T LoadFromJSON(string jsonFile)
        {
            FileSystem.Instance.Path = "";
            string json = FileSystem.Instance.ReadTextFile(jsonFile);
#if DEBUG
            if(ShowDebugLog)
            {
                console.GameConsoleWrite($"LoadFromJSON:{json}");
            }
#endif
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
#if DEBUG
            if (ShowDebugLog)
            {
                console.GameConsoleWrite($"parsed:{obj.ToString()}");
            }
#endif
            return obj;
        }
    }
}
