using System.Net.Sockets;

namespace core
{
    public static class Evaluator
    {
        public static Exception? EvalPING(string[] args, NetworkStream stream)
        {
            byte[] response;
            if (args.Length >= 2)
            {
                return new Exception("ERR wrong number of arguments for 'PING' command");
            }

            if (args.Length == 0)
            {
                response = RespDecoder.Encode("PONG", true);
            }
            else
            {
                response = RespDecoder.Encode(args[0], false);
            }
            stream.Write(response, 0, response.Length);
            return null;
        }

        public static Exception? EvalAndRespond(Rediscmd cmd, NetworkStream stream)
        {
            switch (cmd.Cmd)
            {
                case "PING":
                    return EvalPING(cmd.Args, stream);
                default:
                    return EvalPING(cmd.Args, stream);
            }
        }
    }
}
