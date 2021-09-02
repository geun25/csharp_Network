using System;

namespace FileServer
{
    public class FileRecvServ
    {
        const int MAX_PACK_SIZE = 1024;
        public event AcceptedEventHandler AcceptedEventHandler = null;
        public event ClosedEventHandler ClosedEventHandler = null;
        public event RecvFileNameEventHandler RecvFileNameEventHandler = null;
        public event FileLengthRecvEventHandler FileLengthRecvEventHandler = null;
        public event FileDataRecvEventHandler FileDataRecvEventHandler = null;
        private string v1;
        private int v2;

        public FileRecvServ(string v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        internal void Start()
        {
            throw new NotImplementedException();
        }
    }
}