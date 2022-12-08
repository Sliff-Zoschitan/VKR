using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisLR3
{
    public static class GlobalData
    {
        public static string name = "";
        public static double ULmax;
        public static double kf1UL = 1;
        public static double U0max;
        public static double ULlmax;
        public static double kf1ULl = 1;
        public static double U0lmax;
        public static double W;

        public static double Emax;
        public static double kf1E = 1;

        public static double i0 = 10000;
        public static double tf = 2.2716;
        public static double tp = 68.528;
        public static double z = 4500;
        public static double h = 10;
        public static double r = 10000;
        public static double f = 100000000;

        public static double s = 0.0005;
        public static double d = 0.0005;
        public static double L = 0.5;
        public static double p = 0.002;
        public static double L1 = 2;
        public static double L2 = 8;
        public static MainWindow mw;
    }
}
