using GeneralLib;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace RemotingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpChannel hc = new HttpChannel(10400); // TCP : BinaryFormatter / HTTP: SoapFormatter
            ChannelServices.RegisterChannel(hc, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(General), "MyRemote", WellKnownObjectMode.Singleton);
            Console.ReadKey();
        }
    }
}
