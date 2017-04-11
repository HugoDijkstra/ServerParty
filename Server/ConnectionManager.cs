using System;
using System.Threading;

namespace Server {
    public enum ConnectionType {
        TCP = 0,
        UDP = 1
    }

    class ConnectionManager {
        TCPServer tcpServer;
        UDPServer udpServer;

        Thread tcpThread;
        Thread udpThread;

        public void Start() {
            tcpServer = new TCPServer();
            udpServer = new UDPServer();

            udpThread = new Thread(new ThreadStart(UDP));
            udpThread.Start();

            tcpThread = new Thread(new ThreadStart(TCP));
            tcpThread.Start();

            tcpServer.onMessageRecieved += (string message) => printMessage(ConnectionType.TCP, message);
            udpServer.onMessageRecieved += (string message) => printMessage(ConnectionType.UDP, message);
        }

        private void printMessage(ConnectionType connectionType, string message) {
            Console.WriteLine(connectionType.ToString() + " recieved: " + message);
        }

        private void TCP() {
            tcpServer.Start();
        }

        private void UDP() {
            udpServer.Start();
        }
    }
}
