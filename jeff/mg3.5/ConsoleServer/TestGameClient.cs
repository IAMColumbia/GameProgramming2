using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoGameLibrary.Network;
using MonoGameLibrary.Network.Client;

namespace ConsoleServer
{
    class TestGameClient
    {
        GameClient client;

        public bool Reading {  get { return client.Reading; } }

        public TestGameClient()
        {

        }

        

        public virtual void Initialize()
        {
            client = new GameClient();
            
            client.Connect("127.0.0.1"); //Home address
            if (client.Connected)
                client.BeginWrite(onWrite, "Hello");

            
        }

        public void Update()
        {
            if (client.Connected)
            {
                if (!client.Reading)
                {
                    client.BeginRead(onRead);
                }
            }
        }

        public void SendText(string text)
        {
            if (client.Connected)
            { 
                client.BeginWrite(onWrite, text);
            
                if (!client.Reading)
                {
                    client.BeginRead(onRead);
                }
            }

        }

        private void onWrite(object sender, OnWriteEventArgs e)
        {

        }

        private void onRead(object sender, OnReadEventArgs e)
        {
            string type = e.Obj.GetType().ToString();
            switch (type)
            {
                case "System.String":
                    Console.WriteLine(string.Format("client {0}:{1}", e.ClientID, e.Obj.ToString()));
                    break;
                default:
                    break;
            }
        }

    }

}
