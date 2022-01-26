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
            ENamelbl.Text = Logins.Username;
            Reset();
            ServiceCb.SelectedText = "Service";
            TodayDate.Value=DateTime.Now;
            Grdtotal = 0;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Washs_Load(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fortz\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void FillCust()
        {
            //Con.Open();
            //SqlCommand cmd = new SqlCommand("select Cname from CustomerTbl", Con);
            //SqlDataReader rdr;
            //rdr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("CName", typeof(string));
            //dt.Load(rdr);
            //CustNameCb.ValueMember = "CName";
            //CustNameCb.DataSource = dt;
            //Con.Close();
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

        private void ResetTable()
        {
            Con.Open();
            string query = "Truncate";
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
        private void GetCustData()
        {
            //Con.Open();
            //string query = "select * from CustomerTbl where CName='"+CustNameCb.SelectedValue.ToString()+"'";
            //SqlCommand cmd = new SqlCommand(query, Con);
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);
            //foreach(DataRow dr in dt.Rows)
            //{
            //    CustPhoneTb.Text = dr["CPhone"].ToString();
            //}
            //Con.Close();
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

        private void Reset()
        {
            
            CustNameTb.Text = "";
            CustPhoneTb.Text = "";
            CustPlatTb.Text = "";
            CustCarTb.Text = "";
            ServiceCb.SelectedIndex = -1;
            ServiceCb.SelectedText = "Service";
            PriceTb.Text = "";
            n = 0;
            pos = 120 + 65;
            Grdtotal = 0;
        }
        private void BillBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustPlatTb.Text == "" || CustCarTb.Text == "" || Grdtotal == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into InvoiceTbl(CustName,CustPhone,EName,Amt,IDate,CustPlat,CustCar) values (@Cn,@Cp,@En,@Am,@Id,@Cpl,@Cc)", Con);
                    
                    cmd.Parameters.AddWithValue("@Cn", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@Cp", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@En", ENamelbl.Text);
                    cmd.Parameters.AddWithValue("@Am", Grdtotal);
                    cmd.Parameters.AddWithValue("@Id", TodayDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Cpl", CustPlatTb.Text);
                    cmd.Parameters.AddWithValue("@Cc", CustCarTb.Text);
                    cmd.ExecuteNonQuery();

                   
                    MessageBox.Show("Invoice Disimpan");

                    Con.Close();
                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                    Reset();
                    ServiceDGV.Rows.Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ENamelbl_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }
        int sId, sPrice, pos = 120 + 65;
        string sName, cName, cTelp, cCar, cPlat, eName,iDate;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            cName = CustNameTb.Text;
            eName = ENamelbl.Text;
            cPlat = CustPlatTb.Text;
            cCar = CustCarTb.Text;
            cTelp = CustPhoneTb.Text;
            //iDate = Convert.ToString(TodayDate.Value.Date);
            iDate = Convert.ToString(DateTime.Now);

            e.Graphics.DrawString("Cuci Mobil Kelompok 4", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(50));
            e.Graphics.DrawString("Employee : " + eName, new Font("Century Gothic", 6, FontStyle.Bold), Brushes.Black, new Point(26, 20+30));
            e.Graphics.DrawString("" + iDate, new Font("Century Gothic", 6, FontStyle.Bold), Brushes.Black, new Point(130+42, 20 + 30));
            e.Graphics.DrawString("Customer : " + cName, new Font("Century Gothic", 6, FontStyle.Bold), Brushes.Black, new Point(26 , 50 + 30));
            e.Graphics.DrawString("Telp     : " + cTelp, new Font("Century Gothic", 6, FontStyle.Bold), Brushes.Black, new Point(26 , 70 + 30));
            e.Graphics.DrawString("Mobil    : " + cCar, new Font("Century Gothic", 6, FontStyle.Bold), Brushes.Black, new Point(130 + 42, 50 + 30));
            e.Graphics.DrawString("NoPol    : " + cPlat, new Font("Century Gothic", 6, FontStyle.Bold), Brushes.Black, new Point(130 + 42, 70 + 30));
            e.Graphics.DrawString("************* Invoice *************", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, 107 +20));
            e.Graphics.DrawString("ID SERVIS                 HARGA", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26 + 26,107 + 55));
            foreach (DataGridViewRow row in ServiceDGV.Rows)
            {
                sId = Convert.ToInt32(row.Cells["Column1"].Value);
                sName = "" + row.Cells["Column2"].Value;
                sPrice = Convert.ToInt32(row.Cells["Column3"].Value);

                e.Graphics.DrawString("" + sId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26 + 26, pos));
                e.Graphics.DrawString("" + sName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45 + 26, pos));
                e.Graphics.DrawString("" + sPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120 + 76, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Total : Rp." + Grdtotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("*********** Cuci Mobil ***********" , new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));

        }
        
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
