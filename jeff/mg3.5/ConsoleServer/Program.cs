using MonoGameLibrary.Network.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGameServer server = new TestGameServer();
            server.Initialize();

            Console.WriteLine("Server started..");
            Console.WriteLine("Starting Client");

            while(true)
            {
                System.Threading.Thread.Sleep(100);
                server.Update();
            }
            
        }
    }
}
