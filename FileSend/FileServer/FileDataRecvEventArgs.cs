using System;
using System.Net;

namespace FileServer
{
    public delegate void FileDataRecvEventHandler(object sender, FileDataRecvEventArgs e);
    public class FileDataRecvEventArgs:EventArgs
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

        public long RemainLength
        {
            get;
            private set;
        }

        public byte[] Data
        {
            get;
            private set;
        }

        public FileDataRecvEventArgs(string fname, IPEndPoint rep, long rlen, byte[] data)
        {
            FileName = fname;
            RemoteEndPoint = rep;
            RemainLength = rlen;
            Data = data;
        }
    }
}
