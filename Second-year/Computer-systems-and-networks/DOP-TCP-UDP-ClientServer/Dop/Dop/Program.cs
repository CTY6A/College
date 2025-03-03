using System;
using System.Net;
using System.Net.Sockets;

namespace Dop
{
    class Program
    {
        static void Main(string[] args)
        {
            var ntpServer = "time.windows.com";

            byte[] ntpData = new byte[48]; // RFC 2030 

            ntpData[0] = 0x1B;
            for (int i = 1; i < 48; i++)
                ntpData[i] = 0;


            IPAddress[] address = Dns.GetHostEntry(ntpServer).AddressList;

            if (address == null || address.Length == 0)
                throw new ArgumentException("Could not resolve ip address from '" + ntpServer + "'.", "ntpServer");


            IPEndPoint udpEndPoint = new IPEndPoint(address[0], 123);
            Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.Connect(udpEndPoint);
            udpSocket.Send(ntpData);
            udpSocket.Receive(ntpData);
            udpSocket.Close();


            byte offsetTransmitTime = 40;
            ulong intpart = 0;
            ulong fractpart = 0;

            for (int i = 0; i <= 3; i++)
                intpart = 256 * intpart + ntpData[offsetTransmitTime + i];

            for (int i = 4; i <= 7; i++)
                fractpart = 256 * fractpart + ntpData[offsetTransmitTime + i];
           
            ulong milliseconds = (intpart * 1000 + (fractpart * 1000) / 0x100000000L);

            TimeSpan timeSpan = TimeSpan.FromTicks((long)milliseconds * TimeSpan.TicksPerMillisecond);
      
            DateTime dateTime = new DateTime(1900, 1, 1);

            dateTime += timeSpan;

            TimeSpan offsetAmount = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
            DateTime networkDateTime = (dateTime + offsetAmount);

            Console.WriteLine(networkDateTime);
            Console.ReadLine();
        }
    }
}
