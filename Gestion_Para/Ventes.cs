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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Gestion_Para
{
    public partial class Ventes :KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Ventes()
        {
            InitializeComponent();
        }

        private void Ventes_Load(object sender, EventArgs e)
        {

            /*  MySqlCommand cmd2 = new MySqlCommand("SELECT prixUn from article where libelle = '"+guna2ComboBox2.Text+"'", conn);
              MySqlDataReader reader2 = cmd2.ExecuteReader();
              while (reader2.Read())
              {

                  string name2 = reader2.GetString("prixUn");

                  guna2ComboBox2.DisplayMember = name2;
                  guna2ComboBox2.Items.Add(name2);

              }
              cmd2.Dispose();
              reader2.Close();*/

            FillGrid();
        }
        public void FillGrid()
        {
            conn.Open();


            string displayQuery = "SELECT code,libelle, marque, quantite,prixUn FROM article where quantite>0 ";

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
                string insertQuery = "INSERT INTO vente (code,id_cli,id_pro,quantité_V,date_vente) VALUES (@code,@id1, @id2,@quantite,NOW())";

                conn.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, conn);

                command.Parameters.AddWithValue("@quantite", guna2TextBox3.Text);

                command.Parameters.AddWithValue("@code", guna2TextBox2.Text);

                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Article ajouté avec succes ");

                        guna2TextBox2.Clear();
                        guna2TextBox3.Clear();
                   
                     

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
               // FillGrid();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Stock s = new Stock();
            s.Show();
        }
        private decimal _runningTotal = 0;

      

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            double value;
           if (Double.TryParse(guna2TextBox3.Text, out value) && value > 0)
            {
                string code = guna2TextBox2.Text;

                int quantity = Convert.ToInt32(guna2TextBox3.Text);
                double price = Convert.ToDouble(guna2TextBox5.Text);

             
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = guna2TextBox6.Text });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = guna2TextBox3.Text });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = guna2TextBox5.Text });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = (decimal.Parse(guna2TextBox3.Text) * decimal.Parse(guna2TextBox5.Text)) });


                dataGridView2.Rows.Add(newRow);
                decimal insertedValue = (decimal)dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Column7"].Value;

              
                _runningTotal += insertedValue;

              
                label4.Text = _runningTotal.ToString();
            }
            else
            {
                MessageBox.Show("Entrez une quantité valide");
            }
           

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox2.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            guna2TextBox6.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            guna2TextBox8.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            guna2TextBox7.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            guna2TextBox5.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
           



            MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
            conn.Open();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow)
                {
        using (var transaction = conn.BeginTransaction())
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO vente (article, quantité, prixUn,total,dateV) VALUES (@value1, @value2, @value3,@value4,NOW())", conn);

                   
                    cmd.Parameters.AddWithValue("@value1", row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@value2", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@value3", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@value4", row.Cells[3].Value);
                 
                    cmd.ExecuteNonQuery();
             var updateCommand = new MySqlCommand("UPDATE article,vente SET article.quantite = article.quantite - vente.quantité Where article.libelle = vente.article and vente.dateV= NOW()", conn);

                            updateCommand.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                        FillGrid();
                    }
                    DGVPrinter print = new DGVPrinter();
                    print.Title = "Facture";

                    print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    print.PageNumbers = false;
                    print.PageNumberInHeader = false;
                    print.PorportionalColumns = true;
                    print.HeaderCellAlignment = StringAlignment.Near;
                    print.Footer = "Total " + label4.Text + "DH";
                    print.FooterSpacing = 15;
                    print.PrintDataGridView(dataGridView2);
                    guna2TextBox2.Clear();
                    guna2TextBox3.Clear();
                    guna2TextBox5.Clear();
                    guna2TextBox6.Clear();
                    guna2TextBox7.Clear();
                    guna2TextBox8.Clear();
                    guna2TextBox4.Clear();
                    dataGridView2.Rows.Clear();
                    label4.ResetText();

                }
            }
            conn.Close();

        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double total = 0;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[3].Value != null)
                {
                    try
                    {
                        total += Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                    }
                    catch (FormatException)
                    {
                        // Handle exception if the value in the cell is not a valid double
                    }
                }
            }

            label4.Text = total.ToString();


        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.DefaultView.RowFilter = string.Format("libelle LIKE '%{0}%' OR marque LIKE '%{0}%'", guna2TextBox1.Text);
            dataGridView1.DataSource = dt;
        }
    }
    }

