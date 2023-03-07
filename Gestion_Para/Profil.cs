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
    public partial class Profil : KryptonForm
    {

        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Profil()
        {
            InitializeComponent();
            
        }

        private void Profil_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.Compare(guna2TextBox5.Text, guna2TextBox4.Text) != 0)
            {
                guna2TextBox5.BorderColor = Color.Red;
                MessageBox.Show(" mot de passe est incorrecte ");

            }
            else if (guna2TextBox1.Text == String.Empty || guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty || guna2TextBox4.Text == String.Empty|| guna2TextBox5.Text == String.Empty)

            {
                MessageBox.Show(" Remplir les champs ");


                
            }
          
            else
            {
                guna2TextBox5.BorderColor = Color.Gray;

                string insertQuery = "INSERT INTO compte (nom,prenom,email,mot_de_passe)  VALUES ('" + guna2TextBox1.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox3.Text + "','" + guna2TextBox4.Text + "')";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Compte ajouté avec succes ");
                        guna2TextBox1.Clear();
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        guna2TextBox5.Clear();



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
        public void FillGrid()
        {
            conn.Open();

            string displayQuery = "SELECT * FROM compte  ";
            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment supprimer ce compte ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                try
                {
                    string deleteQuery = "DELETE FROM compte WHERE id_compte='" + guna2TextBox7.Text + "'";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("compte supprimé avec succes");
                        guna2TextBox1.Clear();
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        guna2TextBox5.Clear();
                        guna2TextBox7.Clear();

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox7.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            guna2TextBox1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            guna2TextBox2.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
         
        }
    }
}
