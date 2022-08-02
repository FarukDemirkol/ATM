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
    public partial class Girisyapma : Form
    {
        public Girisyapma()
        {
            InitializeComponent();
        }
        static string conString = "Data Source=DESKTOP-8MU62OL\\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "admin" && textBox2.Text == "admin")
                {
                    MessageBox.Show("Admin Girisi Onaylandi...");
                    Adminpanel admnpnl = new Adminpanel();
                    admnpnl.Show();
                    Hide();
                }
                else
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM kisiler WHERE tcno=@tcno and sifre=@sifre", baglanti);
                    komut.Parameters.AddWithValue("@tcno", textBox1.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox2.Text);
                    baglanti.Open();
                    int sonuc = (int)komut.ExecuteScalar();
                    ; if (sonuc != 0)
                    {
                        MessageBox.Show("Giris Basarili.");
                    }
                    else
                    {
                        MessageBox.Show("Boyle Bir kayit bulunamadi!");
                    }
                    baglanti.Close();

                    Islemsecimi islmscm = new Islemsecimi();
                    islmscm.giristcno = textBox1.Text;
                    islmscm.girissifre = textBox2.Text;
                    islmscm.Show();
                    Hide();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
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