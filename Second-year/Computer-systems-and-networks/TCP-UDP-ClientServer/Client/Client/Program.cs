using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var message = "0123456789ABCD\r\n";
            for (int i = 0; i < 8; i++)
                message += message;

            var data = Encoding.Default.GetBytes(message);

            //TCP PART/////////////////////
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            tcpSocket.Connect(tcpEndPoint);

            for (int i = 0; i < 256 * 40; i++)
              tcpSocket.Send(data);

            tcpSocket.Send(Encoding.Default.GetBytes("End message"));
            tcpSocket.Receive(data);
            ////////////////////////////////

            //UDP PART/////////////////////
            EndPoint udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            for (int i = 0; i < 256 * 40; i++)
                udpSocket.SendTo(data, udpEndPoint);

            udpSocket.SendTo(Encoding.Default.GetBytes("End message"), udpEndPoint);
            ////////////////////////////////
        }
    }
}