using MultiThreadedTcpServer.Server;

class Program
{
    static void Main(string[] args)
    {
        int port = 5000;
        var server = new TcpServer(port);
        server.Start();
    }
}