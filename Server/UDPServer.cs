using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {
    class UDPServer {
        IPEndPoint ipep;
        UdpClient newsock;
        IPEndPoint sender;

        public Action<string> onMessageRecieved;

        public void Start() {
            byte[] data = new byte[1024];

            ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock = new UdpClient(ipep);

            Console.WriteLine("UDP Waiting for a client...");

            sender = new IPEndPoint(IPAddress.Any, 0);

            while (true) {
                data = newsock.Receive(ref sender);

                if (onMessageRecieved != null)
                    onMessageRecieved(Encoding.ASCII.GetString(data, 0, data.Length));
                newsock.Send(data, data.Length, sender);
            }

        }

        public void sendMessage(string message) {
            if (sender == null)
                return;

            byte[] data = new byte[1024];
            Encoding.ASCII.GetBytes(message);
            newsock.Send(data, data.Length, sender);
        }
    }
}
