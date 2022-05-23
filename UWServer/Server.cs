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
                _ = Process(client);  // Таску пока нигде не храним (мб и не нужно будет)
            }


        }

        private async Task Process(TcpClient client) => await Task.Run(() => _Process(client));  // Асинхроночка
        private void _Process(TcpClient client)  // Определяем поток передачи данных , получаем дату - отвечаем 
        {
            NetworkStream stream = client.GetStream();  
            while(true)                                     //Обязательно нужно прикрутить условие выхода 
            {
                byte [] buffer = new byte[4096];
                StringBuilder builder = new();

                do
                {
                    _ = stream.Read(buffer, 0, buffer.Length);          // Количество читаемых байт не храним, но дискард на всякий пожарный поставил
                    builder.Append(Encoding.UTF8.GetString(buffer));

                } while (stream.DataAvailable);

                string message = builder.ToString();    //  Здесь будет обработчик полученной информации , пока посто выводить будем на консоль 
                Console.WriteLine(message);

                string respone = "respone";                 // Тут ответ , пока тривиально , логику приверну позже 
                buffer = Encoding.UTF8.GetBytes(respone);
                stream.Write(buffer, 0, buffer.Length);
            }
            Stop(ref stream, CancellationToken.None);

        }
        void Stop (ref NetworkStream stream, CancellationToken token)  //Остановочка, мб через КанцелТокены
        {
            stream.Close();
        }



    }
}
