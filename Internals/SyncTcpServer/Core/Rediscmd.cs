namespace core
{
    public class Rediscmd
    {
        public string Cmd { get; set; }
        public string[] Args { get; set; }

        public Rediscmd(string cmd, string[] args)
        {
            Cmd = cmd;
            Args = args;
        }
    }
}
