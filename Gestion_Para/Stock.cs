using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;

namespace Gestion_Para
{
    public partial class Stock : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            FillGrid();
        }
        public void FillGrid()
        {
            conn.Open();


            string displayQuery = "SELECT c.libelle, a.libelle, a.marque, a.quantite FROM categorie c, article a where c.id_cat = a.cate_id and a.quantite > 0 ";

            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("libelle LIKE '%{0}%' OR libelle1 LIKE '%{0}%' OR marque LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }
    }
}
