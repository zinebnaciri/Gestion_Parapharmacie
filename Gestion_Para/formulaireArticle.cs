using ComponentFactory.Krypton.Toolkit;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestion_Para
{
    public partial class formulaireArticle : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public formulaireArticle()
        {
            InitializeComponent();
        }

        private void formulaireArticle_Load(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT id_cat,libelle from categorie", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString("id_cat");
                string name2 = reader.GetString("libelle");
                guna2ComboBox1.ValueMember = name;
                guna2ComboBox1.DisplayMember = name2;
                guna2ComboBox1.Items.Add(name2);

            }
            cmd.Dispose();
            reader.Close();

            conn.Close();
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text == String.Empty || guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty || guna2TextBox1.Text == string.Empty || guna2TextBox4.Text == String.Empty)
            {
                MessageBox.Show("Remplir tout les champs");
            }
            else
            {
                string insertQuery = "INSERT INTO article (code,cate_id,libelle,marque,prixUn,date_Ex,quantite) VALUES (@code,@cat,@libelle,@marque,@prixUn,@date,@quantite)";
                using (MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para"))
                {
                    using (MySqlCommand command = new MySqlCommand(insertQuery, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@code", guna2TextBox1.Text);
                        command.Parameters.AddWithValue("@cat", guna2ComboBox1.ValueMember);
                        command.Parameters.AddWithValue("@libelle", guna2TextBox2.Text);
                        command.Parameters.AddWithValue("@marque", guna2TextBox3.Text);
                        command.Parameters.AddWithValue("@prixUn", guna2TextBox4.Text);
                        command.Parameters.AddWithValue("@date", guna2DateTimePicker1.Value);
                        command.Parameters.AddWithValue("@quantite", guna2TextBox5.Text);
                        try
                        {
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Article ajouté avec succes ");
                                guna2TextBox5.Clear();
                                guna2TextBox2.Clear();
                                guna2TextBox3.Clear();
                                guna2TextBox4.Clear();
                                guna2TextBox1.Clear();
                                guna2ComboBox1.SelectedIndex = 0;
                                guna2DateTimePicker1.ResetText();

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

                        GestionArticles f = new GestionArticles();
                        this.Close();
                        f.Show();
                    }
                }
            }




        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment editer cet Article ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                string insertQuery = "UPDATE article SET code =@code, cate_id=@cat, libelle=@libelle , marque= @marque, prixUn=@prix, date_Ex=@date, quantite=@quantite WHERE id_art=@id";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                command.Parameters.AddWithValue("@id", guna2TextBox6.Text);
                command.Parameters.AddWithValue("@code", guna2TextBox1.Text);
                command.Parameters.AddWithValue("@cat", guna2ComboBox1.ValueMember);
                command.Parameters.AddWithValue("@libelle", guna2TextBox2.Text);
                command.Parameters.AddWithValue("@marque", guna2TextBox3.Text);
                command.Parameters.AddWithValue("@prix", guna2TextBox4.Text);
                command.Parameters.AddWithValue("@date", guna2DateTimePicker1.Value);
                command.Parameters.AddWithValue("@quantite", guna2TextBox5.Text);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Article modifié avec succes");
                        guna2TextBox5.Clear();
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        guna2TextBox1.Clear();
                        guna2TextBox6.Clear();
                        guna2ComboBox1.SelectedIndex = 0;
                        guna2DateTimePicker1.ResetText();


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
                GestionArticles form2 = new GestionArticles();
                form2.Show();
                this.Close();



            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment supprimer cet Article ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                try
                {
                    string deleteQuery = "DELETE FROM article WHERE id_art='" + guna2TextBox6.Text + "'";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Article supprimé avec succes");
                        guna2TextBox5.Clear();
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        guna2TextBox1.Clear();
                        guna2TextBox6.Clear();
                        guna2ComboBox1.SelectedIndex = 0;
                        guna2DateTimePicker1.ResetText();
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
                GestionArticles form2 = new GestionArticles();
                this.Close();
                form2.Show();

            }
        }
    }
}

