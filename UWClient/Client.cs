using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWClient
{
    sealed internal class Client
    {
        TcpClient client;
        NetworkStream stream;
        public Client ()
        {
            client = new TcpClient (); 
        }
        public void Connect(IPAddress ip, int port)
        {
            try
            {
                client.Connect(ip, port);
            }
            catch (Exception)
            {

            }
 
        }

        public async Task Start() => await Task.Run(() => _Process());
        void _Process()
        {
            stream = client.GetStream();
            byte [] data = new byte[1024];
            StringBuilder builder = new StringBuilder ();

            do
            {
                stream.Read (data, 0, data.Length);
                builder.Append (Encoding.UTF8.GetString(data));

            } while (stream.DataAvailable);
        }

        
    }
}
