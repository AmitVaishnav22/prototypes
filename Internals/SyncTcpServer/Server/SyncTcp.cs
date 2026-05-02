using System.Net;
using System.Net.Sockets;
using System.Text;
using Project.Config;
using core;
namespace Project.SyncTcpServer;

public static class SyncTcpServer
{
    static Rediscmd ReadCommand(NetworkStream stream)
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        if (bytesRead == 0)
            throw new Exception("EOF");

        byte[] data = new byte[bytesRead];
        Array.Copy(buffer, data, bytesRead);

        var tokens = RespDecoder.DecodeArrayString(data);
        Console.WriteLine($"Decoded command: {tokens[0].ToUpper()}, Arguments: {string.Join(", ", tokens.Skip(1))}");
        return new Rediscmd(tokens[0].ToUpper(), tokens.Skip(1).ToArray());
    }
    static void Respond(Rediscmd cmd, NetworkStream stream)
    {
        var err = Evaluator.EvalAndRespond(cmd, stream);
        if (err != null)
            RespondError(stream, err);
    }

    static void RespondError(NetworkStream stream, Exception err)
    {
        byte[] buffer = Encoding.UTF8.GetBytes($"-{err.Message}\r\n");
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
                    var cmd = ReadCommand(stream);
                    Console.WriteLine($"Received command: {cmd.Cmd}");
                    Respond(cmd, stream);
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