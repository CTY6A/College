using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace trDSP
{
    public partial class DSP_2 : Form
    {
        System.Windows.Forms.DataVisualization.Charting.Series DataSer_1;
        System.Windows.Forms.DataVisualization.Charting.Series DataSer_2;
        int K, phi;
        double[] phc = new double[15];
        
        public DSP_2()
        {
            InitializeComponent();
            BuildGraph();
        }

        private void BuildGraph()
        {
            chart1.Series.Clear();
            DataSer_1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataSer_1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            DataSer_1.Color = Color.Red;
            DataSer_2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataSer_2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            DataSer_2.Color = Color.Blue;
            int N = 1024;
            K = (int)trackBar3.Value;
            phi = (int)trackBar1.Value;
            label5.Text = Convert.ToString(trackBar3.Value);
            label6.Text = Convert.ToString(trackBar1.Value);
            for (int M = K; M <= 2 * N; M++)
            {
                double rms_1 = 0, rms_2 = 0;
                for (int n = 0; n <= M; n++)
                {
                    double t = Math.Sin(2 * Math.PI * n / N + (double)phi / 180 * Math.PI);
                    rms_1 += Math.Pow(t, 2);
                    rms_2 += t;
                }

                DataSer_1.Points.AddXY(M, 0.707 - Math.Sqrt(rms_1 / (M + 1)));
                DataSer_2.Points.AddXY(M, 0.707 - (Math.Sqrt(rms_1 / (M + 1) - Math.Pow(rms_2 / (M + 1), 2))));
            }               
            chart1.ResetAutoValues();
            chart1.Series.Add(DataSer_1);
            chart1.Series.Add(DataSer_2);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BuildGraph();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            BuildGraph();
        } 

    }
}
