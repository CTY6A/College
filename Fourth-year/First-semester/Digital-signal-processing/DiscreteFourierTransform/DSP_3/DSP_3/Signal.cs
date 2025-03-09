using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP_3
{
    abstract class Signal
    {
        internal int n;
        internal double[] signal, restSignal, nfSignal;
        internal double[] sineSp, cosineSp;
        internal double[] amplSp, phaseSp;
        internal int numHarm = 100;
        public Signal()
        {
            signal = GenerateSignal();
            sineSp = GetSineSpectrum();
            cosineSp = GetCosineSpectrum();
            amplSp = GetAmplSpectrum();
            phaseSp = GetPhaseSpectrum();
            restSignal = RestoreSignal();
            nfSignal = RestoreNFSignal();
        }
        public double[] signVal { get { return signal; } }
        public double[] amplSpectrum { get { return amplSp; } }
        public double[] phaseSpectrum { get { return phaseSp; } }
        public double[] restoredSignal { get { return restSignal; } }
        public double[] restorednonphasedSignal { get { return nfSignal; } }

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
                for (int i = 0; i <= n - 1; i++)
                {
                    val += signal[i] * Math.Sin(2 * Math.PI * i * j / n);
                }
                values[j] = 2 * val / n;
            }
            return values;
        }

        internal double[] GetCosineSpectrum()
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                double val = 0;
                for (int i = 0; i <= n - 1; i++)
                {
                    val += signal[i] * Math.Cos(2 * Math.PI * i * j / n);
                }
                values[j] = 2 * val / n;
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
            double[] values = new double[n];
            for (int i = 0; i <= n - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / n - phaseSp[j]);
                }
                values[i] = val;
            }
            return values;
        }

        internal double[] RestoreNFSignal()
        {
            double[] values = new double[n];
            for (int i = 0; i <= n - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / n);
                }
                values[i] = val;
            }
            return values;
        }
    }
}
