using System;
using System.Net;

namespace echo_server_class
{
    public delegate void RecvedMsgEventHandler(object sender, RecvedMsgEventArgs e);
    public class RecvedMsgEventArgs : EventArgs
    {
        public IPEndPoint RemoteEP
        {
            get;
            private set;
        }

        public string Msg
        {
            get;
            private set;
        }

        public RecvedMsgEventArgs(IPEndPoint remote_ep, string msg)
        {
            RemoteEP = remote_ep;
            MSg = msg;
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
