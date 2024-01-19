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
    public partial class productForm : Form
    {
        bdconnect bdcon = new bdconnect();

        private void getTable()
        {
            string selectQuerry = "select * from Product";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_product.DataSource = table;

        }


        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_price.Clear();
            TextBox_qty.Clear();
            combocategory.SelectedIndex = 0;
        }

        private void getcategory()
        {
            string selectQuerry = "select * from Category";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            combocategory.DataSource = table;
            combocategory.ValueMember = "catname";
            comborefresh.DataSource = table;
            comborefresh.ValueMember = "catname";

        }
        public productForm()
        {
            InitializeComponent();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.CornflowerBlue;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {

            try
            {
                string insertquery = "insert into Product values(" + TextBox_id.Text + ",'" + TextBox_name.Text + "','" +TextBox_price.Text + "','"+TextBox_qty.Text+"','"+combocategory.Text+"')";
                SqlCommand cmd = new SqlCommand(insertquery, bdcon.getcon());
                bdcon.opencon();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product added succesfully", "add informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bdcon.closecon();
                getTable();
                clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void productForm_Load(object sender, EventArgs e)
        {
            getcategory();
            getTable();
            clear();
        }

        private void labelcategory_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_qty.Text == "" || TextBox_price.Text == "")
                {
                    MessageBox.Show("Missing information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string deletequerry = "delete from Product where prodId=" + TextBox_id.Text + "";
                    SqlCommand cmd = new SqlCommand(deletequerry, bdcon.getcon());
                    bdcon.opencon();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted succesfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_qty.Text == "" || TextBox_price.Text == "")
                {
                    MessageBox.Show("Missing information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertquery = "update Product set prodname='" + TextBox_name.Text + "',prodqty='" + TextBox_qty.Text + "',prodprice='" + TextBox_price.Text + "',prodcat='" + combocategory.Text + "'where prodId='" + TextBox_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(insertquery, bdcon.getcon());
                    bdcon.opencon();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated succesfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = dataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = dataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_qty.Text = dataGridView_product.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_price.Text = dataGridView_product.SelectedRows[0].Cells[3].Value.ToString();
            combocategory.SelectedValue= dataGridView_product.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void comborefresh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuerry = "select * from Product where prodcat='"+comborefresh.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_product.DataSource = table;
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

        private void label7_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
