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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            displayCust();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fortz\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Reset()
        {
            //CNameTb.Text = "";
            //CPhoneTb.Text = "";
            
            //CCarTb.Text = "";
            //CPlatTb.Text = "";
        }

        private void displayCust()
        {
            Con.Open();
            string Query = "select CustName,CustPhone,CustPlat,CustCar from InvoiceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            //if (CNameTb.Text == "" || CPhoneTb.Text == "" || CPlatTb.Text == "" || CCarTb.Text == "")
            //{
            //    MessageBox.Show("Missing Information");
            //}
            //else
            //{
            //    try
            //    {
            //        Con.Open();
            //        SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CName,CPhone,CPlat,CCar) values (@Cn,@Cp,@Cp,@Cc)", Con);
            //        cmd.Parameters.AddWithValue("@Cn", CNameTb.Text);
            //        cmd.Parameters.AddWithValue("@Cp", CPhoneTb.Text);
            //        cmd.Parameters.AddWithValue("@Cp", CPlatTb.Text);
                    
            //        cmd.Parameters.AddWithValue("@Cc", CCarTb.Text);
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("Customer Disimpan");

            //        Con.Close();
            //        displayCust();
            //        Reset();
            //    }
            //    catch (Exception Ex)
            //    {
            //        MessageBox.Show(Ex.Message);
            //    }
            //}
        }
        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //CNameTb.Text = CustomerDGV.SelectedRows[0].Cells[0].Value.ToString();
            //CPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            //CPlatTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            //CCarTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            //if (CNameTb.Text == "")
            //{
            //    Key = 0;
            //}
            //else
            //{
            //    Key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            //}
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Pilih Customer");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CId=@CuId", Con);
                cmd.Parameters.AddWithValue("@CuId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Dihapus");
                Con.Close();
                displayCust();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            //if (CNameTb.Text == "" || CPhoneTb.Text == "" || CPlatTb.Text == "" || CCarTb.Text == "")
            //{
            //    MessageBox.Show("Missing Information");
            //}
            //else
            //{

            //    Con.Open();
            //    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CName=@Cn,CPhone=@Cp,CPlat=@Cp,CCar=@Cc where CId=@CuId; ", Con);
            //    cmd.Parameters.AddWithValue("@Cn", CNameTb.Text);
            //    cmd.Parameters.AddWithValue("@Cp", CPhoneTb.Text);
            //    cmd.Parameters.AddWithValue("@Cp", CPlatTb.Text);
            //    cmd.Parameters.AddWithValue("@Cc", CCarTb.Text);
            //    cmd.Parameters.AddWithValue("@CuId", Key);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("Customer Diupdate");

            //    Con.Close();
            //    displayCust();
            //    Reset();


            //}
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Services Obj = new Services();
            Obj.Show();
            this.Hide();
        }
    }
}
