
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Mailoroso4and6
{
    public class BootstrapServer
    {
        private List<IPEndPoint> peers = new List<IPEndPoint>();
        private const int Port = 8080;

        public void Start()
        {
            var listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            Console.WriteLine("Bootstrap Server started...");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();
                var buffer = new byte[1024];
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (message == "JOIN")
                {
                    var endPoint = client.Client.RemoteEndPoint as IPEndPoint;
                    if (endPoint != null)
                    {
                        peers.Add(endPoint);
                        Console.WriteLine($"Peer {endPoint} joined.");
                        SendPeersList(stream);
                    }
                }
                client.Close();
            }
        }

        private void SendPeersList(NetworkStream stream)
        {
            var peerList = string.Join(",", peers);
            var data = Encoding.UTF8.GetBytes(peerList);
            stream.Write(data, 0, data.Length);
        }
    }

}
