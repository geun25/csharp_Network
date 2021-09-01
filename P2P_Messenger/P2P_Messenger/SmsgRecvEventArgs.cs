using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Messenger
{
    public delegate void SmsgRecvEventHandler(object sender, SmsgRecvEventArgs e);
    public class SmsgRecvEventArgs:EventArgs
    {
        public IPEndPoint RemoteEndPoint
        {
            get;
            private set;
        }

        public string Msg
        {
            get;
            private set;
        }

        public SmsgRecvEventArgs(IPEndPoint remote, string msg)
        {
            RemoteEndPoint = remote;
            Msg = msg;
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
    }
}
