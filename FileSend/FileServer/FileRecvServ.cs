using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

        public string IPStr
        {
            get;
            private set;
        }

        public int Port
        {
            get;
            private set;
        }

        public FileRecvServ(string ip, int port)
        {
            IPStr = ip;
            Port = port;
        }

        Socket sock;
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
            }
            catch
            {
                return false;
            }
            return true;
        }

        delegate void AcceptDele(); // 대리자 이용
        private void AcceptLoopAsync()
        {
            AcceptDele dele = AcceptLoop;
            dele.BeginInvoke(null, null); // 비동기 실행
        }

        private void AcceptLoop()
        {
            while(true)
            {
                Socket dosock = sock.Accept();
                DoItAsync(dosock);
            }
        }

        Thread thread; // 스레드 이용
        private void DoItAsync(Socket dosock)
        {
            ParameterizedThreadStart pts = DoIt;
            thread = new Thread(pts);
            thread.Start(dosock);
        }

        void DoIt(object osock)
        {
            Socket dosock = osock as Socket;
            IPEndPoint rep = dosock.RemoteEndPoint as IPEndPoint;
            if(AcceptedEventHandler != null)           
                AcceptedEventHandler(this, new AcceptedEventArgs(rep));

            string fname = RecvFileName(dosock);
            if (RecvFileNameEventHandler != null)
                RecvFileNameEventHandler(this, new RecvFileNameEventArgs(fname, rep));

            long length = RecvFileLength(dosock);
            if (FileLengthRecvEventHandler != null)
                FileLengthRecvEventHandler(this, new FileLengthRecvEventArgs(fname, rep, length));

            RecvFile(dosock, fname, length);
            dosock.Close();
            if (ClosedEventHandler != null)
                ClosedEventHandler(this, new ClosedEventArgs(rep));
        }

        private void RecvFile(Socket dosock, string fname, long length)
        {
            IPEndPoint rep = dosock.RemoteEndPoint as IPEndPoint;
            byte[] packet = new byte[MAX_PACK_SIZE];
            while(length>=MAX_PACK_SIZE)
            {
                int rlen = dosock.Receive(packet);
                if(FileDataRecvEventHandler != null)
                {
                    byte[] pd2 = new byte[rlen];
                    MemoryStream ms = new MemoryStream(pd2);
                    ms.Write(packet, 0, rlen);
                    FileDataRecvEventHandler(this, new FileDataRecvEventArgs(fname, rep, length, pd2));
                }
                length -= rlen;
            }
            dosock.Receive(packet, (int)length, SocketFlags.None);
            if (FileDataRecvEventHandler != null)
            {
                byte[] pd2 = new byte[length];
                MemoryStream ms = new MemoryStream(pd2);
                ms.Write(packet, 0, (int)length);
                FileDataRecvEventHandler(this, new FileDataRecvEventArgs(fname, rep, 0, pd2));
            }
        }

        private long RecvFileLength(Socket dosock)
        {
            byte[] packet = new byte[8];
            dosock.Receive(packet);
            MemoryStream ms = new MemoryStream(packet);
            BinaryReader br = new BinaryReader(ms);
            long length = br.ReadInt64();
            br.Close();
            ms.Close();
            return length;
        }

        private string RecvFileName(Socket dosock)
        {
            byte[] packet = new byte[MAX_PACK_SIZE];
            dosock.Receive(packet);
            MemoryStream ms = new MemoryStream(packet);
            BinaryReader br = new BinaryReader(ms);
            string fname = br.ReadString();
            br.Close();
            ms.Close();
            return fname;
        }
    }
}