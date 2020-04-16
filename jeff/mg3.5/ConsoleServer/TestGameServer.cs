using MonoGameLibrary.Network;
using MonoGameLibrary.Network.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer
{
    public class TestGameServer 
    {
        GameServer server;
        List<int> clients;

        public TestGameServer()
        {
            this.clients = new List<int>();
        }

        public virtual void Initialize()
        {
            server = GameServer.Server;
            server.Initialize();
            server.BeginAcceptingClients(onConnect);
        }

        public void Update()
        {
            foreach (int c in clients)
            {
                if (!server.Clients[c].Reading)
                {
                    server.BeginRead(onRead, c);
                }
            }
        }

        private void onConnect(object sender, OnConnectEventArgs e)
        {
            if (!server.Clients[e.ClientID].Reading)
            {
                server.BeginRead(onRead, e.ClientID);
                clients.Add(e.ClientID);
            }
        }

        private void onRead(object sender, OnReadEventArgs e)
        {
            string type = e.Obj.GetType().ToString();

            switch (type)
            {
                case "System.String":
                    Console.WriteLine(string.Format("Server clientID:{0} {1}", e.ClientID, ((string)e.Obj).ToString()));
                    //send to other client
                    foreach (int c in clients)
                    {
                        if (c != e.ClientID)
                        {
                            server.BeginWrite(onWrite, c, e.Obj);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void onWrite(object sender, OnWriteEventArgs e)
        {

        }

    }
}
