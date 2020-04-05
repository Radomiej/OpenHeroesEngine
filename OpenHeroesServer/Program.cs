using System;
using OpenHeroesServer.Server;

namespace OpenHeroesServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server Initialize!");
            var basicServer = BasicServer.CreateInstance();
            Console.ReadKey();
        }
    }
}