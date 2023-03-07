using ComponentFactory.Krypton.Toolkit;
using DGVPrinterHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Para
{
    public partial class ventesList : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public ventesList()
        {
            InitializeComponent();
        }

        private void ventesList_Load(object sender, EventArgs e)
        {
            FillGrid();
        }
        public void FillGrid()
        {
            conn.Open();


            string displayQuery = "SELECT * FROM vente order by dateV ";

            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("article LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "Ventes";

            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = false;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;

            print.FooterSpacing = 15;
            print.PrintDataGridView(dataGridView1);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("dateV = '{0:yyyy-MM-dd}'", dateTimePicker1.Value.Date);
            dataGridView1.DataSource = dt;

        }
    }
}
