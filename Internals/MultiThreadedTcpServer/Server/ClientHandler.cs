using System.Net.Sockets;
using System.Text;

namespace MultiThreadedTcpServer.Server
{
    public class ClientHandler
    {
        private static int activeClients = 0;
        public static void Handle(TcpClient client)
        {
            string addr=client.Client.RemoteEndPoint.ToString();
            Console.WriteLine($"Handling client {addr}");
            int currentClients = Interlocked.Increment(ref activeClients);
            Console.WriteLine($"[{DateTime.Now}] [CONNECTED] {addr} | Active Clients: {currentClients}");
            try
            {
                using var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer,0,buffer.Length);
                if (bytesRead == 0) return;
                string message = Encoding.UTF8.GetString(buffer,0,bytesRead);
                Console.WriteLine($"Received from {addr}:");
                Console.WriteLine($"[PROCESSING] {addr} (sleeping 8 sec...)");
                Thread.Sleep(8000);
                string body = $"Hello from server after 8 sec - {addr}";

                string response =
                    "HTTP/1.1 200 OK\r\n" +
                    "Content-Type: text/plain\r\n" +
                    "Connection: close\r\n" +
                    $"Content-Length: {body.Length}\r\n" +
                    "\r\n" +
                    body;

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
            }catch{
                Console.WriteLine($"Error handling client {addr}");
            }
            finally
            {
                client.Close();
                int currentClientss = Interlocked.Decrement(ref activeClients);
                Console.WriteLine($"[{DateTime.Now}][DISCONNECTED] {addr} | Active Clients: {currentClientss}");
            }
        }
    }
}