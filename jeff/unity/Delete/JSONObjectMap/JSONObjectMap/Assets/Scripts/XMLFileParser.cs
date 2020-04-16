using Microsoft.Xna.Framework;
using MonoGameLibrary.GameFiles;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JSONOjbectMap
{
    class XMLFileParser<T> 
    {
        //GameConsole console;
        public bool ShowDebugLog { get; set; }

        public XMLFileParser() 
        {
            //console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            //if (console == null)
            //{
            //    console = new GameConsole(game);
            //    this.Game.Components.Add(console);
            //    console.GameConsoleWrite($"Game console added by {this}");
            //}

            ShowDebugLog = true;

        }

        public T LoadFromXML(string xmlFile)
        {
            FileSystem.Instance.Path = "";
            string xml = FileSystem.Instance.ReadTextFile(xmlFile);
#if DEBUG
            if (ShowDebugLog)
            {
                console.GameConsoleWrite($"LoadFromXML:{xml}");
            }
#endif
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            T obj = (T)serializer.Deserialize(stream);
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
