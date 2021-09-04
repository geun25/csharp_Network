using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace echo_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = null;
            try
            {
                // 소켓 생성
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 인터페이스와 결합
                IPAddress addr = IPAddress.Parse("211.226.39.241");
                IPEndPoint iep = new IPEndPoint(addr, 10040);
                sock.Bind(iep);

                // 백로그 큐 크기 설정
                sock.Listen(5);

                // AcceptLoop
                Socket dosock;
                while (true)
                {
                    dosock = sock.Accept();
                    DoItAsync(dosock);
                }
            }

            catch
            {

            }
            finally
            {
                // 소켓 닫기
                sock.Close();
            }
        }

        // 대리자 설정
        delegate void DoItDele(Socket dosock);

        private static void DoItAsync(Socket dosock)
        {
            DoItDele dele = DoIt;
            dele.BeginInvoke(dosock, null, null); // 비동기 실행
        }

        private static void DoIt(Socket dosock)
        {
            try
            {
                byte[] packet = new byte[1024];
                IPEndPoint iep = dosock.RemoteEndPoint as IPEndPoint;
                while (true)
                {
                    dosock.Receive(packet);
                    MemoryStream ms = new MemoryStream(packet);
                    BinaryReader br = new BinaryReader(ms);
                    string msg = br.ReadString();
                    br.Close();
                    ms.Close();

                    Console.WriteLine($"{iep.Address}:{iep.Port} -> {msg}");
                    if (msg == "exit")
                        break;
                    dosock.Send(packet);
                }
            }
            catch
            {

            }
            finally
            {
                dosock.Close();
            }
        }
    }
}