using System;
using System.Net;

namespace FileServer
{
    public delegate void AcceptedEventHandler(object sender, AcceptedEventArgs e);
    public class AcceptedEventArgs:EventArgs
    {      
        public IPEndPoint RemoteEndPoint
        {
            get;
            private set;
        }

        public string IPStr
        {
            get
            {
                return RemoteEndPoint.Address.ToString();
            }
        }

        public int Port
        {
            get
            {
                return RemoteEndPoint.Port;
            }
        }

        public AcceptedEventArgs(IPEndPoint rep)
        {
            RemoteEndPoint = rep;
        }
    }
}
