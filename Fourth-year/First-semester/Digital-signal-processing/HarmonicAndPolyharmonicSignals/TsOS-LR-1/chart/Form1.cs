using System;
using System.Windows.Forms;
using System.Numerics;

namespace chart
{

    public partial class Form1 : Form
    {
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Show();
            chart2.Hide();
            chart3.Hide();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Hide();
            chart2.Show();
            chart3.Hide();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Hide();
            chart2.Hide();
            chart3.Show();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Hide();
            chart2.Hide();
            chart3.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            chart1.Series["Series4"].Points.Clear();

            chart2.Series["Series1"].Points.Clear();

            chart3.Series["Series1"].Points.Clear();

            Calculate(SignalType.Harmonic);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            chart1.Series["Series4"].Points.Clear();

            chart2.Series["Series1"].Points.Clear();

            chart3.Series["Series1"].Points.Clear();

            Calculate(SignalType.Polyharmonic);
        }

        enum SignalType { Harmonic, Polyharmonic }

        public Form1()
        {
            InitializeComponent();

            Calculate(SignalType.Harmonic);
        }

        private void Calculate(SignalType st)
        {
            Signal CurSignal;
            int N = 512;

            if (st == SignalType.Harmonic)
            {
                CurSignal = new HarmonicSignal(50, 5, -Math.PI / 3, N);
            }
            else
            {
                double[] A = new double[7] { 1, 5, 7, 8, 9, 10, 17 };
                double[] ph = new double[6] { Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2, 3 * Math.PI / 4, Math.PI };
                CurSignal = new PolyharmonicSignal(A, 3, ph, N);
            }


            for (int i = 0; i <= N - 1; i++)
            {
                chart1.Series["Series1"].Points.AddXY(i, CurSignal.signVal[i]);
            }

            if (st == SignalType.Harmonic)
                for (int i = 0; i <= N / 2 - 1; i++)
                {
                    chart1.Series["Series2"].Points.AddXY(i, CurSignal.restoredSignal[i]);
                    chart1.Series["Series3"].Points.AddXY(i, CurSignal.restorednonphasedSignal[i]);
                    chart1.Series["Series4"].Points.AddXY(i + N / 2, CurSignal.restoredWithFilterSignal[i]);
                }
            else
                for (int i = 0; i <= N / 2 - 1; i++)
                {
                    chart1.Series["Series2"].Points.AddXY(i, 1 / 2 + CurSignal.restoredSignal[i]);
                    chart1.Series["Series3"].Points.AddXY(i, 1 / 2 + CurSignal.restorednonphasedSignal[i]);
                    chart1.Series["Series4"].Points.AddXY(i + N / 2, 1 / 2 + CurSignal.restoredWithFilterSignal[i]);
                }
            for (int i = 0; i <= 99; i++)
            {
                chart2.Series["Series1"].Points.AddXY(i, CurSignal.phaseSp[i]);
                chart3.Series["Series1"].Points.AddXY(i, CurSignal.amplSp[i]);
            }
        }
    }

    class HarmonicSignal : Signal
    {
        double A, f, Ф;
        public HarmonicSignal(double amplitude, double freq, double phase, int discrPoints)
        {
            A = amplitude;
            N = discrPoints;
            f = freq;
            Ф = phase;
            signal = GenerateSignal();
            sineSp = GetSineSpectrum();
            cosineSp = GetCosineSpectrum();
            amplSp = GetAmplSpectrum();
            phaseSp = GetPhaseSpectrum();
            restSignal = RestoreSignal();
            nfSignal = RestoreNFSignal();
            wfSignal = RestoreWFSignal();
        }

        internal override double[] GenerateSignal()
        {
            double[] sign = new double[N];
            for (int i = 0; i <= N - 1; i++)
            {
                sign[i] = A * Math.Cos(2 * Math.PI * f * i / N + Ф);
            }
            return sign;
        }
    }

    class PolyharmonicSignal : Signal
    {
        double[] A, Ф;
        double f;
        public PolyharmonicSignal(double[] amplitudes, double freq, double[] phases, int discrPoints)
        {
            A = amplitudes;
            N = discrPoints;
            f = freq;
            Ф = phases;
            signal = GenerateSignal();
            sineSp = GetSineSpectrum();
            cosineSp = GetCosineSpectrum();
            amplSp = GetAmplSpectrum();
            phaseSp = GetPhaseSpectrum();
            restSignal = RestoreSignal();
            nfSignal = RestoreNFSignal();
            wfSignal = RestoreWFSignal();
        }

        internal override double[] GenerateSignal()
        {
            double[] sign = new double[N];
            Random rnd = new Random();
            for (int i = 0; i <= N - 1; i++)
            {
                double tm = 0;
                for (int j = 0; j <= 29; j++)
                {
                    tm += A[rnd.Next(7)] * Math.Cos(2 * Math.PI * f * i / N + Ф[rnd.Next(6)]);
                }
                sign[i] = tm;
            }
            return sign;
        }
    }


    abstract class Signal
    {
        internal int N;
        internal double[] signal, restSignal, nfSignal, wfSignal;
        internal double[] sineSp, cosineSp;
        internal double[] amplSp, phaseSp;
        internal int numHarm = 100;

        public double[] signVal { get { return signal; } }
        public double[] restoredSignal { get { return restSignal; } }
        public double[] restorednonphasedSignal { get { return nfSignal; } }
        public double[] restoredWithFilterSignal { get { return wfSignal; } }

        internal virtual double[] GenerateSignal()
        {
            return null;
        }

        internal double[] GetSineSpectrum()
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                double val = 0;
                for (int i = 0; i <= N - 1; i++)
                {
                    val += signal[i] * Math.Sin(2 * Math.PI * i * j / N);
                }
                values[j] = 2 * val / N;
            }
            return values;
        }

        internal double[] GetCosineSpectrum()
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                double val = 0;
                for (int i = 0; i <= N - 1; i++)
                {
                    val += signal[i] * Math.Cos(2 * Math.PI * i * j / N);
                }
                values[j] = 2 * val / N;
            }
            return values;
        }

        internal double[] GetAmplSpectrum()
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                values[j] = Math.Sqrt(Math.Pow(sineSp[j], 2) + Math.Pow(cosineSp[j], 2));
            }
            return values;
        }

        internal double[] GetPhaseSpectrum()
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                values[j] = Math.Atan(sineSp[j] / cosineSp[j]);
            }
            return values;
        }

        internal double[] RestoreSignal()
        {
            double[] values = new double[N / 2];
            for (int i = 0; i <= N / 2 - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / N - phaseSp[j]);
                }
                values[i] = val;
            }
            return values;
        }

        internal double[] RestoreNFSignal()
        {
            double[] values = new double[N / 2];
            for (int i = 0; i <= N / 2 - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / N);
                }
                values[i] = val;
            }
            return values;
        }

        internal double[] RestoreWFSignal()
        {
            double maxA = amplSp[0], minA = amplSp[0], maxPh = phaseSp[0], minPh = phaseSp[0], borderA, borderPh;
            foreach (double val in amplSp)
            {
                if (val < minA)
                {
                    minA = val;
                }

                if (val > maxA)
                {
                    maxA = val;
                }
            }

            foreach (double val in phaseSp)
            {
                if (val < minPh)
                {
                    minPh = val;
                }

                if (val > maxPh)
                {
                    maxPh = val;
                }
            }

            borderA = (maxA + minA) / 100 * 10;
            borderPh = (maxPh + minPh) / 100 * 0.0000000000000000000001;

            


            double[] values = new double[N / 2];
            for (int i = 0; i <= N / 2 - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    if ((amplSp[j] > borderA) /*&& (phaseSp[j] > borderPh)*/)
                    {
                        val += amplSp[j] * Math.Cos(2 * Math.PI * (i + N / 2) * j / N - phaseSp[j]);
                    }
                }
                values[i] = val;
            }
            return values;
        }
    }
}
