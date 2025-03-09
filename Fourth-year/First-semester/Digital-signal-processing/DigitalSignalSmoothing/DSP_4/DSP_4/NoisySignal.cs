using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP_4
{
    class NoisySignal:Signal
    {
        public enum FilteringType { Sliding, Median, Parabolic }
        double A, f, phi;
        public double[] ps,ms,ss,asp,psp;
        public NoisySignal(double amplitude, double freq, double phase, int discrPoints)
        {
            A = amplitude;
            n = discrPoints;
            f = freq;
            phi = phase;
            signal = GenerateSignal();
            ps = ParabolicSmoothing();
            ms = MedianSmoothing(5);
            ss = SlidingSmoothing(9);
            sineSp = GetSineSpectrum(signal);
            cosineSp = GetCosineSpectrum(signal);
            amplSp = GetAmplSpectrum(sineSp,cosineSp);
            phaseSp = GetPhaseSpectrum(sineSp, cosineSp);
            restSignal = RestoreSignal();
            nfSignal = RestoreNFSignal();
        }

        public void Operate(FilteringType ft)
        {
            double[] fs=null;
            switch (ft)
            {
                case FilteringType.Parabolic:
                    fs = ps;
                    break;
                case FilteringType.Median:
                    fs = ms;
                    break;
                case FilteringType.Sliding:
                    fs = ss;
                    break;
                default:
                    break;
            }
            double[] sinSp = GetSineSpectrum(fs);
            double[] cosinSp = GetCosineSpectrum(fs);
            asp = GetAmplSpectrum(sinSp,cosinSp);
            psp = GetPhaseSpectrum(sinSp, cosinSp);
        }

        internal override double[] GenerateSignal()
        {
            double[] sign = new double[n];
            Random rnd = new Random();
            double B = A / 70;
            for (int i = 0; i <= n - 1; i++)
            {
                sign[i] = A * Math.Sin(2 * Math.PI * f * i / n + phi);
                double noise = 0;
                for (int j = 50; j <= 70; j++)
                {
                    noise+=(rnd.Next(100000)%2==0)?(B * Math.Sin(2 * Math.PI * f * i *j / n + phi)):(-B * Math.Sin(2 * Math.PI * f * i*j / n + phi));
                }
                sign[i] += noise;
            }
            return sign;
        }

        public double[] ParabolicSmoothing()
        {
            double[] rest = new double[n];
            for (int i = 7; i <= rest.Length - 8; i ++)
            {
                rest[i] = (-3 * signal[i - 7] - 6 * signal[i - 6] - 5 * signal[i - 5] + 3 * signal[i - 4] + 21 * signal[i - 3] +46 * signal[i - 2] + 67 * signal[i - 1] + 74 * signal[i]-3 * signal[i + 7] - 6 * signal[i + 6] - 5 * signal[i + 5] + 3 * signal[i + 4] + 21 * signal[i + 3] +46 * signal[i + 2] + 67 * signal[i + 1] ) / 320;
            }
            return rest;
        }

        public double[] SlidingSmoothing(int windowSize)
        {
            double[] rest = (double[])signal.Clone();
            List<double> window = new List<double>();
            for (int i = 0; i <= rest.Length - 1 - windowSize; i++)
            {
                window.Clear();
                for (int j = i; j <= i + windowSize - 1; j++)
                {
                    window.Add(signal[j]);
                }
                double avg = window.Sum() / windowSize;
                rest[i + windowSize / 2] = avg;
            }
            return rest;
        }

        public double[] MedianSmoothing(int windowSize)
        {
            double[] rest = (double[])signal.Clone();
            List<double> window = new List<double>();
            for (int i = 0; i <= rest.Length - 1 - windowSize; i++)
            {
                window.Clear();
                for (int j = i; j <= i + windowSize - 1; j++)
                {
                    window.Add(signal[j]);
                }
                window.Sort();
                rest[i + windowSize / 2] = window[windowSize / 2 + 1];
            }
            return rest;
        }

    }
}
