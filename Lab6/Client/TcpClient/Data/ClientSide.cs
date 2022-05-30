using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpClient.Entities;

namespace TcpClient.Data
{
    public static class ClientSide
    {
        public static void StartClient(int port)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            byte[] bytes = new byte[1024];

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress adress = host.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(adress, port);

            Socket sender = new Socket(adress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            var questions = new List<Question>()
            {
               new Question { Text = "OOKP is the best lesson?", Answer = "Yes" },
               new Question { Text = "С# uses the oops paradigm?", Answer = "Yes" },
               new Question { Text = "OM The worst lesson?", Answer = "No" },
               new Question { Text = "Test starts February 25?", Answer = "No" },
               new Question { Text = "In this lab we used a udp protocol?", Answer = "No" }
            };

            sender.Connect(endpoint);

            Random random = new Random();

            string message = null;
            int result = 0;

            Console.WriteLine("Socket connect to port {0} ", sender.RemoteEndPoint.ToString());

            foreach (var question in questions.OrderBy(q => random.Next()))
            {
                Console.WriteLine(question.Text);

                Console.Write("Enter anser: ");
                message = Console.ReadLine();

                if (question.IsCorrect(message))
                {
                    result++;
                }
            }

            byte[] msg = Encoding.UTF8.GetBytes(message);
            int bytesSent = sender.Send(msg);

            int bytesRec = sender.Receive(bytes);
            Console.WriteLine($"Your result:{result}");
            Console.WriteLine("\nServer response: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
