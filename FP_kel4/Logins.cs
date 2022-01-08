using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_kel4
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void CPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void CNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Logins_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fortz\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string Username = "";
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeeTbl where EName='" + UNameTb.Text + "' and EPass='" + PasswordTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString() == "1")
            {
                Username = UNameTb.Text;
                Washs Obj = new Washs();
                Obj.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Username atau Password salah");
            }
            Con.Close();
        }
    }
}
