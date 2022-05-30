using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer.Data
{
    public class ServerSide
    {
        public static void StartServer()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress adress = host.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(adress, 8080);

            Socket sListener = new Socket(adress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(endpoint);
                sListener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Wait for connection on port {0}", endpoint);

                    Socket handler = sListener.Accept();
                    string data = null;

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    string reply = $"Connected";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}
