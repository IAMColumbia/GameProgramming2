using System;

namespace ConsoleAppWebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri("https://webapplicationgmweb20200422141932.azurewebsites.net/api/ghost");

            using (var w = new System.Net.WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(uri);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
