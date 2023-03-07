using ComponentFactory.Krypton.Toolkit;
using DGVPrinterHelper;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestion_Para
{
    public partial class Reapprovisionnement : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Reapprovisionnement()
        {
            InitializeComponent();
        }
        public void FillGrid()
        {
            conn.Open();

           
          string displayQuery = "SELECT c.code,f.nom,f.prenom,a.libelle,c.quantite,c.date_c, c.statut FROM commande c, fournisseur f, article a where c.id_fo= f.id_f and c.id_arti = a.id_art  ";
           
            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }
      
        private void Commande_Load(object sender, EventArgs e)
        {
            
         
            FillGrid();    
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT id_f,nom from fournisseur", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString("id_f");
                string name2 = reader.GetString("nom");
                guna2ComboBox1.ValueMember = name;
                guna2ComboBox1.DisplayMember = name2;
                guna2ComboBox1.Items.Add(name2);

            }
            cmd.Dispose();
            reader.Close();
            MySqlCommand cmd1 = new MySqlCommand("SELECT id_art, libelle from article", conn);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                string name = reader1.GetString("id_art");
                string name2 = reader1.GetString("libelle");
                guna2ComboBox2.ValueMember = name;
                guna2ComboBox2.DisplayMember = name2;
                guna2ComboBox2.Items.Add(name2);

            }
            cmd1.Dispose();
            reader1.Close();
            conn.Close();

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("nom LIKE '%{0}%' OR prenom LIKE '%{0}%' OR libelle LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty )
            {
                MessageBox.Show(" Remplir les champs ");
            }
            else
            {
                string insertQuery = "INSERT INTO commande (id_fo,id_arti,quantite,date_c,code,statut) VALUES (@fo,@arti, @quantite,NOW(),@code,'Livré')";
               
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
              
                command.Parameters.AddWithValue("@fo", guna2ComboBox1.ValueMember);
                command.Parameters.AddWithValue("@arti", guna2ComboBox2.ValueMember);
                command.Parameters.AddWithValue("@quantite", guna2TextBox3.Text);
               
                command.Parameters.AddWithValue("@code", guna2TextBox2.Text);
                
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Article commandé avec succes ");

                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2ComboBox1.SelectedIndex = 0;
                        guna2ComboBox2.SelectedIndex = 0;

                    }
                    else
                    {
                        MessageBox.Show(" Erreur d'ajout ");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                FillGrid();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection("datasource=localhost; username=root;password=;database=para"))
            {
                if (guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty)
                {
                    MessageBox.Show(" Remplir les champs ");
                }
                else
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Create a new MySqlCommand object for the first SQL statement
                            var insertCommand = new MySqlCommand("INSERT INTO commande (id_fo,id_arti,quantite,date_c,code,statut) VALUES (@fo,@arti, @quantite,NOW(),@code,'Livré')", connection);
                            insertCommand.Parameters.AddWithValue("@fo", guna2ComboBox1.ValueMember);
                            insertCommand.Parameters.AddWithValue("@arti", guna2ComboBox2.ValueMember);
                            insertCommand.Parameters.AddWithValue("@quantite", guna2TextBox3.Text);

                            insertCommand.Parameters.AddWithValue("@code", guna2TextBox2.Text);

                            // Execute the first SQL statement
                            insertCommand.ExecuteNonQuery();

                            

                            // Create a new MySqlCommand object for the second SQL statement
                            var updateCommand = new MySqlCommand("UPDATE article JOIN commande ON article.id_art = commande.id_arti SET article.quantite = article.quantite + @quantity WHERE article.id_art = '"+ guna2ComboBox2.ValueMember + "'  ", connection);
                           
                            updateCommand.Parameters.AddWithValue("@quantity", guna2TextBox3.Text);

                            // Execute the second SQL statement
                            updateCommand.ExecuteNonQuery();

                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // An error occurred, so the transaction is rolled back
                            transaction.Rollback();
                            throw ex;
                        }

                    }
                    MessageBox.Show("succes");
                    FillGrid();
                    
                }
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "Commandes";

            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = false;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
           
            print.FooterSpacing = 15;
            print.PrintDataGridView(dataGridView1);
        }
    }
}
