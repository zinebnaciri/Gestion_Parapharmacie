using ComponentFactory.Krypton.Toolkit;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using  Guna.UI2.WinForms;
using System.Web.UI.WebControls;

namespace Gestion_Para
{
    public partial class FormulaireFournisseur : KryptonForm
    {
      
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public FormulaireFournisseur()
        {
            InitializeComponent();
        }

        public void Formulaire_Load(object sender, EventArgs e)
        {
            Fournisseurs f = new Fournisseurs();
            f.FillGrid();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {



            if (guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty || guna2TextBox4.Text == String.Empty || kryptonRichTextBox1.Text == string.Empty)
            // if ( guna2TextBox2.PlaceholderText == "Nom" || guna2TextBox3.PlaceholderText == "Prenom" || guna2TextBox4.PlaceholderText == "Mobile" || kryptonRichTextBox1.Text== string.Empty || guna2TextBox2.Text == " " || guna2TextBox3.Text == " " || guna2TextBox4.Text == " " )
            {
                MessageBox.Show(" Remplir les champs obligatoires  ");

                guna2TextBox2.BorderColor = Color.Red;
                guna2TextBox3.BorderColor = Color.Red;
                guna2TextBox4.BorderColor = Color.Red;
                kryptonRichTextBox1.StateCommon.Border.Color1 = Color.Red;
                kryptonRichTextBox1.StateCommon.Border.Color2 = Color.Red;


            }

            else
            {

                string insertQuery = "INSERT INTO fournisseur (nom,prenom,mobile,email,adresse,ville,pays)  VALUES ('" + guna2TextBox2.Text + "','" + guna2TextBox3.Text + "','" + guna2TextBox4.Text + "','" + guna2TextBox8.Text + "','" + kryptonRichTextBox1.Text + "','" + guna2TextBox6.Text + "','" + guna2TextBox5.Text + "')";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Fournisseur ajouté avec succes ");
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        kryptonRichTextBox1.Clear() ;
                        guna2TextBox5.Clear();
                        guna2TextBox8.Clear();
                        guna2TextBox6.Clear();
                        guna2TextBox7.Clear();



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

                Fournisseurs f = new Fournisseurs();
                this.Close();
                f.Show();
            }
    }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment supprimer ce fournisseur ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                try
                {
                    string deleteQuery = "DELETE FROM fournisseur WHERE id_f='" + guna2TextBox7.Text + "'";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("fournisseur supprimé avec succes");

                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        kryptonRichTextBox1.Clear();
                        guna2TextBox5.Clear();
                        guna2TextBox8.Clear();
                        guna2TextBox6.Clear();
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
                Fournisseurs form2 = new Fournisseurs();
                this.Close();
                form2.Show();

            }
        }


        private void edit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment editer ce fournisseur ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
              
               
                string insertQuery = "UPDATE fournisseur SET nom ='" + guna2TextBox2.Text + "', prenom='" + guna2TextBox3.Text + "',mobile='" + guna2TextBox4.Text + "' ,email= '" + guna2TextBox8.Text + "', adresse='" + kryptonRichTextBox1.Text + "', ville='" + guna2TextBox6.Text + "', pays='" + guna2TextBox5.Text + "'WHERE id_f='" + guna2TextBox7.Text + "'";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("fournisseur modifié avec succes");
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        kryptonRichTextBox1.Clear();
                        guna2TextBox5.Clear();
                        guna2TextBox8.Clear();
                        guna2TextBox6.Clear();
                        guna2TextBox7.Clear();


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
                Fournisseurs form2 = new Fournisseurs();
                form2.Show();
                this.Close();
              
             
              
            }
        }
    }
}
