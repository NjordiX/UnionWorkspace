using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UWServer
{

    internal class EntryPoint
    {
        static void Mian()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Server server = new Server(ip, 8080);
            server.Start();


        }

    }
}
