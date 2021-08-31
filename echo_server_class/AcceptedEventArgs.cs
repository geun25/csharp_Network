using System;
using System.Net;

namespace echo_server_class
{
    public delegate void AcceptedEventHandler(object sender, AcceptedEventArgs e);

    public class AcceptedEventArgs : EventArgs
    {       
        public IPEndPoint RemoteEP
        {
            get;
            private set;
        }

        public AcceptedEventArgs(IPEndPoint remote_ep)
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
