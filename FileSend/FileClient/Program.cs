using System;

namespace FileClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("서버IP:");
            string ip = Console.ReadLine();

            int port = 10340;
            Console.Write($"포트번호:{port}");

            FileSendClient fsc = new FileSendClient(ip, port);
            fsc.SendFileDataEventHandler += Fsc_SendFileDataEventHandler;

            Console.Write("전송할 파일명:");
            string fname = Console.ReadLine();
            fsc.SendAsync(fname);

            Console.ReadKey(); // 프로그램 바로 끝나지 않게 하기 위함
        }

        private static void Fsc_SendFileDataEventHandler(object sender, SendFileDataEventArgs e)
        {
            Console.WriteLine($"{e.FileName}파일 {e.Remain}bytes 남았음");
        }
    }
}

