using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FP_kel4
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            displayEmp();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fortz\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Reset()
        {
            ENameTb.Text = "";
            EPhoneTb.Text = "";
            EGenCb.SelectedIndex = -1;
            EAddTb.Text = "";
            PassTb.Text = "";
        }

        private void displayEmp()
        {
            Con.Open();
            string Query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EPhoneTb.Text == "" || EGenCb.SelectedIndex == -1 || EAddTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EName,EPhone,EGen,EAdd,EPass) values (@En,@Ep,@Eg,@Ea,@Passw)", Con);
                    cmd.Parameters.AddWithValue("@En", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@Ep", EPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Eg", EGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ea", EAddTb.Text);
                    cmd.Parameters.AddWithValue("@Passw", PassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Disimpan");
                    
                    Con.Close();
                    displayEmp();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ENameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            PassTb.Text = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (ENameTb.Text=="")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString()); 
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(Key == 0)
            {
                MessageBox.Show("Pilih Employee");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EId=@EmId", Con);
                cmd.Parameters.AddWithValue("@EmId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Dihapus");
                Con.Close();
                displayEmp();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EPhoneTb.Text == "" || EGenCb.SelectedIndex == -1 || EAddTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EName=@En,EPhone=@Ep,EGen= @Eg,EAdd= @Ea, EPass=@Passw where EId=@EmId", Con);
                cmd.Parameters.AddWithValue("@En", ENameTb.Text);
                cmd.Parameters.AddWithValue("@Ep", EPhoneTb.Text);
                cmd.Parameters.AddWithValue("@Eg", EGenCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Ea", EAddTb.Text);
                cmd.Parameters.AddWithValue("@Passw", PassTb.Text);
                cmd.Parameters.AddWithValue("@EmId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Diupdate");

                Con.Close();
                displayEmp();
                Reset();
            }
        }

        private void PassTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //Con.Open();
            //SqlCommand cmd = new SqlCommand("TRUNCATE TABLE EmployeeTbl", Con);
            //cmd.ExecuteNonQuery();
            //Con.Close();
            Services Obj = new Services();
            Obj.Show();
            this.Hide();
        }
    }
}
