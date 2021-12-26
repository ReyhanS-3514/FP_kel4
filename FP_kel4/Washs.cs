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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

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

        private void GetCustData()
        {
            Con.Open();
            string query = "select * from CustomerTbl where CName='"+CustNameCb.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustPhoneTb.Text = dr["CPhone"].ToString();
            }
            Con.Close();
        }

        private void GetServiceData()
        {
            Con.Open();
            string query = "select * from ServiceTbl where SName='" + ServiceCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PriceTb.Text = dr["SPrice"].ToString();
            }
            Con.Close();
        }

        private void CustNameCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustData();
        }

        private void ServiceCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetServiceData();
        }
        int n = 0, Grdtotal = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PriceTb.Text=="")
            {
                MessageBox.Show("Pilih Servis");
            }
            else
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ServiceDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ServiceCb.SelectedValue.ToString();
                newRow.Cells[2].Value = PriceTb.Text;
                ServiceDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + Convert.ToInt32(PriceTb.Text);
                Totalbl.Text = "Rp." + Grdtotal;
            }
        }
    }
}
