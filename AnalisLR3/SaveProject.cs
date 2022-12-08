using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AnalisLR3
{
    public partial class SaveProject : Form
    {
        
        public SaveProject()
        {
            InitializeComponent();
            
        }
        private SqlConnection sqlConnection = null;
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand($"INSERT INTO [Projects] (name, i0, tf, tp, z, h, r, s, d, L, p, L1, L2) VALUES(@name, @i0, @tf, @tp, @z, @h, @r, @s, @d, @L, @p, @L1, @L2)", sqlConnection);
                command.Parameters.AddWithValue("name", GlobalData.name);
                command.Parameters.AddWithValue("i0", GlobalData.i0);
                command.Parameters.AddWithValue("tf", GlobalData.tf);
                command.Parameters.AddWithValue("tp", GlobalData.tp);
                command.Parameters.AddWithValue("z", GlobalData.z);
                command.Parameters.AddWithValue("h", GlobalData.h);
                command.Parameters.AddWithValue("r", GlobalData.r);
                command.Parameters.AddWithValue("s", GlobalData.s);
                command.Parameters.AddWithValue("d", GlobalData.d);
                command.Parameters.AddWithValue("L", GlobalData.L);
                command.Parameters.AddWithValue("p", GlobalData.p);
                command.Parameters.AddWithValue("L1", GlobalData.L1);
                command.Parameters.AddWithValue("L2", GlobalData.L2);
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch
            {
                ErrBD EB = new ErrBD();
                EB.Show();
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveProject_Load(object sender, EventArgs e)
        {

            
        }
    }
}
