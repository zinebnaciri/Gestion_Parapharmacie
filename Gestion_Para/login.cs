using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Globalization;
using System.Web.Security;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;

namespace Gestion_Para
{
    public partial class Login : KryptonForm
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; username=root;password=;database=para");
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           

        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void kryptonPalette1_PalettePaint_1(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection("datasource= localhost; database=para; username = root; password="); //open connection
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from compte where email = '" + guna2TextBox1.Text + "' AND mot_de_passe = '" + guna2TextBox2.Text + "'", con);
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                   
                    this.Hide();
                   
                    Dashboard form1 = new Dashboard();
                   // form1.AutoScaleMode = AutoScaleMode.Dpi;
                    //form1.AutoScaleDimensions = new SizeF(96F, 96F);
                    form1.Show();
                }
                else
                {
                    MessageBox.Show("username or password incorrect. please try again !", "Oups !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            reader.Close();
            cmd.Dispose();
            con.Close(); // always close connection }
        }
    }
}
