using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab5
{

    public partial class Form1 : Form
    {
        private const int time = 10000;
        private const int n = 2;
        private int m;
        private const int Lambda = 1;
        private const double Mu = 0.5;
        private const double w = 2;

        private int QueueCount;
        private double CurrentTime;
        private double TimeWhenNewRequestComes;
        private List<double?> FinishTimeList;

        private int QueueCountAll;
        private int QueueCountCount;

        private int GeneratedRequests;
        private int DroppedRequests;

        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            m = trackBar1.Value;
            FinishTimeList = new List<double?>();

            for (int i = 0; i < n; i++)
            {
                FinishTimeList.Add(null);
            }

            Run();

            double Potk = Math.Pow(w, n + m) / (Math.Pow(n, m) * 2) * Math.Pow(1 + w + Math.Pow(w, 2) / 2 + m * Math.Pow(n, n) / 2, -1);
            double Loch = m * (m + 1) * Math.Pow(1 + w + Math.Pow(w, 2) / 2 + m * Math.Pow(n, n) / 2, -1);

            label13.Text = ((double) QueueCountAll / (QueueCountCount)).ToString();
            label14.Text = Loch.ToString();
            label15.Text = ((double)DroppedRequests / GeneratedRequests).ToString();
            label2.Text = Potk.ToString();

            label3.Text = label3.Text.Substring(0, label3.Text.Length - 1) + m;
        }

        public void Run()
        {
            QueueCount = 0;
            CurrentTime = 0;

            QueueCountAll = 0;
            QueueCountCount = 0;

            GeneratedRequests = 0;
            DroppedRequests = 0;

            while (CurrentTime < time)
            {
                TimeWhenNewRequestComes = CurrentTime + SimpleStream.Next(Lambda);

                var nextProcessedIndex = NextRequestProcessedIndex();

                if (FinishTimeList[nextProcessedIndex] < TimeWhenNewRequestComes)
                {
                    CurrentTime = FinishTimeList[nextProcessedIndex].Value;
                    if (QueueCount > 0)
                    {
                        QueueCount--;
                        FinishTimeList[nextProcessedIndex] = CurrentTime + SimpleStream.Next(Mu);
                    }
                    else
                    {
                        FinishTimeList[nextProcessedIndex] = null;
                    }
                }
                else
                {
                    CurrentTime = TimeWhenNewRequestComes;
                    GeneratedRequests++;
                    var freeIndex = FreeConsumerIndex();
                    if (freeIndex == -1)
                    {
                        if (QueueCount >= m)
                        {
                            DroppedRequests++;
                        }
                        else
                        {
                            QueueCount++;
                        }
                    }
                    else
                    {
                        FinishTimeList[freeIndex] = CurrentTime + SimpleStream.Next(Mu);
                    }
                }

                QueueCountAll += QueueCount;
                QueueCountCount++;
            }
        }

        private int FreeConsumerIndex()
        {
            return FinishTimeList.IndexOf(null);
        }

        private int NextRequestProcessedIndex()
        {
            var minIndex = 0;
            for (int i = 1; i < FinishTimeList.Count(); i++)
            {
                if (FinishTimeList[i] < FinishTimeList[minIndex])
                {
                    minIndex = i;
                }
            }
            return minIndex;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            trackBar1_ValueChanged(sender, e);
        }
    }


    class SimpleStream
    {
        private static readonly Random random = new Random();

        public static double Next(double lambda)
        {
            return -1 * Math.Log(random.NextDouble()) / lambda;
        }
    }

   
}
