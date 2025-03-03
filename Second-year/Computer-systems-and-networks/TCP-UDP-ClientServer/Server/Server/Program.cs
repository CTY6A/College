using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Linq;







namespace tcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var t = "0123456789ABCD\r\n";
            for (int i = 0; i < 16; i++)
                t += t;
            var reference = "";
            for (int i = 0; i < 40; i++)
                reference += t;

            reference += "End message";

            var stopwatch = new Stopwatch();

            var buffer = new byte[1024 * 4];
            var data = new StringBuilder();

            int size;
            var allsize = 0;


            //TCP PART/////////////////////
            Console.WriteLine("ТСP ПРОТОКОЛ:");
            Console.WriteLine("-------------");
            ///////////////////////////////
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);

            //Process.Start(@"Client.exe");

            var client = tcpSocket.Accept();
            client.ReceiveTimeout = 5;

            try
            {
                stopwatch.Stop();
                stopwatch.Reset();
                stopwatch.Start();

                do
                {
                    allsize += size = client.Receive(buffer);
                    data.Append(Encoding.Default.GetString(buffer, 0, size));
                }
                while (Encoding.Default.GetString(buffer, 0, size) != "End message");
            }
            catch { }
            stopwatch.Stop();
            ///////////////////////////

            //OUTPUT///////////////////
            using (StreamWriter sw = new StreamWriter(@"C:\Users\User\Desktop\1.txt", false, System.Text.Encoding.ASCII))
            {
                sw.Write(data);
            }

            Console.WriteLine("All Size: {0}Kb", allsize);

            Console.WriteLine("Milliseconds: {0}ms", stopwatch.Elapsed.Milliseconds);

            if (stopwatch.Elapsed.Milliseconds > 0)
            {
                var speed = allsize * 8 / stopwatch.Elapsed.Milliseconds;
                Console.Write("Speed: ");
                Console.Write(speed.ToString("#,#", CultureInfo.InvariantCulture)); Console.WriteLine("Kbps");
            }

            Console.Write("Результат проверки: ");
            if (reference.SequenceEqual(data.ToString()))
                Console.WriteLine("Трафик идентичен эталону");
            else
                Console.WriteLine("Трафик искажен");

            Console.WriteLine("Количество потерянных пакетов: {0}/256", (1048576 - allsize) / 4096);
            ///////////////////////////////

            //UDP PART/////////////////////  
            Console.WriteLine("");
            Console.WriteLine("UDP ПРОТОКОЛ:");
            Console.WriteLine("-------------");
            ///////////////////////////////          
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.ReceiveTimeout = 5;

            udpSocket.Bind(udpEndPoint);

            EndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8080);

            allsize = 0;

            client.Send(Encoding.Default.GetBytes("Сообщение получено"));

            /////////////////////////////////////
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            ////////////////////////////////////

            try
            {
                stopwatch.Stop();
                stopwatch.Reset();
                stopwatch.Start();

                do
                {
                    allsize += size = udpSocket.ReceiveFrom(buffer, ref clientEndPoint);
                    data.Append(Encoding.Default.GetString(buffer, 0, size));
                }
                while (Encoding.Default.GetString(buffer, 0, size) != "End message");
            }
            catch { }

            stopwatch.Stop();
            //////////////////////////////////

            //OUTPUT///////////////////////////
            using (StreamWriter sw = new StreamWriter(@"C:\Users\User\Desktop\2.txt", false, System.Text.Encoding.ASCII))
            {
                sw.Write(data);
            }

            Console.WriteLine("All Size: {0}Kb", allsize);

            Console.WriteLine("Milliseconds: {0}ms", stopwatch.Elapsed.Milliseconds);

            if (stopwatch.Elapsed.Milliseconds > 0)
            {
                var speed = allsize * 8 / stopwatch.Elapsed.Milliseconds;
                Console.Write("Speed: ");
                Console.Write(speed.ToString("#,#", CultureInfo.InvariantCulture)); Console.WriteLine("Kbps");
            }

            Console.Write("Результат проверки: ");
            if (reference.SequenceEqual(data.ToString()))
                Console.WriteLine("Трафик идентичен эталону");
            else
                Console.WriteLine("Трафик искажен");

            Console.WriteLine("Количество потерянных пакетов: {0}/256", (1048576 - allsize) / 4096);
            //////////////////////////////////

            Console.ReadLine();
        }
    }
}
