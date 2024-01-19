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
using DGVPrinterHelper;

namespace Marketmanagement
{
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }

        bdconnect bdcon = new bdconnect();
        DGVPrinter printer = new DGVPrinter();
        
        private void getTable()
        {
            string selectQuerry = "select prodname,prodprice from Product";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_product.DataSource = table;

        }
        private void getsellTable()
        {
            string selectQuerry = "select * from Bill";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_sellList.DataSource = table;

        }
        private void getcategory()
        {
            string selectQuerry = "select * from Category";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            comboBox_category.DataSource = table;
            comboBox_category.ValueMember = "catname";
            

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

        private void SellingForm_Load(object sender, EventArgs e)
        {
            labeld.Text = DateTime.Today.ToShortDateString();
            labeln.Text = LoginForm.sellername;
            getcategory();
            getTable();
            getsellTable();
        }

        private void dataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_name.Text = dataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_price.Text = dataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
        }

        int grandtotal = 0, n = 0;

        private void button_addlist_Click(object sender, EventArgs e)
        {
            try
            {
                string insertquery = "insert into Bill values(" + TextBox_id.Text + ",'" + labeln.Text + "','" + labeld.Text + "'," + grandtotal.ToString() + ")";
                SqlCommand cmd = new SqlCommand(insertquery, bdcon.getcon());
                bdcon.opencon();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order added succesfully", "Order informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bdcon.closecon();
                getsellTable();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonprint_Click(object sender, EventArgs e)
        {
            //DGV printer for pdf file
            printer.Title = "Markert Management Sell Lists";
            printer.SubTitle = string.Format("Date: {0}",DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = false;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "foxlearn";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dataGridView_sellList);
        }

        private void buttonrefresh_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void comboBox_category_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuerry = "select prodname,prodprice from Product where prodcat='" + comboBox_category.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(selectQuerry, bdcon.getcon());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ad.Fill(table);
            dataGridView_product.DataSource = table;
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            if (TextBox_name.Text == "" || TextBox_qty.Text == "")
            {
                MessageBox.Show("Missing information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int price, quantity;
                int.TryParse(TextBox_price.Text, out price);
                int.TryParse(TextBox_qty.Text, out quantity);

                int total = price * quantity;
                DataGridViewRow addrow = new DataGridViewRow();
                addrow.CreateCells(DataGridView_order);
                addrow.Cells[0].Value = ++n;
                addrow.Cells[1].Value = TextBox_name.Text;
                addrow.Cells[2].Value = TextBox_price.Text;
                addrow.Cells[3].Value = TextBox_qty.Text;
                addrow.Cells[4].Value = total;
                DataGridView_order.Rows.Add(addrow);
                grandtotal += total;
                label9.Text = grandtotal + "";
            }
        }
    }
}
