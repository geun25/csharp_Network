using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer
{
    class Program
    {
        static void Main(string[] args)
        {
            FileRecvServ fs = new FileRecvServ("172.30.1.13", 10340);
            fs.AcceptedEventHandler += Fs_AcceptedEventHandler;
            fs.ClosedEventHandler += Fs_ClosedEventHandler;
            fs.RecvFileNameEventHandler += Fs_RecvFileNameEventHandler;
            fs.FileLengthRecvEventHandler += Fs_FileLengthRecvEventHandler;
            fs.FileDataRecvEventHandler += Fs_FileDataRecvEventHandler;
            fs.Start();
            Console.ReadKey();
        }

        static long length;
        static FileStream fs;
        private static void Fs_FileDataRecvEventHandler(object sender, FileDataRecvEventArgs e)
        {
            Console.WriteLine($"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}에서 {e.FileName}남은 길이:{e.RemainLength}");
            fs.Write(e.Data, 0, e.Data.Length);
            if (e.RemainLength == 0)
                fs.Close();
        }

        private static void Fs_FileLengthRecvEventHandler(object sender, FileLengthRecvEventArgs e)
        {
            Console.WriteLine($"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}에서 {e.FileName}길이:{e.Length}");
            length = e.Length;
        }

        private static void Fs_RecvFileNameEventHandler(object sender, RecvFileNameEventArgs e)
        {
            Console.WriteLine($"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}에서 {e.FileName}전송 시작");
            fs = File.Create(e.FileName);
        }

        private static void Fs_ClosedEventHandler(object sender, ClosedEventArgs e)
        {
            Console.WriteLine($"{e.IPStr}:{e.Port}와 연결 해제");
        }

        private static void Fs_AcceptedEventHandler(object sender, AcceptedEventArgs e)
        {
            Console.WriteLine($"{e.IPStr}:{e.Port}와 연결");
        }
    }
}
