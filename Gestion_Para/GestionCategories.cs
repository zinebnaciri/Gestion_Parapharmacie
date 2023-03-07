using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;

namespace Gestion_Para
{
    public partial class GestionCategories : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public GestionCategories()
        {
            InitializeComponent();
        }

        private void GestionCategories_Load(object sender, EventArgs e)
        {
            FillGrid();
        }
       
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("libelle LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void FillGrid()
        {
            conn.Open();

            string displayQuery = "SELECT * FROM categorie  ";
            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty)
      
            {
                MessageBox.Show(" Remplir les champs ");


            }
            else
            {

                string insertQuery = "INSERT INTO categorie (code,libelle)  VALUES ('" + guna2TextBox2.Text + "','" + guna2TextBox3.Text + "')";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Categorie ajoutée avec succes ");
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                    


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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment editer cette categorie ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                string insertQuery = "UPDATE categorie SET code ='" + guna2TextBox2.Text + "', libelle='" + guna2TextBox3.Text + "'WHERE id_cat='" + guna2TextBox4.Text + "'";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("categorie modifiée avec succes");
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                     

                    }
                    else
                    {
                        MessageBox.Show(" Erreur de modification");

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
            guna2TextBox4.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            guna2TextBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            guna2TextBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment supprimer cette categorie ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                try
                {
                    string deleteQuery = "DELETE FROM categorie WHERE id_cat='" + guna2TextBox4.Text + "'";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("categorie supprimée avec succes");

                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                       
                    }
                    else
                    {
                        MessageBox.Show(" Erreur de suppression");

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
    }
}
