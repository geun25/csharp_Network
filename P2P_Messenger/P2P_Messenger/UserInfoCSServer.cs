using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace P2P_Messenger
{
    public class UserInfoCSServer
    {
        public event UserInfoEventHandler UserInfoEventHandler = null;

        public string IPStr
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public UserInfoCSServer(string ipstr, int port)
        {
            IPStr = ipstr;
            Port = port;
        }
        Socket sock = null;

        public bool Start()
        {
            try
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipaddr = IPAddress.Parse(IPStr);
                IPEndPoint iep = new IPEndPoint(ipaddr, Port);
                bool check = true;
                while(check)
                {
                    try
                    {
                        sock.Bind(iep);
                        check = false;
                    }
                    catch
                    {
                        Port += 2;
                        iep = new IPEndPoint(ipaddr, Port);
                    }
                }
                sock.Listen(5);
                AcceptLoopAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        delegate void AcceptDele();
        private void AcceptLoopAsync()
        {
            AcceptDele dele = AcceptLoop;
            dele.BeginInvoke(null, null);
        }

        private void AcceptLoop()
        {
            Socket dosock;
            while(true)
            {
                dosock = sock.Accept();
                DoItAsync(dosock);
            }
        }

        delegate void DoItDele(Socket dosock);
        private void DoItAsync(Socket dosock)
        {
            DoItDele dele = DoIt;
            dele.BeginInvoke(dosock, null, null);
        }

        private void DoIt(Socket dosock)
        {
            byte[] packet = new byte[1024];
            dosock.Receive(packet);
            MemoryStream ms = new MemoryStream(packet);
            BinaryReader br = new BinaryReader(ms);
            string id = br.ReadString();
            string ip = br.ReadString();
            int sport = br.ReadInt32();
            int fport = br.ReadInt32();
            br.Close();
            ms.Close();
            if(UserInfoEventHandler != null)
                UserInfoEventHandler(this, new UserInfoEventArgs(id, ip, sport, fport));
            dosock.Close();
        }
    }
}