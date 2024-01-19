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
    public partial class LoginForm : Form
    {
        bdconnect con = new bdconnect();
        public static string sellername;
        public LoginForm()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.AliceBlue;
        }

        private void label_clear_MouseEnter(object sender, EventArgs e)
        {
            label_clear.ForeColor = Color.Red;
        }

        private void label_clear_MouseLeave(object sender, EventArgs e)
        {
            label_clear.ForeColor = Color.RoyalBlue;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_clear_Click(object sender, EventArgs e)
        {
            TextBox_user.Clear();
            TextBox_pw.Clear();
        }

        private void Button_login_Click(object sender, EventArgs e)
        {
            if (TextBox_user.Text == "" && TextBox_pw.Text == "")
            {
                MessageBox.Show("Please enter username and password", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (comboBoxtype.SelectedIndex > -1)
                {
                    if (comboBoxtype.SelectedItem.ToString() == "ADMIN")
                    {
                        if (TextBox_user.Text == "admin" || TextBox_pw.Text == "1234")
                        {
                            productForm product = new productForm();
                            product.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("if are admin, please enter the correct Id and password", "Miss Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    else
                    {
                        string selectquery = "select * from Seller where sellername='" + TextBox_user.Text + "'and sellerpw='" + TextBox_pw.Text + "'";
                        SqlDataAdapter ad = new SqlDataAdapter(selectquery, con.getcon());
                        DataTable table = new DataTable();
                        ad.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            sellername = TextBox_user.Text;
                            SellingForm selling = new SellingForm();
                            selling.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong username or password", "Wrong information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }


                }
                else
               {
                    MessageBox.Show("Please select role", "Wrong informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
