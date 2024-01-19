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

namespace Marketmanagement
{
    public partial class SellerForm : Form
    {
        bdconnect bdcon = new bdconnect();

        private void getTable()
        {
            string selectQuerry = "select * from Seller";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_seller.DataSource = table;

        }

        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_age.Clear();
            TextBox_phone.Clear();
            TextBox_pw.Clear();
        }

        public SellerForm()
        {
            InitializeComponent();
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            try
            {
                string insertquery = "insert into Seller values(" + TextBox_id.Text + ",'" + TextBox_name.Text + "','" + TextBox_age.Text + "','" + TextBox_phone.Text + "','" + TextBox_pw.Text + "')";
                SqlCommand cmd = new SqlCommand(insertquery, bdcon.getcon());
                bdcon.opencon();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller added succesfully", "add informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bdcon.closecon();
                getTable();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.CornflowerBlue;
        }

        private void labellogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void labellogout_MouseEnter(object sender, EventArgs e)
        {
            labellogout.ForeColor = Color.Red;
        }

        private void labellogout_MouseLeave(object sender, EventArgs e)
        {
            labellogout.ForeColor = Color.CornflowerBlue;
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_age.Text == "" || TextBox_phone.Text == "" || TextBox_pw.Text=="")
                {
                    MessageBox.Show("Missing information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertquery = "update Seller set sellername='" + TextBox_name.Text + "',sellerage='" + TextBox_age.Text + "',sellerphone='" + TextBox_phone.Text + "',sellerpw='" + TextBox_pw.Text + "'where sellerId='" + TextBox_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(insertquery, bdcon.getcon());
                    bdcon.opencon();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller updated succesfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bdcon.closecon();
                    getTable();
                    clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_seller_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = dataGridView_seller.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = dataGridView_seller.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_age.Text = dataGridView_seller.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_phone.Text = dataGridView_seller.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_pw.Text = dataGridView_seller.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (TextBox_id.Text == ""  )
                {
                    MessageBox.Show("Missing information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if(MessageBox.Show("Are you sure you want to delete this record", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                   { string deletequerry = "delete from Seller where sellerId=" + TextBox_id.Text + "";
                        SqlCommand cmd = new SqlCommand(deletequerry, bdcon.getcon());
                        bdcon.opencon();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Seller deleted succesfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bdcon.closecon();
                        getTable();
                        clear(); }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            getTable();
            clear();
        }

        private void labelcategory_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void label_pdt_Click(object sender, EventArgs e)
        {
            productForm product = new productForm();
            product.Show();
            this.Hide();
        }
    }
}
