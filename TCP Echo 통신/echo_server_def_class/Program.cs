using System;

namespace echo_server_class
{
    class Program
    {
        static void Main(string[] args)
        {
            EchoServer es = new EchoServer("211.226.39.241", 10248);
            es.RecvedMsgEventHandler += Es_RecvedMsgEventHandler;
            es.AcceptedEventHandler += Es_AcceptedEventHandler;
            es.ClosedEventHandler += Es_ClosedEventHandler;
            if (es.Start() == false)
            {
                Console.WriteLine("서버 가동 실패");
                return;
            }
            Console.ReadKey();
        }

        private static void Es_ClosedEventHandler(object sender, ClosedEventArgs e)
        {
            Console.WriteLine($"{e.IPStr}:{e.Port}에서 연결을 닫음");
        }

        private static void Es_AcceptedEventHandler(object sender, AcceptedEventArgs e)
        {
            Console.WriteLine($"{e.IPStr}:{e.Port}에서 연결 했음");
        }

        private static void Es_RecvedMsgEventHandler(object sender, RecvedMsgEventArgs e)
        {
            Console.WriteLine($"{e.IPStr}:{e.Port}->{e.Msg}");
        }
    }   
}
