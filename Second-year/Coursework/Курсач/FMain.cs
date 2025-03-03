using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Курсач
{
    public partial class FMain : Form
    {
        public string PlayerNickname = "Player";
        public Color PlayerColor = Color.Green;
        FAuthorization PlayerInfo = new FAuthorization();
        FGame Game = new FGame();
        const string ip = "127.0.0.1";
        const int port = 80;
        public FMain()
        {
            InitializeComponent();
        }
        private void BPlay_Click(object sender, EventArgs e)
        {   
            Game.PlayerNickname = PlayerNickname;
            Game.PlayerColor = PlayerColor;
            Hide();
            Game.ShowDialog();
            Show();
        }
        private void BChangePlayer_Click(object sender, EventArgs e)
        {
            PlayerInfo.PlayerNickname = PlayerNickname;
            PlayerInfo.PlayerColor = PlayerColor;
            PlayerInfo.ShowDialog();
            PlayerNickname = PlayerInfo.PlayerNickname;
            PlayerColor = PlayerInfo.PlayerColor;
        }
        private void BStatistics_Click(object sender, EventArgs e)
        {

        }
        private void BExit_Click(object sender, EventArgs e)
        {
            Game.udpSocket.SendTo(Encoding.Default.GetBytes("Exit" + ' '), Game.ServerEndPoint);
            Game.udpSocket.Shutdown(SocketShutdown.Both);
            Game.udpSocket.Close();
            Close();
        }
    }
}
