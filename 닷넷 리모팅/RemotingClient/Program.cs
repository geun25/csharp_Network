using GeneralLib;
using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace RemotingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpChannel hc = new HttpChannel();
            ChannelServices.RegisterChannel(hc, false);
            General gen = Activator.GetObject(typeof(General), "http://172.30.1.47:10400/MyRemote") as General;
            
            string str = gen.ConvertIntToStr(2);
            Console.WriteLine($"호출 결과:{str}");
            Console.ReadLine();

            str = gen.ConvertIntToStr(1);
            Console.WriteLine($"호출 결과:{str}");
            Console.ReadLine();

            str = gen.ConvertIntToStr(3);
            Console.WriteLine($"호출 결과:{str}");
            Console.ReadLine();

            str = gen.ConvertIntToStr(0);
            Console.WriteLine($"호출 결과:{str}");
            Console.ReadLine();
        }
    }
}
