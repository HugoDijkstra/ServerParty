using System;
using System.Net.Sockets;
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
            listner = new TcpListener(6669);
            listner.Start();
            client = default(TcpClient);
        }
    }
}
