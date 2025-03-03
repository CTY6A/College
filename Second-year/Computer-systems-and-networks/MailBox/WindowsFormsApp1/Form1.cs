using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Pop3 pop3 = new Pop3();
        public string username = "123456@mail.ru";
        public string password = "**********"; 

        public Form1()
        {
            InitializeComponent();
        }

        private void PostMessage(string instruction, string request)
        {
            TextBox.Text += "Client: " + instruction + " " + request + "\r\n";
            if (request == "")
            {
                pop3.PostMessage(instruction + "\r\n");
            }
            else
            {
                pop3.PostMessage(instruction + " " + request + "\r\n");
            }
            TextBox.Text += "Server: ";
            pop3.GetMessage(TextBox);
            if (instruction == "RETR")
            {
                pop3.GetMessage(TextBox);
            }
            TextBox.SelectionStart = TextBox.Text.Length;
            TextBox.ScrollToCaret();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            pop3.Connect();
            TextBox.Text += "Server: ";
            pop3.GetMessage(TextBox);
            TextBox.SelectionStart = TextBox.Text.Length;
            TextBox.ScrollToCaret();
        }

        private void BtnAuthorizate_Click(object sender, EventArgs e)
        {
            PostMessage("USER", username);
            PostMessage("PASS", password);
        }        

        private void BtnStat_Click(object sender, EventArgs e)
        {
            PostMessage("STAT", "");
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            PostMessage("QUIT", "");
            pop3.Close();
        }

        private void BtnRetr_Click(object sender, EventArgs e)
        {
            PostMessage("RETR", TextBoxMessage.Text);
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            PostMessage("LIST", TextBoxList.Text);
        }

        private void BtnNoop_Click(object sender, EventArgs e)
        {
            PostMessage("NOOP", "");
        }
    }

    public class Pop3
    {
        private readonly int port = 995;
        private readonly string pop3Server = "pop.mail.ru";
        private TcpClient tcpClient;
        public SslStream sslStream;
        public void Connect()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(pop3Server, port);
            sslStream = new SslStream(tcpClient.GetStream());
            sslStream.AuthenticateAsClient(pop3Server);
        }
        public void GetMessage(TextBox TextBox)
        {
            byte[] buffer = new byte[512];
            int bytes = sslStream.Read(buffer, 0, buffer.Length);
            TextBox.Text += Encoding.UTF8.GetString(buffer, 0, bytes);
            if (bytes == buffer.Length)
                while (!Encoding.UTF8.GetString(buffer, 0, bytes).Contains("\r\n.\r\n"))
                {
                    bytes = sslStream.Read(buffer, 0, buffer.Length);
                    TextBox.Text += Encoding.UTF8.GetString(buffer, 0, bytes);
                }
        }
        public void PostMessage(string request)
        {
            sslStream.Write(Encoding.UTF8.GetBytes(request));
        }
        public void Close()
        {
            sslStream.Close();
            tcpClient.Close();
        }
    }
}
