// See https://aka.ms/new-console-template for more information
using Mailoroso4and6;

//Console.WriteLine("Hello, World!");

using System;
using System.Threading;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "server")
        {
            var server = new BootstrapServer();
            server.Start();
        }
        else
        {
            Console.WriteLine("Enter Bootstrap Server IP:");
            var bootstrapServerIp = Console.ReadLine();

            var peer = new Peer();
            var peerThread = new Thread(() => peer.Start(bootstrapServerIp));
            peerThread.Start();
        }
    }
}

