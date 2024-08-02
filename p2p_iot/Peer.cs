using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mailoroso4and6
{
    public class Peer
    {
        private readonly List<IPEndPoint> connectedPeers = new List<IPEndPoint>();
        private const int Port = 0; // Let OS assign a port

        public void Start(string bootstrapServerIp)
        {
            var bootstrapServerEndPoint = new IPEndPoint(IPAddress.Parse(bootstrapServerIp), 8080);
            ConnectToBootstrapServer(bootstrapServerEndPoint);

            var listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            Console.WriteLine("Peer started...");
            var listenerThread = new Thread(() => ListenForMessages(listener));
            listenerThread.Start();

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "exit")
                    break;

                SendMessageToAllPeers(message);
            }

            listener.Stop();
        }

        private void ConnectToBootstrapServer(IPEndPoint serverEndPoint)
        {
            var client = new TcpClient();
            client.Connect(serverEndPoint);
            var stream = client.GetStream();

            var data = Encoding.UTF8.GetBytes("JOIN");
            stream.Write(data, 0, data.Length);

            var buffer = new byte[1024];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            var peers = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split(',');

            foreach (var peer in peers)
            {
                var parts = peer.Split(':');
                connectedPeers.Add(new IPEndPoint(IPAddress.Parse(parts[0]), int.Parse(parts[1])));
            }

            client.Close();
        }

        private void ListenForMessages(TcpListener listener)
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();
                var buffer = new byte[1024];
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + message);
                client.Close();
            }
        }

        private void SendMessageToAllPeers(string message)
        {
            foreach (var peer in connectedPeers)
            {
                var client = new TcpClient();
                client.Connect(peer);
                var stream = client.GetStream();
                var data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                client.Close();
            }
        }
    }
}
