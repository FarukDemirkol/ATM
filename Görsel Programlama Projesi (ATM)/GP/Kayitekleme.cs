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
    public partial class Kayitekleme : Form
    {
        public Kayitekleme()
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
                string kayit = "INSERT INTO kisiler(adsoyad,tcno,telefon,sifre,bakiye) values(@adsoyad,@tcno,@telefon,@sifre,@bakiye)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@adsoyad", textBox1.Text);
                komut.Parameters.AddWithValue("@tcno", textBox2.Text);
                komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                komut.Parameters.AddWithValue("@sifre", textBox4.Text);
                int bakiyeal = Convert.ToInt32(textBox5.Text);
                komut.Parameters.AddWithValue("@bakiye", bakiyeal);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayit Basarili ! ");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayit Ekleme Basarisiz "+hata.Message);
                baglanti.Close();
            }

            ClearAll(this);
        }


        private void ClearAll(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                if(c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if(c.Controls.Count > 0)
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
