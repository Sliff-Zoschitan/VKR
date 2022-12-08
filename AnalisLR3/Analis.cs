using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace AnalisLR3
{
    static class Analis
    {
        unsafe static void Normal(double* kf1X, double Xmax)
        {
            bool norm = true;
            double k1 = *kf1X;
            
            while (norm)
            {
                if (Xmax * k1 < 100)
                {
                    k1 *= 10;
                }
                else if (Xmax * k1 >1000)
                {
                    k1 /= 10;
                }
                else if (Xmax * k1 > 750)
                {
                    k1 /= 4;
                }
                else if (Xmax * k1 > 500)
                {
                    k1 /= 3;
                }
                else if (Xmax * k1 > 250)
                {
                    k1 /= 2;
                }
                else
                {
                    
                    norm = false;
                }
            }
            *kf1X = k1;
            
        }

        
        
        public static void Voltages(Point[] pointsULl, Point[] pointsU0l)
        {
            double[] ULlmas = new double[500];
            double[] U0lmas = new double[500];
            double pi = Math.PI;            
            double c = 300000000;            
            
            double z = GlobalData.z;
            double dz = z / 2;
            double h = GlobalData.h;
            double r = GlobalData.r;
            double e0 = 8.85 * Math.Pow(8.85, -12);
            double R = Math.Sqrt(r * r + (z / 2 - h) * (z / 2 - h));

            double E = GlobalData.Emax;
            double s = GlobalData.s;
            double d = GlobalData.d;            
            double p = GlobalData.p;
            double mu0 = 4 * pi / 10000000;
            double mu = 1;
            double eps0 = 8.854 / 1000000000000;
            double eps = 2.1;

            double sgm = 5.882 * 10000000;
            double f = 100000000;
            double Zs = 100;
            double Zl = 100;

            double alf = 1 / (Math.Sqrt(s * s / 4 + p * p / (4 * pi * pi))) * pi / 180;
            alf = 33 * pi / 180;
            
            double bt0 = Math.Sqrt(1 / (pi * f * mu * sgm));
            double Rs = 1 / (sgm * bt0);
            double Zi = 2 * Rs / (pi * d);
            double omg = 2 * pi * f;
            double ttp = Math.Acos(r / R);
            double tte = 45.0 * pi / 180.0;
            double fip = 90.0 * pi/180.0;

            double ex = Math.Sin(tte) * Math.Sin(ttp);
            double ey = -Math.Sin(tte) * Math.Cos(ttp) * Math.Cos(fip) - Math.Cos(tte) * Math.Sin(fip);
            double ez = -Math.Sin(tte) * Math.Cos(ttp) * Math.Sin(fip) + Math.Cos(tte) * Math.Cos(fip);
            double k = omg / c;
            double kx = -k * Math.Cos(ttp);
            double ky = -k * Math.Sin(ttp) * Math.Cos(fip);
            double kz = -k * Math.Sin(ttp) * Math.Sin(fip);
            kx = kx * alf * p / (2 * pi);
            ky = ky * alf * p / (2 * pi);
            kz = kz * alf * p / (2 * pi);
            Complex j = new Complex(0, 1);
            double ULlmax = 0;
            double U0lmax = 0;
            double L1 = GlobalData.L1;
            double L2 = GlobalData.L2;
            for (int i = 1;i < 500;i++)
            {
                double L = L1+i*((L2-L1)/500);
                double Le = mu0 * mu / pi * L * Math.Cosh(s / d);
                double C = eps0 * eps * pi / (L * Math.Cosh(s / d));
                double G = 2 * pi * C * Math.Tan(1 * pi / 180);
                Complex Zc = Complex.Sqrt((new Complex(Zi, omg * Le)) / (new Complex(G, omg * C)));
                Complex gm = Complex.Sqrt((new Complex(Zi, omg * Le)) * (new Complex(G, omg * C)));

                Complex D = Complex.Cosh(gm * L) * (Zs + Zl) + Complex.Sinh(gm * L) * (Zc + Zs * Zl / Zc);
                Complex F1 = (alf * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L)) - (alf * Math.Cos(alf * L) + (gm + j * kz) * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm + j * kz, 2) + alf * alf);
                Complex F2 = (alf * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) - (alf * Math.Cos(alf * L) + (-gm + j * kz) * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm - j * kz, 2) + alf * alf);
                Complex K1 = ((gm + j * kz) * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L)) + (alf * Math.Sin(alf * L) - (gm + j * kz) * Math.Cos(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm + j * kz, 2) + alf * alf);
                Complex K2 = ((-gm + j * kz) * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + (alf * Math.Sin(alf * L) + (gm - j * kz) * Math.Cos(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm - j * kz, 2) + alf * alf);
                Complex Fp = F1 + F2;
                Complex Fm = F1 - F2;
                Complex Kp = K1 + K2;
                Complex Km = K1 - K2;

                Complex Fzp = F1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + F2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
                Complex Fzm = -F1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + F2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
                Complex Kzp = K1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + K2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
                Complex Kzm = -K1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + K2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
                Complex IL = -E * s / (2 * D) * ((alf * ex + j * ky * ez) * (Fp + Zl / Zc * Fm) - (alf * ey - j * kx * ez) * (Kp + Zl / Zc * Km) + 2 * (ex * Math.Cos(alf * L) + ey * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Math.Sin(kz * L)) - 2 * ex * (Complex.Cosh(gm * L) + Zl / Zc * Complex.Sinh(gm * L)));
                Complex I0 = -E * s / (2 * D) * ((alf * ex + j * ky * ez) * (Fzp + Zs / Zc * Fzm) - (alf * ey - j * kx * ez) * (Kzp + Zs / Zc * Kzm) + 2 * (Complex.Cosh(gm * L) + Zs / Zc * Complex.Sinh(gm * L)) * (ex * Math.Cos(alf * L) + ey * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Math.Sin(kz * L)) - 2 * ex);
                double ILd = Math.Sqrt(IL.Real * IL.Real + IL.Imaginary * IL.Imaginary);
                double I0d = Math.Sqrt(I0.Real * I0.Real + I0.Imaginary * I0.Imaginary);
                double ULd = ILd * Zl / (Math.Sqrt(2)); 
                double U0d = I0d * Zs / (Math.Sqrt(2));
                if (ULlmax < ULd) ULlmax = ULd;
                if (U0lmax < U0d) U0lmax = U0d;
                if (i % 50 == 0 || i == 1|| i==499) Console.WriteLine(U0d);
                

                ULlmas[i] = ULd;
                U0lmas[i] = U0d;
                
            }
            double kf1ULl = 1;
            

            unsafe
            {
                if (ULlmax > U0lmax) Normal(&kf1ULl, ULlmax);
                else Normal(&kf1ULl, U0lmax);
                
            }
            GlobalData.kf1ULl = kf1ULl;
            GlobalData.ULlmax = ULlmax;
            GlobalData.U0lmax = U0lmax;
            


            for (int i = 0; i < 500; i++)
            {
                pointsULl[i] = new Point(i, 250 - (int)(ULlmas[i] * kf1ULl));
                pointsU0l[i] = new Point(i, 250 - (int)(U0lmas[i] * kf1ULl));
                
            }

        }
        public static void Graphics(Point[] pointsUL, Point[] pointsU0, Point[] pointsE)
        {
            double[] Emas = new double[500];
            double[] ULmas = new double[500];
            double[] U0mas = new double[500];
            double ULmax = 0;
            double U0max = 0;
            double Emax = 0;
            double W = 0;

            double dt = (double)1 / 1000000000;
            double i0 = GlobalData.i0;
            double tf = GlobalData.tf;
            double tp = GlobalData.tp;
            double z = GlobalData.z;
            double dz = z / 2;
            double pi = Math.PI;
            double h = GlobalData.h;
            double r = GlobalData.r;
            double e0 = 8.85 * Math.Pow(8.85, -12);
            double R = Math.Sqrt(r * r + (z / 2 - h) * (z / 2 - h));

            double c = 300000000;
            double si = 0;
            double im0 = 0;
            //////////////////////////////////////////////
            double s = GlobalData.s;
            double d = GlobalData.d;
            double L = GlobalData.L;
            double p = GlobalData.p;
            double mu0 = 4 * pi / 10000000;
            double mu = 1;
            double eps0 = 8.854 / 1000000000000;
            double eps = 2.1;

            double sgm = 5.882 * 10000000;
            double f = 100000000;
            double Zs = 100;
            double Zl = 100;

            double alf = 1 / (Math.Sqrt(s * s / 4 + p * p / (4 * pi * pi))) * pi / 180;
            alf = 33 * pi / 180;
            double Le = mu0 * mu / pi * L * Math.Cosh(s / d);
            double C = eps0 * eps * pi / (L * Math.Cosh(s / d));
            double bt0 = Math.Sqrt(1 / (pi * f * mu * sgm));
            double Rs = 1 / (sgm * bt0);
            double Zi = 2 * Rs / (pi * d);
            double omg = 2 * pi * f;
            double ttp = Math.Acos(r / R);
            double tte = 45.0 * pi / 180.0;
            double fip = 90.0 * pi / 180.0;
            double G = 2 * pi * C * Math.Tan(0 * pi / 180);
            Complex Zc = Complex.Sqrt((new Complex(Zi, omg * Le)) / (new Complex(G, omg * C)));
            Complex gm = Complex.Sqrt((new Complex(Zi, omg * Le)) * (new Complex(G, omg * C)));
            Complex D = Complex.Cosh(gm * L) * (Zs + Zl) + Complex.Sinh(gm * L) * (Zc + Zs * Zl / Zc);
            double ex = Math.Sin(tte) * Math.Sin(ttp);
            double ey = -Math.Sin(tte) * Math.Cos(ttp) * Math.Cos(fip) - Math.Cos(tte) * Math.Sin(fip);
            double ez = -Math.Sin(tte) * Math.Cos(ttp) * Math.Sin(fip) + Math.Cos(tte) * Math.Cos(fip);
            double k = omg / c;
            double kx = -k * Math.Cos(ttp);
            double ky = -k * Math.Sin(ttp) * Math.Cos(fip);
            double kz = -k * Math.Sin(ttp) * Math.Sin(fip);
            kx = kx * alf * p / (2 * pi);
            ky = ky * alf * p / (2 * pi);
            kz = kz * alf * p / (2 * pi);
            Complex j = new Complex(0, 1);
            Complex F1 = (alf * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L)) - (alf * Math.Cos(alf * L) + (gm + j * kz) * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm + j * kz, 2) + alf * alf);
            Complex F2 = (alf * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) - (alf * Math.Cos(alf * L) + (-gm + j * kz) * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm - j * kz, 2) + alf * alf);
            Complex K1 = ((gm + j * kz) * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L)) + (alf * Math.Sin(alf * L) - (gm + j * kz) * Math.Cos(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm + j * kz, 2) + alf * alf);
            Complex K2 = ((-gm + j * kz) * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + (alf * Math.Sin(alf * L) + (gm - j * kz) * Math.Cos(alf * L)) * (Math.Cos(kz * L) - j * Complex.Sin(kz * L))) / (Complex.Pow(gm - j * kz, 2) + alf * alf);
            Complex Fp = F1 + F2;
            Complex Fm = F1 - F2;
            Complex Kp = K1 + K2;
            Complex Km = K1 - K2;

            Complex Fzp = F1*(Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + F2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
            Complex Fzm = -F1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + F2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
            Complex Kzp = K1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + K2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
            Complex Kzm = -K1 * (Complex.Cos(gm * L) - j * Complex.Sin(gm * L)) + K2 * (Complex.Cos(gm * L) + j * Complex.Sin(gm * L));
            for (int i = 0; i < 5000; i++)
            {

                double t = 0;
                t = (double)i * dt;
                double ti = t;
                
                double im = (i0 * Math.Pow(ti / (tf / 1000000), 10) * Math.Exp(-ti / (tp / 1000000))) / (0.9406 * (1 + Math.Pow(ti / (tf / 1000000), 10)));
                si += im * dt;
                double E = dz / (4 * pi * e0) * (3 * r * (dz - h) / Math.Pow(R, 5) * si + (3 * r * (dz - h)) / (c * Math.Pow(R, 4)) * im + r * (dz - h) / (c * c * R * R * R) * (im - im0) / dt);

                
                
                im0 = im;
                /////////////////////////////////////////////////////////////////
                Complex IL = -E * s / (2 * D) * ((alf * ex + j * ky * ez) * (Fp + Zl / Zc * Fm) - (alf * ey - j * kx * ez) * (Kp + Zl / Zc * Km) + 2 * (ex * Math.Cos(alf * L) + ey * Math.Sin(alf * L)) * (Math.Cos(kz * L) - j * Math.Sin(kz * L)) - 2 * ex * (Complex.Cosh(gm * L) + Zl / Zc * Complex.Sinh(gm * L)));
                Complex I0 = -E * s / (2 * D) * ((alf * ex + j * ky * ez) * (Fzp + Zs / Zc * Fzm) - (alf * ey - j * kx * ez) * (Kzp + Zs / Zc * Kzm) + 2 * ( Complex.Cosh(gm * L) + Zs/Zc*Complex.Sinh(gm*L)) * (ex*Math.Cos(alf*L)+ey*Math.Sin(alf*L))*(Math.Cos(kz * L) - j * Math.Sin(kz * L)) - 2 * ex);
                double ILd = Math.Sqrt(IL.Real * IL.Real + IL.Imaginary * IL.Imaginary);
                double I0d = Math.Sqrt(I0.Real * I0.Real + I0.Imaginary * I0.Imaginary);
                double ULd = ILd * Zl / (Math.Sqrt(2));
                double U0d = I0d * Zs / (Math.Sqrt(2));

                W += (ULd / 2) * (ULd / 2)*dt;
                if (ULmax < ULd) ULmax = ULd;
                if (U0max < U0d) U0max = U0d;
                if (Emax < E) Emax = E;
                
                if (i % 10 == 0 || i == 0) ULmas[i / 10] = ULd;
                if (i % 10 == 0 || i == 0) U0mas[i / 10] = U0d;
                if (i % 10 == 0 || i == 0) Emas[i / 10] = E;
                
            }
            double kf1UL = 1;            
            double kf1E = 1;

            unsafe
            {
                if (ULmax > U0max) Normal(&kf1UL, ULmax);
                else Normal(&kf1UL, U0max);
                Normal(&kf1E, Emax);
            }
            GlobalData.kf1UL = kf1UL;            
            GlobalData.ULmax = ULmax;                       
            GlobalData.U0max = U0max;
            GlobalData.kf1E = kf1E;
            GlobalData.Emax = Emax;
            GlobalData.W = W / Zl;


            for (int i = 0; i<500; i++)
            {
                pointsUL[i] = new Point(i, 250 - (int)(ULmas[i] * kf1UL));
                pointsU0[i] = new Point(i, 250 - (int)(U0mas[i] * kf1UL));
                pointsE[i] = new Point(i, 250 - (int)(Emas[i] * kf1E));
            }
                        
        }
    }
}
