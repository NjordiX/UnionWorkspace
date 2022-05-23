using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UWServer
{
    sealed class Server // Без наследования , смысла в нем нет
    {
        static public TcpListener? Listener { get; private set; } = null; // Статик с нулл для дальнейших проверок и запуска в одном экземпляре
        public List <TcpClient> Clients { get; private set; } // Список подключающихся клиентов 


        public Server(IPAddress ip , int port)  // Базовый конструктор
        {
            try
            {
                if (Listener==null)
                {
                    Listener = new TcpListener(ip, port);
                    Clients = new List<TcpClient>();
                }
                else
                {
                    throw new Exception("Сервер уже запущен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Start();
            
        }

        void Start()  // Запуск процесса прослушивание и получения клиентов   (будет уводить какждого клиента в свою таску)
        {
            Listener.Start();
            while (true)
            {
                TcpClient client = Listener.AcceptTcpClient();
                Clients.Add(client);
                Process(client);
            }


        }


        private void Process(TcpClient client)  // Определяем поток передачи данных , получаем дату - отвечаем 
        {
            NetworkStream stream = client.GetStream();
            do
            {
                byte [] buffer = new byte[4096];
                StringBuilder builder = new();

                do
                {
                    _ = stream.Read(buffer, 0, buffer.Length);
                    builder.Append(Encoding.UTF8.GetString(buffer));

                } while (stream.DataAvailable);

                string message = builder.ToString();    //  Здесь будет обработчик полученной информации , пока посто выводить будем на консоль 
                Console.WriteLine(message);


                

            } while (true);

        }
        void Stop ()
        {

        }



    }
}
