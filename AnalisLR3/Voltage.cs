using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisLR3
{
    public partial class Voltage : Form
    {
        public Voltage()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(GlobalData.L);
            textBox2.Text = Convert.ToString(GlobalData.d);
            textBox3.Text = Convert.ToString(GlobalData.s);
            textBox4.Text = Convert.ToString(GlobalData.p);
            textBox5.Text = Convert.ToString(GlobalData.L1);
            textBox6.Text = Convert.ToString(GlobalData.L2);

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.Clear(Color.WhiteSmoke);

            Pen pen3 = new Pen(Color.Gray, 1f);
            for (int i = 0; i <= 250; i += 25)
            {
                Point[] points3 = new Point[2];
                points3[0] = new Point(0, i);
                points3[1] = new Point(500, i);
                graphics.DrawLines(pen3, points3);

            }
            for (int i = 0; i <= 500; i += 25)
            {
                Point[] points3 = new Point[2];
                points3[0] = new Point(i, 0);
                points3[1] = new Point(i, 300);
                graphics.DrawLines(pen3, points3);

            }
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.Clear(Color.WhiteSmoke);

            Pen pen3 = new Pen(Color.Gray, 1f);
            for (int i = 0; i <= 250; i += 25)
            {
                Point[] points3 = new Point[2];
                points3[0] = new Point(0, i);
                points3[1] = new Point(500, i);
                graphics.DrawLines(pen3, points3);

            }
            for (int i = 0; i <= 500; i += 25)
            {
                Point[] points3 = new Point[2];
                points3[0] = new Point(i, 0);
                points3[1] = new Point(i, 300);
                graphics.DrawLines(pen3, points3);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalData.L = Convert.ToDouble(textBox1.Text);
                GlobalData.d = Convert.ToDouble(textBox2.Text);
                GlobalData.s = Convert.ToDouble(textBox3.Text);
                GlobalData.p = Convert.ToDouble(textBox4.Text);
                GlobalData.L1 = Convert.ToDouble(textBox5.Text);
                GlobalData.L2 = Convert.ToDouble(textBox6.Text);
            }
            catch
            {
                ErrData P = new ErrData();
                P.Show();
            }



            label1.Visible = true;
            label16.Visible = true;
            label39.Visible = true;
            label40.Visible = true;
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.WhiteSmoke);
            Graphics graphics3 = pictureBox3.CreateGraphics();
            graphics3.Clear(Color.WhiteSmoke);
            Pen pen3 = new Pen(Color.Gray, 1f);
            for (int i = 0; i <= 250; i += 25)
            {
                Point[] points3 = new Point[2];
                points3[0] = new Point(0, i);
                points3[1] = new Point(500, i);
                graphics.DrawLines(pen3, points3);
                graphics3.DrawLines(pen3, points3);
            }
            for (int i = 0; i <= 500; i += 25)
            {
                Point[] points3 = new Point[2];
                points3[0] = new Point(i, 0);
                points3[1] = new Point(i, 300);
                graphics.DrawLines(pen3, points3);
                graphics3.DrawLines(pen3, points3);
            }
            Pen pen = new Pen(Color.Green, 1f);
            Pen pen2 = new Pen(Color.Blue, 1f);
            Point[] pointsUL = new Point[500];
            Point[] pointsU0 = new Point[500];
            Point[] pointsULl = new Point[500];
            Point[] pointsU0l = new Point[500];
            Point[] pointsE = new Point[500];
            Analis.Graphics(pointsUL, pointsU0, pointsE);
            graphics.DrawLines(pen, pointsUL);
            graphics.DrawLines(pen2, pointsU0);

            Analis.Voltages(pointsULl, pointsU0l);
            graphics3.DrawLines(pen, pointsULl);
            graphics3.DrawLines(pen2, pointsU0l);

            Graphics graphics2 = pictureBox2.CreateGraphics();
            Graphics graphics4 = pictureBox4.CreateGraphics();



            Point[] points = new Point[2];
            points[0] = new Point(0,6);
            points[1] = new Point(30, 6);
            graphics2.DrawLines(pen2, points);
            graphics4.DrawLines(pen2, points);
            points[0] = new Point(0, 39);
            points[1] = new Point(30, 39);
            graphics2.DrawLines(pen, points);
            graphics4.DrawLines(pen, points);

            label10.Text = Convert.ToString(50 / (GlobalData.kf1UL));
            label11.Text = Convert.ToString(100 / (GlobalData.kf1UL));
            label12.Text = Convert.ToString(150 / (GlobalData.kf1UL));
            label13.Text = Convert.ToString(200 / (GlobalData.kf1UL));
            label14.Text = Convert.ToString(250 / (GlobalData.kf1UL));
            label30.Text = Convert.ToString(GlobalData.ULmax);

            label33.Text = Convert.ToString(50 / (GlobalData.kf1ULl));
            label34.Text = Convert.ToString(100 / (GlobalData.kf1ULl));
            label35.Text = Convert.ToString(150 / (GlobalData.kf1ULl));
            label36.Text = Convert.ToString(200 / (GlobalData.kf1ULl));
            label21.Text = Convert.ToString(250 / (GlobalData.kf1ULl));
            label31.Text = Convert.ToString(GlobalData.ULlmax);
            label44.Text = Convert.ToString(GlobalData.W);

            label23.Text = Convert.ToString(GlobalData.L1);
            label24.Text = Convert.ToString(((GlobalData.L2- GlobalData.L1)/5));
            label25.Text = Convert.ToString(((GlobalData.L2 - GlobalData.L1) / 5)*2);
            label26.Text = Convert.ToString(((GlobalData.L2 - GlobalData.L1) / 5)*3);
            label27.Text = Convert.ToString(((GlobalData.L2 - GlobalData.L1) / 5)*4);
            label22.Text = Convert.ToString(GlobalData.L2);

            if (GlobalData.W > 0.0000001) label63.Text = "Да";
            else label63.Text = "Нет";
            if (GlobalData.W > 0.00001) label62.Text = "Да";
            else label62.Text = "Нет";
            if (GlobalData.W > 0.000001) label61.Text = "Да";
            else label61.Text = "Нет";
            if (GlobalData.W > 0.001) label60.Text = "Да";
            else label60.Text = "Нет";
            if (GlobalData.W > 0.000001) label59.Text = "Да";
            else label59.Text = "Нет";
            if (GlobalData.W > 0.00001) label58.Text = "Да";
            else label58.Text = "Нет";
            if (GlobalData.W > 0.000001) label57.Text = "Да";
            else label57.Text = "Нет";
            if (GlobalData.W > 0.01) label54.Text = "Да";
            else label54.Text = "Нет";
            if (GlobalData.W > 0.0001) label53.Text = "Да";
            else label53.Text = "Нет";

        }

        
    }
}
