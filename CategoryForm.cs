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
    public partial class CategoryForm : Form
    {
        bdconnect bdcon = new bdconnect();
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void getTable()
        {
            string selectQuerry = "select * from Category";
            SqlCommand cmd = new SqlCommand(selectQuerry,bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_category.DataSource = table;

        }

        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_des.Clear();
        }
        private void buttonadd_Click(object sender, EventArgs e)
        {

            try {

                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_des.Text == "")
                {
                    MessageBox.Show("Missing information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertquery = "insert into Category values(" + TextBox_id.Text + ",'" + TextBox_name.Text + "','" + TextBox_des.Text + "')";
                    SqlCommand cmd = new SqlCommand(insertquery, bdcon.getcon());
                    bdcon.opencon();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category added succesfully", "add informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bdcon.closecon();
                    getTable();
                    clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            getTable();
            
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string updatequery = "update Category set catname='"+TextBox_name.Text+"',catdes='"+TextBox_des.Text+"' where catId='"+TextBox_id.Text+"'";
                SqlCommand cmd = new SqlCommand(updatequery, bdcon.getcon());
                bdcon.opencon();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category updated succesfully","Update informations",MessageBoxButtons.OK,MessageBoxIcon.Information);
                bdcon.closecon();
                getTable();
                clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void dataGridView_category_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = dataGridView_category.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = dataGridView_category.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_des.Text = dataGridView_category.SelectedRows[0].Cells[2].Value.ToString();
        }

      

        private void buttondelete_Click(object sender, EventArgs e)
        {
            try
            {
                string deletequerry ="delete from Category where catId="+TextBox_id.Text+"";
                SqlCommand cmd = new SqlCommand(deletequerry, bdcon.getcon());
                bdcon.opencon();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category deleted succesfully", "delete informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bdcon.closecon();
                getTable();
                clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void labellogout_MouseEnter(object sender, EventArgs e)
        {
            labellogout.ForeColor = Color.Red;
        }

        private void labellogout_MouseLeave(object sender, EventArgs e)
        {
            labellogout.ForeColor = Color.CornflowerBlue;
        }

        private void labellogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void labelproduct_Click(object sender, EventArgs e)
        {
            productForm product = new productForm();
            product.Show();
            this.Hide();
        }

        private void labelseller_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void labelselling_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }
    }
}
