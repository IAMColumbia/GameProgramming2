using ConsoleServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServerClient
{
    class Program
    {
        static TestGameClient client;

        static void Main(string[] args)
        {
            Console.WriteLine("client: waiting to connect.");
            System.Threading.Thread.Sleep(1000);
            client = new TestGameClient();
            client.Initialize();
            Console.WriteLine("client: conected ");

            //UpdateClient();

            string stringToServer = "";
            while (stringToServer != "quit")
            {
                
                Console.Write("client:");
                client.SendText(Console.ReadLine());
                
            }
        }

        static async Task UpdateClient()
        {
            while (true)
            {
                await Task.Delay(500);
                client.Update();
            }
        }
    }
}
