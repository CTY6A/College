using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP_3
{
    class PolyharmonicSignal:Signal
    {
        double[] A, phi;
        double f;
        public PolyharmonicSignal(double[] amplitudes, double freq, double[] phases, int discrPoints)
        {
            A = amplitudes;
            n = discrPoints;
            f = freq;
            phi = phases;
            signal = GenerateSignal();
            sineSp = GetSineSpectrum();
            cosineSp = GetCosineSpectrum();
            amplSp = GetAmplSpectrum();
            phaseSp = GetPhaseSpectrum();
            restSignal = RestoreSignal();
            nfSignal = RestoreNFSignal();
        }

        internal override double[] GenerateSignal()
        {
            double[] sign = new double[n];
            Random rnd = new Random();
            for (int i = 0; i <= n - 1; i++)
            {
                double tm = 0;
                for (int j = 0; j <= 29; j++)
                {
                    tm += A[rnd.Next(7)] * Math.Cos(2 * Math.PI * f * i / n + phi[rnd.Next(6)]);
                }
                sign[i] = tm;
            }
            return sign;
        }
    }
}
