using System;
using System.Net;

namespace echo_server_class
{
    public delegate void ClosedEventHandler(object sender, ClosedEventArgs e);
    public class ClosedEventArgs : EventArgs
    {
        public IPEndPoint RemoteEP
        {
            get;
            private set;
        }

        public ClosedEventArgs(IPEndPoint remote_ep)
        {
            RemoteEP = remote_ep;
        }

        public string IPStr
        {
            get
            {
                return RemoteEP.Address.ToString();
            }
        }

        public int Port
        {
            get
            {
                return RemoteEP.Port;
            }
        }
    }
}
