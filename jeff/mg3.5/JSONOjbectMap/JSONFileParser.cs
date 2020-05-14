using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Util;
using MonoGameLibrary.GameFiles;
using Microsoft.Xna.Framework;
using System.Net;
using System.Net.Http;

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

        public T LoadFromWebJSON()
        {
            string json = string.Empty;
            Uri uri = new Uri("https://webapplicationgmweb20200422141932.azurewebsites.net/api/ghost");
            using (var w = new System.Net.WebClient())
            {
                // attempt to download JSON data as a string
                try
                {
                    json = w.DownloadString(uri);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

#if DEBUG
            if (ShowDebugLog)
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
