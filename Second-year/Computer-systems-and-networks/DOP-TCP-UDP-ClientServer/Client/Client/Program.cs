using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Diagnostics;

namespace tcpClient
{
    class Program
    {

        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 37;

            var TimeNow = new byte[8];
            var dummy = new byte[1];
            Process.Start(@"Server.exe");

            //TCP PART/////////////////////
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Connect(tcpEndPoint);
            
            tcpSocket.Receive(TimeNow);
            ////////////////////////////////

            Console.WriteLine(DateTime.FromBinary(BitConverter.ToInt64(TimeNow, 0)));
            
            //UDP PART/////////////////////
            EndPoint udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.SendTo(dummy, udpEndPoint);
            udpSocket.ReceiveFrom(TimeNow, ref udpEndPoint);
            ////////////////////////////////

            Console.WriteLine(DateTime.FromBinary(BitConverter.ToInt64(TimeNow, 0)));

            Console.ReadLine();
        }
    }
}