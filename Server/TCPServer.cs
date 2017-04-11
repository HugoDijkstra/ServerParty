using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {
    class TCPServer {
        Socket handler = null;
        public Action<string> onMessageRecieved;

        public void Start() {
            byte[] bytes = new Byte[1024];

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 9050);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine("TCP Waiting for a client...");
            handler = listener.Accept();
            Console.WriteLine("Connected with: " + handler.LocalEndPoint);
            try {

                while (true) {
                    string data = null;

                    while (true) {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
 
                        if (data.IndexOf("<EOF>") > -1) {
                            break;
                        }
                    }

                    data = data.Substring(0, data.IndexOf("<EOF>"));
                   if(onMessageRecieved != null)
                        onMessageRecieved(data);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        public void sendMessage(string message) {
            if (handler == null) return;

            byte[] msg = Encoding.ASCII.GetBytes(message);

            handler.Send(msg);
        }
    }
}