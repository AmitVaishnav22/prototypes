using System.Net;
using System.Net.Sockets;
using System.Threading;
using MultiThreadedTcpServer.Server;        


namespace MultiThreadedTcpServer.Server
{
    public class TcpServer
    {
        private readonly int _port;
        public TcpServer(int port)
        {
            _port = port;
        }
        public void Start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"Server started on port {_port}. Waiting for clients...");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
                Thread thread = new(() => { ClientHandler.Handle(client); });
                thread.Start();
                Console.WriteLine($"[ACTIVE THREADS] {ThreadPool.ThreadCount}");
            }
        }
    }
}