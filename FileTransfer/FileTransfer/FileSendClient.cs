using System;

namespace FileClient
{
    internal class FileSendClient
    {
        private string ip;
        private int port;

        public FileSendClient(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        internal void SendAsync(string fname)
        {
            throw new NotImplementedException();
        }
    }
}