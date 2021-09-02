using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FileClient
{
    public class FileSendClient
    {
        const int MAX_PACK_SIZE = 1024;
        public event SendFileDataEventHandler SendFileDataEventHandler = null;

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

        public FileSendClient(string ip, int port)
        {
            IPStr = ip;
            Port = port;
        }

        delegate void SendDele(string fname);
        public void SendAsync(string fname)
        {
            SendDele dele = Send;
            dele.BeginInvoke(fname, null, null);
        }

        public void Send(string fname)
        {
            if (File.Exists(fname) == false)
                return;
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPStr), Port);
            sock.Connect(iep);

            byte[] packet = new byte[MAX_PACK_SIZE];
            MemoryStream ms = new MemoryStream(packet);
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(fname);
            bw.Close();
            ms.Close();
            sock.Send(packet); // 파일의 이름 전송

            FileStream fs = File.OpenRead(fname);
            ms = new MemoryStream(packet);
            bw = new BinaryWriter(ms);
            bw.Write(fs.Length);
            sock.Send(packet, 0, 8, SocketFlags.None);
            ms.Close();
            bw.Close();

            long remain = fs.Length;
            int sl;
            while (remain >= MAX_PACK_SIZE) ;
            {
                fs.Read(packet, 0, MAX_PACK_SIZE);
                sl = sock.Send(packet);
                while (sl < MAX_PACK_SIZE) // 남은 파일 전송
                {
                    sl += sock.Send(packet, sl, MAX_PACK_SIZE - sl, SocketFlags.None);
                }

                if (SendFileDataEventHandler != null)
                    SendFileDataEventHandler(this, new SendFileDataEventArgs(fname, remain));
                remain -= MAX_PACK_SIZE;
            }

            fs.Read(packet, 0, (int)remain);
            sl = sock.Send(packet);
            while (sl < remain)
            {
                sl += sock.Send(packet, sl, (int)remain - sl, SocketFlags.None);
            }
            remain = 0;

            if (SendFileDataEventHandler != null)
                SendFileDataEventHandler(this, new SendFileDataEventArgs(fname, remain));

            fs.Close();
            sock.Close();
        }
    }
}