using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using DGVPrinterHelper;

namespace Gestion_Para
{
    public partial class GestionArticles : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public GestionArticles()
        {
            InitializeComponent();
        }
        public void FillGrid()
        {
            conn.Open();

            string displayQuery = "SELECT * FROM article  ";
            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }
        private void GestionArticles_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            FormulaireFournisseur f = new FormulaireFournisseur();
            f.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            formulaireArticle fo = new formulaireArticle();
            fo.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            formulaireArticle a = new formulaireArticle();
            this.Hide();

            a.guna2TextBox6.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            a.guna2TextBox1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            a.guna2ComboBox1.SelectedValue = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            a.guna2TextBox2.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            a.guna2TextBox3.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            a.guna2TextBox4.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            a.guna2DateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value);
            a.guna2TextBox5.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            a.edit.Visible = true;
            a.guna2Button1.Visible = false;
            a.guna2Button3.Visible = true;
            a.Show();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("libelle LIKE '%{0}%' OR marque LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "Articles";

            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = false;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;

            print.FooterSpacing = 15;
            print.PrintDataGridView(dataGridView1);
        }

        private void GestionArticles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dashboard D = new Dashboard();
            D.Refresh();
        }
    }
}
