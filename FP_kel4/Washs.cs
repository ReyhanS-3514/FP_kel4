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
    public partial class Washs : Form
    {
        public Washs()
        {
            InitializeComponent();
            FillCust();
            FillServices();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Washs_Load(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Reyhan\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void FillCust()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Cname from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CName", typeof(string));
            dt.Load(rdr);
            CustNameCb.ValueMember = "CName";
            CustNameCb.DataSource = dt;
            Con.Close();
        }

        private void FillServices()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Sname from ServiceTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(string));
            dt.Load(rdr);
            ServiceCb.ValueMember = "SName";
            ServiceCb.DataSource = dt;
            Con.Close();
        }
    }
}
