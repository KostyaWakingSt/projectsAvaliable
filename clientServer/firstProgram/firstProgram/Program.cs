using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace firstProgram
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const string user = "Server";
            const string ip = "127.0.0.1";
            const int port = 8080;

            var IpPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(IpPoint);
            socket.Listen(5);

            while (true)
            {
                var listener = socket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (listener.Available > 0);

                Console.WriteLine(data);

                Console.Write("Вы: ");
                var message = Console.ReadLine();
                var resultMessage = user + ": " + message;

                listener.Send(Encoding.UTF8.GetBytes(resultMessage));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
