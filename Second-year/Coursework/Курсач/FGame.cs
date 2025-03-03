using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсач
{
    public partial class FGame : Form
    {
        public string PlayerNickname { get; set; }
        public Color PlayerColor { get; set; }
        const string ip = "192.168.43.130";
        const int port = 80;
        public EndPoint ServerEndPoint { get; set; } = new IPEndPoint(IPAddress.Parse(ip), port);
        public Socket udpSocket { get; set; } = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        string[] message;
        public Task RecipientTask;
        public bool UserKeyPress = false;
        public FGame()
        {
            InitializeComponent();

            DGVMainArea.AllowUserToAddRows = false;
            DGVMainArea.AllowUserToDeleteRows = false;
            DGVMainArea.AllowUserToResizeColumns = false;
            DGVMainArea.AllowUserToResizeRows = false;
            DGVMainArea.RowHeadersVisible = false;
            DGVMainArea.ColumnHeadersVisible = false;
            DGVMainArea.ScrollBars = ScrollBars.None;
            DGVMainArea.GridColor = Color.Black;
            DGVMainArea.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DGVMainArea.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVMainArea.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVMainArea.ColumnCount = 9;
            DGVMainArea.Rows.Add(9);
            DGVMainArea.MultiSelect = false;
            for (int i = 0; i < 9; i++)
            {
                DGVMainArea.Columns[i].Width = DGVMainArea.Width / 9;
                DGVMainArea.Rows[i].Height = DGVMainArea.Height / 9;
            }
            DGVMainArea.Width = DGVMainArea.Columns[1].Width * 9 + 3;
            RecipientTask = new Task(Recipient);
        }
        private void FGame_Load(object sender, EventArgs e)
        {
            RTBConsole.Text = "";
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (((i == 3) | (i == 4) | (i == 5)) & !((j == 3) | (j == 4) | (j == 5)) |
                       !((i == 3) | (i == 4) | (i == 5)) & ((j == 3) | (j == 4) | (j == 5)))
                        DGVMainArea.Rows[i].Cells[j].Style.BackColor = PlayerColor;
            if (RecipientTask.Status.GetHashCode() != 3)
            {
                RecipientTask.Start();
            }
            udpSocket.SendTo(Encoding.Default.GetBytes("Connected" + ' ' + PlayerNickname + ' ' + PlayerColor.Name + ' '), ServerEndPoint);
        }
        private void BExit_Click(object sender, EventArgs e)
        {
            udpSocket.SendTo(Encoding.Default.GetBytes("Disconnected" + ' '), ServerEndPoint);
            Hide();
        }
        private void DGVMainArea_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            tb.MaxLength = 1;
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back) || (e.KeyChar == '0'))
            {
                e.Handled = true; 
            }
            else
            {
                UserKeyPress = true;
            }
        }

        private void Recipient()
        {
            while (true)
            {
                EndPoint CurSenderEndPoint = ServerEndPoint;
                byte[] buffer = new byte[200];
                try
                {
                    udpSocket.ReceiveFrom(buffer, ref CurSenderEndPoint);
                }
                catch (Exception e)
                {
                    if (e.HResult != 10004)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                if (CurSenderEndPoint == ServerEndPoint)
                {
                    message = Encoding.Default.GetString(buffer).Split(' ');
                    while (!RTBConsole.IsHandleCreated || !DGVMainArea.IsHandleCreated)
                    {
                    }
                    switch (message[0])
                    {
                        case "Connected":
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText(message[1] + " подключился!" + '\n', Color.FromName(message[2]));
                            });
                            break;

                        case "Disconnected":
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText(message[1] + " отключился." + '\n', Color.FromName(message[2]));
                            });
                            break;

                        case "Exit":
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText(message[1] + " вышел." + '\n', Color.FromName(message[2]));
                            });
                            break;

                        case "NewGame":
                            DGVMainArea.Invoke((MethodInvoker)delegate
                            {
                                for (int i = 0; i < 9; i++)
                                    for (int j = 0; j < 9; j++)
                                        if (message[1][i * 9 + j] != '0')
                                        {
                                            DGVMainArea.Rows[j].Cells[i].Value = message[1][i * 9 + j];
                                            DGVMainArea.Rows[j].Cells[i].Style.ForeColor = Color.Black;
                                            DGVMainArea.Rows[j].Cells[i].ReadOnly = true;
                                        }
                                        else
                                        {
                                            DGVMainArea.Rows[j].Cells[i].Value = ' ';
                                            DGVMainArea.Rows[j].Cells[i].Style.ForeColor = Color.Black;
                                            DGVMainArea.Rows[j].Cells[i].ReadOnly = false;
                                        }
                            });
                            break;

                        case "EnterDigit":
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText(message[1] + " изменил значение ячейки (" + message[3] + ", " + message[4] + ") на " + message[5] + " и получил за это " + message[6] + " очков." + '\n', Color.FromName(message[2]));
                            });
                            DGVMainArea.Invoke((MethodInvoker)delegate
                            {
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Value = message[5];
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Style.ForeColor = Color.FromName(message[2]) == PlayerColor ? Color.Black : Color.FromName(message[2]);
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].ReadOnly = true;
                            });
                            break;

                        case "Error":
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText(message[1] + " изменил значение ячейки (" + message[3] + ", " + message[4] + ") на " + message[5] + " и ошибся." + '\n', Color.FromName(message[2]));
                            });
                            DGVMainArea.Invoke((MethodInvoker)delegate
                            {
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Value = message[5];
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Style.ForeColor = Color.FromName(message[2]);
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].ReadOnly = true;
                            });
                            switch (message[6])
                            {
                                case "One":
                                    DGVMainArea.Invoke((MethodInvoker)delegate
                                    {
                                        DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Style.BackColor = Color.Red;
                                    });
                                    break;

                                case "Horizontal":
                                    DGVMainArea.Invoke((MethodInvoker)delegate
                                    {
                                        for (int i = 0; i < 9; i++)
                                            DGVMainArea.Rows[int.Parse(message[4])].Cells[i].Style.BackColor = Color.Red;
                                    });
                                    break;

                                case "Vertical":
                                    DGVMainArea.Invoke((MethodInvoker)delegate
                                    {
                                        for (int i = 0; i < 9; i++)
                                            DGVMainArea.Rows[i].Cells[int.Parse(message[3])].Style.BackColor = Color.Red;
                                    });
                                    break;

                                case "Square":
                                    DGVMainArea.Invoke((MethodInvoker)delegate
                                    {
                                        for (int i = 0; i < 3; i++)
                                            for (int j = 0; j < 3; j++)
                                                DGVMainArea.Rows[int.Parse(message[4]) / 3 * 3 + i].Cells[int.Parse(message[3]) / 3 * 3 + j].Style.BackColor = Color.Red;
                                    });
                                    break;
                            }
                            Thread.Sleep(1000);
                            DGVMainArea.Invoke((MethodInvoker)delegate
                            {
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Value = ' ';
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].Style.ForeColor = Color.Black;
                                DGVMainArea.Rows[int.Parse(message[4])].Cells[int.Parse(message[3])].ReadOnly = false;
                            });
                            for (int i = 0; i < 9; i++)
                                for (int j = 0; j < 9; j++)
                                    if (((i == 3) | (i == 4) | (i == 5)) & !((j == 3) | (j == 4) | (j == 5)) |
                                       !((i == 3) | (i == 4) | (i == 5)) & ((j == 3) | (j == 4) | (j == 5)))
                                        DGVMainArea.Rows[i].Cells[j].Style.BackColor = PlayerColor;
                                    else
                                        DGVMainArea.Rows[i].Cells[j].Style.BackColor = Color.White;
                            break;

                        case "GetStatistics":
                            RTBStatistics.Invoke((MethodInvoker)delegate
                            {
                                RTBStatistics.AppendText(message[1] + " был онлайн:" + message[3] + ' ' + message[4] + ", заработал очков:" + message[5] + '.' + '\n', Color.FromName(message[2]));
                            });
                            break;

                        case "EndGame":
                            DGVMainArea.Invoke((MethodInvoker)delegate
                            {
                                DGVMainArea.ReadOnly = true;
                            });
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText("Игра закончилась. Новая игра начнется через:\n", PlayerColor);
                            });
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText("5\n", PlayerColor);
                            });
                            Thread.Sleep(1000);
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText("4\n", PlayerColor);
                            });
                            Thread.Sleep(1000);
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText("3\n", PlayerColor);
                            });
                            Thread.Sleep(1000);
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText("2\n", PlayerColor);
                            });
                            Thread.Sleep(1000);
                            RTBConsole.Invoke((MethodInvoker)delegate
                            {
                                RTBConsole.AppendText("1\n", PlayerColor);
                            });
                            Thread.Sleep(1000);
                            DGVMainArea.Invoke((MethodInvoker)delegate
                            {
                                DGVMainArea.ReadOnly = false;
                            });
                            break;
                    }
                }
            }
        }

        private void DGVMainArea_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (UserKeyPress)
            {
                udpSocket.SendTo(Encoding.Default.GetBytes(
                    "EnterDigit" + ' ' +
                    e.ColumnIndex.ToString() + ' ' +
                    e.RowIndex.ToString() + ' ' +
                    DGVMainArea.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + ' '),
                    ServerEndPoint);
                UserKeyPress = false;
            }
        }

        private void BRefreshStatistics_Click(object sender, EventArgs e)
        {
            RTBStatistics.Text = "";
            udpSocket.SendTo(Encoding.Default.GetBytes("GetStatistics" + ' '), ServerEndPoint);
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
