using System;
using TcpClient.Data;

namespace TcpClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClientSide.StartClient(8080);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}

