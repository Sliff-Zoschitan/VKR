﻿using MySql.Data.MySqlClient;
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
    public partial class CreateProject : Form
    {
        public CreateProject()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {          

            
            try
            {
                GlobalData.name = textBox1.Text;

            }
            catch
            {
                ErrBD EB = new ErrBD();
                EB.Show();
            }


            this.Close();
        }
    }
}
