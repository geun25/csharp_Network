using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace auth_server
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpChannel hc = new HttpChannel(10800);
            ChannelServices.RegisterChannel(hc, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(EHAAALib.EHAAA), "AAASVC", WellKnownObjectMode.Singleton);
            Console.ReadKey();
        }
    }
}
