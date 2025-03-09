using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSP_3
{
    public partial class MainForm : Form
    {
        Series DataSer_1, DataSer_2, DataSer_3, DataSer_4, DataSer_5;
        enum SignalType { Harmonic,Polyharmonic}

        public MainForm()
        {
            InitializeComponent();
            chart1.SetBounds(10, 10, this.Size.Width - 180, (this.Size.Height - 70) / 3);
            chart2.SetBounds(10, (this.Size.Height - 70) / 3 + 20, this.Size.Width - 180, (this.Size.Height - 70) / 3);
            chart3.SetBounds(10, 2 * (this.Size.Height - 70) / 3 + 30, this.Size.Width - 180, (this.Size.Height - 70) / 3);
            Calculate(SignalType.Harmonic,1);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            chart1.SetBounds(10, 10, this.Size.Width - 180, (this.Size.Height - 70) / 3);
            chart2.SetBounds(10,(this.Size.Height - 70) / 3+20,this.Size.Width - 180, (this.Size.Height - 70) / 3);
            chart3.SetBounds(10, 2*(this.Size.Height - 70) / 3 + 30, this.Size.Width - 180, (this.Size.Height - 70) / 3);
        }

        private void Calculate(SignalType st, int freq)
        {
            Signal hs;
            if (st == SignalType.Harmonic)
            {
                hs = new HarmonicSignal(10, freq, Math.PI/4, 360);
            }
            else
            {
                double[] A = new double[7] { 1, 3, 5, 8, 10, 12, 16 };
                double[] ph = new double[6] { Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2, Math.PI, 3 * Math.PI / 4 };
                hs = new PolyharmonicSignal(A, freq, ph, 360);
            }
            chart1.Series.Clear();
            chart1.Legends.Clear();
            DataSer_1 = new Series();
            Legend l1 = new Legend();
            chart1.Legends.Add(l1);
            l1.Title = "Сигналы";
            DataSer_1.ChartType = SeriesChartType.Spline;
            DataSer_1.Color = Color.Red;
            DataSer_1.Name = "Исходный сигнал";
            DataSer_4 = new Series();
            DataSer_4.ChartType = SeriesChartType.Spline;
            DataSer_4.Color = Color.Blue;
            DataSer_4.Name = "Восстановленный сигнал";
            DataSer_5 = new Series();
            DataSer_5.ChartType = SeriesChartType.Spline;
            DataSer_5.Color = Color.Yellow;
            DataSer_5.Name = "Восстановленный сигнал \r\nбез учета фазы";
            for (int i = 0; i <= 359; i++)
            {
                DataSer_1.Points.AddXY(2 * Math.PI * i / 360, hs.signVal[i]);
                DataSer_4.Points.AddXY(2 * Math.PI * i / 360, hs.restoredSignal[i]);
                DataSer_5.Points.AddXY(2 * Math.PI * i / 360, hs.restorednonphasedSignal[i]);
            }
            chart1.ResetAutoValues();
            chart1.Series.Add(DataSer_1);
            chart1.Series.Add(DataSer_4);
            chart1.Series.Add(DataSer_5);
            chart2.Series.Clear();
            chart3.Series.Clear();
            chart2.Legends.Clear();
            chart3.Legends.Clear();
            Legend l2 = new Legend();
            chart2.Legends.Add(l2);
            Legend l3 = new Legend();
            chart3.Legends.Add(l3);
            DataSer_2 = new Series();
            DataSer_2.ChartType = SeriesChartType.Candlestick;
            DataSer_2.Color = Color.Red;
            DataSer_2.Name = "Фазовый спектр";
            DataSer_3 = new Series();
            DataSer_3.ChartType = SeriesChartType.Candlestick;
            DataSer_3.Color = Color.Green;
            DataSer_3.Name = "Амплитудный спектр";
            for (int i = 0; i <= 49; i++)
            {
                DataSer_2.Points.AddXY(i, hs.phaseSp[i]);
                DataSer_3.Points.AddXY(i, hs.amplSp[i]);
            }
            chart2.ResetAutoValues();
            chart2.Series.Add(DataSer_2);
            chart3.ResetAutoValues();
            chart3.Series.Add(DataSer_3);
        }

        private void radioButton1_Checked(object sender, EventArgs e)
        {
            Calculate(SignalType.Harmonic,trackBar1.Value);
        }

        private void radioButton2_Checked(object sender, EventArgs e)
        {
            Calculate(SignalType.Polyharmonic, trackBar1.Value);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Частота: " + Convert.ToString(trackBar1.Value);
            if (radioButton1.Checked)
            {
                Calculate(SignalType.Harmonic, trackBar1.Value);
            }
            else
            {
                Calculate(SignalType.Polyharmonic, trackBar1.Value);
            }
        }
    }
}
