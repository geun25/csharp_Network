using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.Write("전송할 파일명:");
            string fname = Console.ReadLine();
            fsc.SendAsync(fname);

            Console.ReadKey();
        }
    }
}
