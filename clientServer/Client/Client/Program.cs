using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        protected static bool isSended = false;
        protected const string user = "Client";
        protected const string ip = "127.0.0.1";
        protected const int port = 8080;
        protected static IPEndPoint IpPoint;
        protected static Socket socket;
        static void Main(string[] args)
        {
            IpPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SendMessage();
        }

        public static void SendMessage()
        {
            try
            {
                if (!isSended)
                {
                    Console.Write("Вы: ");
                    var message = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(message))
                    {
                        throw new Exception("Сообщение не должно быть пустым.");
                    }
                    var resultMessage = user + ": " + message;

                    var data = Encoding.UTF8.GetBytes(resultMessage);
                    socket.Connect(IpPoint);

                    socket.Send(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                SendMessage();
            }
            finally
            {
                ReadBase();
                isSended = true;
            }
        }

        public static void ReadBase()
        {
            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = socket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (socket.Available > 0);

            Console.WriteLine(answer.ToString());

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            isSended = false;
            Main(null);
        }
    }
}
