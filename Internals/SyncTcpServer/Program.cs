// Console.WriteLine("Hello, World!");
using Project.SyncTcpServer;

class Program
{
    static void Main(string [] args)
    {
        Console.WriteLine("Starting TCP Server...");
        SyncTcpServer.run();
    }
}