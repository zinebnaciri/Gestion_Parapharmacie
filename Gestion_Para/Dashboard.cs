using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;

namespace Gestion_Para
{
    public partial class Dashboard : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Dashboard()
        {
            InitializeComponent();
        }
        private void fillchart1()
        {
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select libelle ,quantite from article order by quantite", conn);
            MySqlDataReader dr2 = cmd1.ExecuteReader();
            while (dr2.Read())
            {

                chart1.Series["Quantité par article"].Points.AddXY(dr2["libelle"].ToString(), Convert.ToInt32(dr2["quantite"]));

            }

            conn.Close();
        }
        private void fillchart2()
        {
            
            // connexion.connexOpen();
            MySqlCommand cmd = new MySqlCommand("select * from vente", conn);
            MySqlDataReader myreader;
            try
            {
              conn.Open();
                myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    // this.commandechart.Series["Commande"].Points.AddXY(myreader.GetString("date_commande"), myreader.GetInt32("qty_commande"));
                    DateTime date = DateTime.Parse(myreader.GetString("dateV"));
                    int year = date.Year;
                    int month = date.Month;
                    int day = date.Day;

                    string year_month = day + "/" + month + "/" + year;
                    this.chart2.Series["Ventes par Date"].Points.AddXY(year_month, myreader.GetInt32("total"));
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
           
         
            fillchart1();
            fillchart2();
            load_total();
            load_totalTODAY();
          load_totalArticle();
          



        }
        private void check()
        {

            conn.Open();
            MySqlCommand comm = new MySqlCommand("SELECT libelle,quantite FROM article WHERE quantite < 5", conn);
            using (MySqlDataReader reader = comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    StringBuilder sb = new StringBuilder();
                    while (reader.Read())
                    {
                        sb.AppendFormat("{0}", reader["libelle"].ToString());
                    }
                    MessageBox.Show("Les articles " + sb.ToString() + " sont en rupture de stock, pensez à les commander. " );
                }
            }
            conn.Close();
        }
        private void checkDate()
        {

            conn.Open();
            MySqlCommand comm = new MySqlCommand("SELECT libelle,date_Ex FROM article WHERE date_Ex >= DATE_SUB(NOW(), INTERVAL 5 DAY) AND date_Ex < DATE_ADD(NOW(), INTERVAL 5 DAY)", conn);
            using (MySqlDataReader reader = comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    StringBuilder sb = new StringBuilder();
                    while (reader.Read())
                    {
                        sb.AppendFormat("{0} (Expires on {1})", reader["libelle"].ToString(), Convert.ToDateTime(reader["date_Ex"]).ToString("yyyy-MM-dd"));
                    }
                    MessageBox.Show("Les Articles avec la date d'expiration proche de la date d'aujourd'hui : " + sb.ToString());
                }
            }
            conn.Close();
        }
        public void load_total()
        {
            
            conn.Open();
            MySqlCommand comm = new MySqlCommand("SELECT sum(total) FROM vente", conn);
            
            Int32 count = Convert.ToInt32(comm.ExecuteScalar());
            if (count > 0)
            {
                label1.Text = Convert.ToString(count.ToString()) + " DHS";
            }
            else
            {
                label1.Text = "0 DHS";
            }
            conn.Close();
        }
        public void load_totalTODAY()
        {

            conn.Open();
            MySqlCommand comm = new MySqlCommand("SELECT sum(total) FROM vente where dateV=CURDATE()", conn);
            object value = comm.ExecuteScalar();
            int count = Convert.IsDBNull(value) ? 0 : Convert.ToInt32(value);
            if (count > 0)
            {
                label4.Text = Convert.ToString(count.ToString()) + " DHS";
            }
            else
            {
                label4.Text = "0 DHS";
            }
            conn.Close();
        }
        public void load_totalArticle()
        {

            conn.Open();
            MySqlCommand comm = new MySqlCommand("SELECT count(libelle) FROM article ", conn);
            Int32 count = Convert.ToInt32(comm.ExecuteScalar());
            if (count > 0)
            {
                label2.Text = Convert.ToString(count.ToString()) + " Articles";
            }
            else
            {
                label2.Text = "0 Article";
            }
            conn.Close();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
      

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Fournisseurs f = new Fournisseurs();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reapprovisionnement co = new Reapprovisionnement();
            co.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Ventes v = new Ventes();
            v.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Profil pro = new Profil();
            pro.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GestionCategories c = new GestionCategories();
            c.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GestionArticles a = new GestionArticles();
            a.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Stock S = new Stock();
            S.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Clients cl = new Clients();
            cl.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
           Login form = new Login();
            form.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Profil pro = new Profil();
            pro.Show();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Rupture r = new Rupture();
            r.Show();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            ventesList v = new ventesList();
            v.Show();
        }

        private void Dashboard_Shown(object sender, EventArgs e)
        {
            check();
            checkDate();
        }
    }
}
