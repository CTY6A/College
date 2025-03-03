using System;
using System.Net;
using System.Net.Sockets;

namespace tcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 37;

            var TimeNow = BitConverter.GetBytes(DateTime.Now.ToBinary());
            var dummy = new byte[1];
            
            //TCP INITIALIZATION//
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            EndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            //////////////////////

            //UDP INITIALIZATION//
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);
            //////////////////////

            //TCP//
            tcpSocket.Listen(1);
            var client = tcpSocket.Accept();

            client.Send(TimeNow);

            client.Shutdown(SocketShutdown.Both);
            client.Close();
            ///////

            TimeNow = BitConverter.GetBytes(DateTime.Now.ToBinary());

            //UDP//
            udpSocket.ReceiveFrom(dummy, ref clientEndPoint);

            udpSocket.SendTo(TimeNow, clientEndPoint);
            ////////
        }
    }
}
