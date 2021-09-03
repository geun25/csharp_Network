using System;
using System.Net;

namespace FileServer
{
    public delegate void FileLengthRecvEventHandler(object sender, FileLengthRecvEventArgs e);
    public class FileLengthRecvEventArgs:EventArgs
    {
        public IPEndPoint RemoteEndPoint
        {
            get;
            private set;
        }

        public string FileName
        {
            get;
            private set;
        }

        public long Length
        {
            get;
            private set;
        }

        public FileLengthRecvEventArgs(string fname, IPEndPoint rep, long length)
        {
            FileName = fname;
            RemoteEndPoint = rep;
            Length = length;
        }
    }
}
