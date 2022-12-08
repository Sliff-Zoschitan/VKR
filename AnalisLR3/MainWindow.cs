using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisLR3
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {            
            InitializeComponent();
            textBox1.Text = Convert.ToString(GlobalData.i0);
            textBox2.Text = Convert.ToString(GlobalData.tf);
            textBox3.Text = Convert.ToString(GlobalData.tp);
            textBox4.Text = Convert.ToString(GlobalData.z);
            textBox5.Text = Convert.ToString(GlobalData.r);
            textBox6.Text = Convert.ToString(GlobalData.h);
        }
        



        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProject CP = new CreateProject();
            CP.Show();
            
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject OP = new OpenProject();
            GlobalData.mw = this;
            OP.Show();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalData.name == "")
            {
                CreateProject CP = new CreateProject();
                CP.Show();
                
            }
            else
            {
                SaveProject SP = new SaveProject();
                SP.Show();
            }
            
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelProject DP = new DelProject();
            DP.Show();
        }

        

        private void анализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            моделированиеВлиянияToolStripMenuItem.Enabled = true;
            try
            {
                GlobalData.i0 = Convert.ToDouble(textBox1.Text);
                GlobalData.tf = Convert.ToDouble(textBox2.Text);
                GlobalData.tp = Convert.ToDouble(textBox3.Text);
                GlobalData.z = Convert.ToDouble(textBox4.Text);
                GlobalData.r = Convert.ToDouble(textBox5.Text);
                GlobalData.h = Convert.ToDouble(textBox6.Text);
                
                
            }
            catch
            {
                ErrData P = new ErrData();
                P.Show();
            }

            Graphics graphics = pictureBox1.CreateGraphics();
            
            graphics.Clear(Color.WhiteSmoke);
            
            Pen pen3 = new Pen(Color.Gray, 1f);
            for (int i = 0; i<=250;i+=25)
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
            Pen pen = new Pen(Color.Green, 1f);
            Pen pen2 = new Pen(Color.Black, 1f);
            Point[] pointsUL = new Point[500];            
            Point[] pointsU0 = new Point[500];
            Point[] pointsE = new Point[500];

            Analis.Graphics(pointsUL, pointsU0, pointsE);
            graphics.DrawLines(pen, pointsE);
            

            label10.Text = Convert.ToString(50 / (GlobalData.kf1E));
            label11.Text = Convert.ToString(100 / (GlobalData.kf1E));
            label12.Text = Convert.ToString(150 / (GlobalData.kf1E));
            label13.Text = Convert.ToString(200 / (GlobalData.kf1E));
            label14.Text = Convert.ToString(250 / (GlobalData.kf1E));
            
            label30.Text = Convert.ToString(GlobalData.Emax);    
        }

        private void моделированиеВлиянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Voltage V = new Voltage();
            V.Show();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        public void Reload()
        {
            textBox1.Text = Convert.ToString(GlobalData.i0);
            textBox2.Text = Convert.ToString(GlobalData.tf);
            textBox3.Text = Convert.ToString(GlobalData.tp);
            textBox4.Text = Convert.ToString(GlobalData.z);
            textBox5.Text = Convert.ToString(GlobalData.r);
            textBox6.Text = Convert.ToString(GlobalData.h);
            моделированиеВлиянияToolStripMenuItem.Enabled = false;
            Graphics graphics = pictureBox1.CreateGraphics();

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
            label10.Text = "0";
            label11.Text = "0";
            label12.Text = "0";
            label13.Text = "0";
            label14.Text = "0";

            label30.Text = "0";
        }
    }
}
