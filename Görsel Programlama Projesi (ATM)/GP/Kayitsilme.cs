using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GP
{
    public partial class Kayitsilme : Form
    {
        public Kayitsilme()
        {
            InitializeComponent();
        }
        static string conString = "Data Source=DESKTOP-8MU62OL\\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string kayitsilme = "delete from kisiler where tcno=@tcno and sifre=@sifre";
                SqlCommand komut = new SqlCommand(kayitsilme,baglanti);
                komut.Parameters.AddWithValue("@tcno",textBox1.Text);
                komut.Parameters.AddWithValue("@sifre",textBox2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayit Silme Basarili");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayit Silme Basarisiz ! " + hata.Message);
                baglanti.Close();
            }
            ClearAll(this);
        }

        private void ClearAll(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if (c.Controls.Count > 0)
                {
                    ClearAll(c);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 anasayfa = new Form1();
            anasayfa.Show();
            Hide();
        }
    }
}
