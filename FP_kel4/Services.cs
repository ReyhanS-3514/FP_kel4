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
    public partial class Services : Form
    {
        public Services()
        {
            InitializeComponent();
            displayServices();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Reyhan\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Reset()
        {
            SNameTb.Text = "";
            PriceTb.Text = "";

        }

        private void displayServices()
        {
            Con.Open();
            string Query = "select * from ServiceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ServiceDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ServiceTbl(SName,SPrice) values (@Sn,@SP)", Con);
                    cmd.Parameters.AddWithValue("@Sn", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SP", PriceTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Servis Disimpan");

                    Con.Close();
                    displayServices();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void ServiceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SNameTb.Text = ServiceDGV.SelectedRows[0].Cells[1].Value.ToString();
            PriceTb.Text = ServiceDGV.SelectedRows[0].Cells[2].Value.ToString();

            if (SNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ServiceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Pilih Servis");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from ServiceTbl where SId=@SeId", Con);
                cmd.Parameters.AddWithValue("@SeId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Servis Dihapus");
                Con.Close();
                displayServices();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update ServiceTbl set SName=@Sn,SPrice=@Sp where SId=@SeId", Con);
                cmd.Parameters.AddWithValue("@Sn", SNameTb.Text);
                cmd.Parameters.AddWithValue("@Sp", PriceTb.Text);
                cmd.Parameters.AddWithValue("@SeId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Service Diupdate");

                Con.Close();
                displayServices();
                Reset();
            }
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

        private void label5_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }
    }
}
