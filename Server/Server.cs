using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        TcpListener listner;
        TcpClient client;
        public void Start()
        {
            Console.WriteLine("Creating server");

            IPAddress ip = IPAddress.Parse("10.125.128.39");
            listner = new TcpListener(ip, 8888);
            listner.Start();
            Console.WriteLine("Listener started");
            client = default(TcpClient);
            Thread t = new Thread(getConnection);
            t.Start();
            Console.WriteLine("Waiting for connection");
            while ((true))
            {
                if (client != null)
                    if (!client.Connected)
                        continue;
                if (client != null)
                    try
                    {
                        NetworkStream networkStream = client.GetStream();
                        byte[] bytesFrom = new byte[90025];
                        if ((int)client.ReceiveBufferSize == 0)
                        {
                            networkStream.Flush();
                            continue;
                        }
                        networkStream.Read(bytesFrom, 0, (int)client.ReceiveBufferSize);
                        string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        Console.WriteLine("Data >> " + dataFromClient.Substring(0, dataFromClient.IndexOf("$")));
                        networkStream.Flush();
                        if(dataFromClient.Substring(0, dataFromClient.IndexOf("$")) == "/close")
                        {
                            break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception >> " + (int)client.ReceiveBufferSize);
                    }
            }
            client.Close();
            listner.Stop();

        }
        void getConnection()
        {
            client = listner.AcceptTcpClient();
        }
    }
}
