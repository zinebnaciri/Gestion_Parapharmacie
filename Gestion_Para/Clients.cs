using ComponentFactory.Krypton.Toolkit;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
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

namespace Gestion_Para
{
    public partial class Clients : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Clients()
        {
            InitializeComponent();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("nom LIKE '%{0}%' OR prenom LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }
        public void FillGrid()
        {
            conn.Open();

            string displayQuery = "SELECT * FROM client  ";
            MySqlDataAdapter da = new MySqlDataAdapter(displayQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty || guna2TextBox4.Text == String.Empty|| guna2TextBox5.Text == String.Empty)

            {
                MessageBox.Show(" Remplir les champs ");


            }
            else
            {

                string insertQuery = "INSERT INTO client (nom,prenom,mobile,cin)  VALUES ('" + guna2TextBox2.Text + "','" + guna2TextBox4.Text + "','" + guna2TextBox5.Text + "','" + guna2TextBox3.Text + "')";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("client ajouté avec succes ");
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment editer ce client ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                string insertQuery = "UPDATE client SET nom ='" + guna2TextBox2.Text + "', prenom='" + guna2TextBox4.Text + "', mobile='" + guna2TextBox5.Text + "', cin='" + guna2TextBox3.Text + "' WHERE id_client='" + guna2TextBox6.Text + "'";
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("client modifié avec succes");
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        guna2TextBox5.Clear();
                        guna2TextBox6.Clear();

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
            guna2TextBox6.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            guna2TextBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            guna2TextBox4.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            guna2TextBox5.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            guna2TextBox3.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous vraiment supprimer ce client ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {


                try
                {
                    string deleteQuery = "DELETE FROM client WHERE id_client='" + guna2TextBox6.Text + "'";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("client supprimé avec succes");
                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox4.Clear();
                        guna2TextBox5.Clear();
                        guna2TextBox6.Clear();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF(*.pdf)|*.pdf";
                save.FileName = "ClientsRapport.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Impossible de sauvegarder" + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                PdfPCell pCell = new PdfPCell();
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {

                                    pTable.AddCell(dcell.Value.ToString());
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Export effectué avec succes", "info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de l'export" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun enregistements, veuillez remplire la table des fournisseurs", "info");

            }
        }
    }
}
