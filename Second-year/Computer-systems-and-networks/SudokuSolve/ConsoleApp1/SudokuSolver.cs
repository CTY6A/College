using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    public struct Player
    {
        public EndPoint PlayerEndPoint;
        public string Nickname;
        public string Color;
        public string LastEntrance;
        public int Score;
    }
    class Server
    {
        public string Sudoku = new Sudoku().ToString();
        const string ip = "192.168.43.130";
        const int port = 80;
        EndPoint NewSenderEndPoint = new IPEndPoint(IPAddress.Any, 0);
        EndPoint udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        List<EndPoint> PlayersEndPoint = new List<EndPoint>();
        List<Player> Players = new List<Player>();
        Player CurPlayer;
        int CurPlayerId;
        public string[] message;
        EndPoint LastMove;
        int Score;
        public Server()
        {
            udpSocket.Bind(udpEndPoint);
            byte[] buffer = new byte[200];
            while(true)
            {
                EndPoint CurSenderEndPoint = NewSenderEndPoint;
                udpSocket.ReceiveFrom(buffer, ref CurSenderEndPoint);
                CurPlayerId = PlayersEndPoint.IndexOf(CurSenderEndPoint);
                if (CurPlayerId == -1)
                {
                    PlayersEndPoint.Add(CurSenderEndPoint);
                    Players.Add(new Player());
                    CurPlayerId = Players.Count - 1;
                }
                message = Encoding.Default.GetString(buffer).Split(' ');///////
                Console.WriteLine("");
                Console.WriteLine(Encoding.Default.GetString(buffer));
                buffer = new byte[200];
                Console.WriteLine(udpEndPoint.ToString());
                switch (message[0])
                {
                    case "Connected":
                        CurPlayer.PlayerEndPoint = CurSenderEndPoint;
                        CurPlayer.Nickname = message[1];
                        CurPlayer.Color = message[2];
                        CurPlayer.LastEntrance = DateTime.Now.ToString();
                        CurPlayer.Score = Players[CurPlayerId].Score;
                        Players[CurPlayerId] = CurPlayer;
                        PlayersEndPoint[CurPlayerId] = CurSenderEndPoint;
                        PlayersEndPoint.ForEach(CurSender =>
                        {
                            udpSocket.SendTo(Encoding.Default.GetBytes("Connected" + ' ' + CurPlayer.Nickname + ' ' + CurPlayer.Color + ' '), CurSender);
                            Console.WriteLine(CurSender);
                        });
                        udpSocket.SendTo(Encoding.Default.GetBytes("NewGame" + ' ' + Sudoku + ' '), CurPlayer.PlayerEndPoint);
                        break;

                    case "Disconnected":
                        PlayersEndPoint.ForEach(CurSender =>
                        {
                            if (!CurSender.Equals(CurSenderEndPoint))
                            {
                                udpSocket.SendTo(Encoding.Default.GetBytes("Disconnected" + ' ' + Players[CurPlayerId].Nickname + ' ' + Players[CurPlayerId].Color + ' '), CurSender);
                                Console.WriteLine(CurSender.ToString());
                            }
                        });
                        break;

                    case "Exit":
                        CurPlayer = Players[CurPlayerId];
                        Players.RemoveAt(CurPlayerId);
                        PlayersEndPoint.RemoveAt(CurPlayerId);
                        if (CurPlayer.Nickname != "")
                            PlayersEndPoint.ForEach(CurSender =>
                            {
                                udpSocket.SendTo(Encoding.Default.GetBytes("Exit" + ' ' + CurPlayer.Nickname + ' ' + CurPlayer.Color + ' '), CurSender);
                                Console.WriteLine(CurSender.ToString());
                            });
                        break;

                    case "EnterDigit":
                        int i = int.Parse(message[1]);
                        int j = int.Parse(message[2]);
                        try
                        {
                            checkValidity(message[3][0], j, i);
                            if (new Sudoku().Check(Sudoku))
                            {
                                if (LastMove == null || PlayersEndPoint.IndexOf(LastMove) != CurPlayerId)
                                {
                                    LastMove = PlayersEndPoint[CurPlayerId];
                                    Score = 0;
                                }
                                Score += 50;
                                CurPlayer.PlayerEndPoint = LastMove;
                                CurPlayer.Nickname = Players[CurPlayerId].Nickname;
                                CurPlayer.Color = Players[CurPlayerId].Color;
                                CurPlayer.LastEntrance = Players[CurPlayerId].LastEntrance;
                                CurPlayer.Score = Players[CurPlayerId].Score + Score;
                                Players[CurPlayerId] = CurPlayer;
                                PlayersEndPoint[CurPlayerId] = LastMove;
                                Sudoku = Sudoku.Remove(i * 9 + j, 1).Insert(i * 9 + j, message[3]);
                                PlayersEndPoint.ForEach(CurSender =>
                                {
                                    udpSocket.SendTo(Encoding.Default.GetBytes("EnterDigit" + ' ' + Players[CurPlayerId].Nickname + ' ' + Players[CurPlayerId].Color + ' ' + i + ' ' + j + ' ' + message[3] + ' ' + Score.ToString() + ' '), CurSender);
                                    Console.WriteLine(CurSender);
                                });
                                if (Sudoku.IndexOf('0') == -1)
                                {
                                    PlayersEndPoint.ForEach(CurSender =>
                                    {
                                        udpSocket.SendTo(Encoding.Default.GetBytes("EndGame" + ' '), CurSender);
                                        Console.WriteLine(CurSender);
                                    });
                                    Sudoku = new Sudoku().ToString();
                                    PlayersEndPoint.ForEach(CurSender =>
                                    {
                                        udpSocket.SendTo(Encoding.Default.GetBytes("NewGame" + ' ' + Sudoku + ' '), CurSender);
                                        Console.WriteLine(CurSender);
                                    });
                                }
                            }
                            else
                            {
                                throw new Exception("One");
                            }
                        }
                        catch (Exception e)
                        {
                            if (LastMove == PlayersEndPoint[CurPlayerId])
                            {
                                LastMove = null;
                            }
                            PlayersEndPoint.ForEach(CurSender =>
                            {
                                udpSocket.SendTo(Encoding.Default.GetBytes("Error" + ' ' + Players[CurPlayerId].Nickname + ' ' + Players[CurPlayerId].Color + ' ' + i + ' ' + j + ' ' + message[3] + ' ' + e.Message + ' '), CurSender);
                                Console.WriteLine(CurSender);
                            });
                        }
                        break;

                    case "GetStatistics":
                        Players.Sort((a, b) => a.Score.CompareTo(b.Score));
                        Players.ForEach(CurPlayer =>
                        {
                            udpSocket.SendTo(Encoding.Default.GetBytes("GetStatistics" + ' ' + CurPlayer.Nickname + ' ' + CurPlayer.Color + ' ' + CurPlayer.LastEntrance + ' ' + CurPlayer.Score.ToString() + ' '), CurSenderEndPoint);
                            Console.WriteLine(CurSenderEndPoint);
                        });
                        break;

                }
                Console.WriteLine("");
            }
        }

        public bool checkValidity(char val, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (Sudoku[y * 9 + i] == val)
                    throw new Exception("Vertical");
                else if (Sudoku[i * 9 + x] == val)
                    throw new Exception("Horizontal");
            }
            int startX = (x / 3) * 3;
            int startY = (y / 3) * 3;
            for (int i = startY; i < startY + 3; i++)
            {
                for (int j = startX; j < startX + 3; j++)
                {
                    if (Sudoku[i * 9 + j] == val)
                        throw new Exception("Square");
                }
            }
            return true;
        }

        public static void Main(String[] args)
        {
            new Server();
            Console.Read();
        }
    }
    class Sudoku
    {
        public bool OneSolution;
        public Random CurRandom = new Random();
        private char[] Grid;
        public string CurSudoku = "";
        public Sudoku()
        {
            CreateBase();
            if (CurRandom.Next(2) == 1)
            {
                Transposing();
            }
            for (int i = 0; i < CurRandom.Next(5); i++)
            {
                SwapColums(CurRandom.Next(3), CurRandom.Next(3), CurRandom.Next(3));
            }
            for (int i = 0; i < CurRandom.Next(5); i++)
            {
                SwapRows(CurRandom.Next(3), CurRandom.Next(3), CurRandom.Next(3));
            }
            for (int i = 0; i < CurRandom.Next(5); i++)
            {
                SwapColumsArea(CurRandom.Next(3), CurRandom.Next(3));
            }

            for (int i = 0; i < CurRandom.Next(5); i++)
            {
                SwapRowsArea(CurRandom.Next(3), CurRandom.Next(3));
            }
            Solve();
        }
        public bool Check(string s)
        {
            OneSolution = false;
            Grid = s.ToCharArray();
            try
            {
                PlaceNumber(0);
            }
            catch (Exception e)
            {
                if (e.Message == "Sudoku is wrong")
                {
                }
            }
            if (OneSolution)
                return true;
            else
                return false;
        }
        public void CreateBase()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CurSudoku += ((i * 3 + i / 3 + j) % 9 + 1).ToString();
                }
            }
        }
        public void Transposing()
        {
            string[] TimeSudoku = new string[9]; 
            for (int i = 0; i < 9; i++)
            {
                TimeSudoku[i] = CurSudoku.Substring(i * 9, 9);
            }
            CurSudoku = "";
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    CurSudoku += TimeSudoku[j][i];
                }
        }
        public void SwapRows(int Area, int Row1, int Row2)
        {
            if (Row1 != Row2)
            {
                string[] TimeSudoku = new string[9];
                for (int i = 0; i < 9; i++)
                {
                    TimeSudoku[i] = CurSudoku.Substring(i * 9, 9);
                }
                TimeSudoku[Area * 3 + Row1] += TimeSudoku[Area * 3 + Row2];
                TimeSudoku[Area * 3 + Row2] = TimeSudoku[Area * 3 + Row1].Substring(0, 9);
                TimeSudoku[Area * 3 + Row1] = TimeSudoku[Area * 3 + Row1].Substring(9);
                CurSudoku = "";
                for (int i = 0; i < 9; i++)
                {
                    CurSudoku += TimeSudoku[i];
                }
            }
        }
        public void SwapColums(int Area, int Row1, int Row2)
        {
            if (Row1 != Row2)
            {
                Transposing();
                SwapRows(Area, Row1, Row2);
                Transposing();
            }
        }
        public void SwapRowsArea(int Area1, int Area2)
        {
            if (Area1 != Area2)
            {
                string PartArea1 = CurSudoku.Substring(Area1 * 9 * 3, 9 * 3);
                string PartArea2 = CurSudoku.Substring(Area2 * 9 * 3, 9 * 3);
                CurSudoku = CurSudoku.Remove(Area1 * 9 * 3, 9 * 3).Insert(Area1 * 9 * 3, PartArea2)
                                     .Remove(Area2 * 9 * 3, 9 * 3).Insert(Area2 * 9 * 3, PartArea1);
            }
        }
        public void SwapColumsArea(int Area1, int Area2)
        {
            if (Area1 != Area2)
            {
                Transposing();
                SwapRowsArea(Area1, Area2);
                Transposing();
            }
        }
        public void Solve()
        {
            bool[] Views = new bool[81];
            bool SudokuIsWrong;
            int Position;
            string CurValue;
            int EmptyCells = 0;
            do
            {
                while(Views[Position = CurRandom.Next(81)])
                {
                }
                Views[Position] = true;
                CurValue = CurSudoku[Position].ToString();
                Grid = (CurSudoku = CurSudoku.Remove(Position, 1).Insert(Position, "0")).ToCharArray();
                OneSolution = false;
                SudokuIsWrong = false;
                try
                {
                    PlaceNumber(0);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sudoku is wrong")
                    {
                        SudokuIsWrong = true;
                    }
                }
                if (OneSolution && !SudokuIsWrong)
                {
                    EmptyCells++;
                }
                if (!OneSolution || SudokuIsWrong)
                {
                    CurSudoku = CurSudoku.Remove(Position, 1).Insert(Position, CurValue);
                }
            }
            while (!OneSolution || SudokuIsWrong || EmptyCells < 1);
            Grid = CurSudoku.ToCharArray();
        }

        public void PlaceNumber(int pos)
        {
            if (pos == 81)
            {
                if (!OneSolution)
                {
                    OneSolution = true;
                }
                else
                {
                    throw new Exception("Sudoku is wrong");
                }
            }
            else
            {
                if (Grid[pos] > '0')
                {
                    PlaceNumber(pos + 1);
                }
                else
                {
                    for (char Candidate = '1'; Candidate <= '9'; Candidate++)
                    {
                        if (checkValidity(Candidate, pos % 9, pos / 9))
                        {
                            Grid[pos] = Candidate;
                            PlaceNumber(pos + 1);
                            Grid[pos] = '0';
                        }
                    }
                }
            }
        }

        public bool checkValidity(char val, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (Grid[y * 9 + i] == val || Grid[i * 9 + x] == val)
                    return false;
            }
            int startX = (x / 3) * 3;
            int startY = (y / 3) * 3;
            for (int i = startY; i < startY + 3; i++)
            {
                for (int j = startX; j < startX + 3; j++)
                {
                    if (Grid[i * 9 + j] == val)
                        return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return CurSudoku;
        }
    }
}
