using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Project.Config;
namespace Project.SyncTcpServer;

public static class SyncTcpServer
{
    static string ReadCommand(NetworkStream stream)
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        if (bytesRead == 0)
            throw new Exception("EOF");
        return Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
    }
    static void Respond(NetworkStream stream, string cmd)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(cmd + "\n");
        stream.Write(buffer, 0, buffer.Length);
    }

    public static void run()
    {
        Console.WriteLine($"starting a synchronous TCP server on {ServerConfig.Host}:{ServerConfig.Port}");
        int con_clients=0;
        TcpListener server = new TcpListener(IPAddress.Parse(ServerConfig.Host), ServerConfig.Port);
        server.Start();
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            con_clients++;
            Console.WriteLine($"Client connected. Total clients: {con_clients}");
            NetworkStream stream = client.GetStream();
            while (true)
            {
                try
                {
                    string cmd = ReadCommand(stream);
                    Console.WriteLine($"Received command: {cmd}");
                    Respond(stream, $"Echo: {cmd}");
                }
                catch (Exception ex)
                {
                    client.Close();
                    Console.WriteLine($"Client disconnected. Total clients: {--con_clients}");
                    if (ex.Message == "EOF")
                        break;
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}