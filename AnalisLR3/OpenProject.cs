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
    public partial class OpenProject : Form
    {
        public static DataSet DS;
        public OpenProject()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            GlobalData.name = (string)DS.Tables[0].Rows[i][1];
            GlobalData.i0 = (double)DS.Tables[0].Rows[i][2];
            GlobalData.tf = (double)DS.Tables[0].Rows[i][3];
            GlobalData.tp = (double)DS.Tables[0].Rows[i][4];
            GlobalData.z = (double)DS.Tables[0].Rows[i][5];
            GlobalData.h = (double)DS.Tables[0].Rows[i][6];
            GlobalData.r = (double)DS.Tables[0].Rows[i][7];

            GlobalData.s = (double)DS.Tables[0].Rows[i][8];
            GlobalData.d = (double)DS.Tables[0].Rows[i][9];
            GlobalData.L = (double)DS.Tables[0].Rows[i][10];
            GlobalData.p = (double)DS.Tables[0].Rows[i][11];
            GlobalData.L1 = (double)DS.Tables[0].Rows[i][12];
            GlobalData.L2 = (double)DS.Tables[0].Rows[i][13];
            MainWindow mw = new MainWindow();
            GlobalData.mw.Reload();
            this.Close();

        }

        private void OpenProject_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT * FROM [Projects]",
                    sqlConnection);
                DS = new DataSet();
                adapter.Fill(DS);
                sqlConnection.Close();
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    string name = (string)DS.Tables[0].Rows[i][1];
                    listBox1.Items.Insert(i, name);

                }
            }
            catch
            {
                ErrBD err = new ErrBD();
                err.Show();
            }
            
            
        }
    }
}
