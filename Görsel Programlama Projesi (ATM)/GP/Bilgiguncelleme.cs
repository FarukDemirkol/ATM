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
    public partial class Bilgiguncelleme : Form
    {
        public Bilgiguncelleme()
        {
            InitializeComponent();
            comboBox1.Items.Add("Ad Soyad");
            comboBox1.Items.Add("Telefon Numarası");
            comboBox1.Items.Add("Sifre");
        }
        static string conString = "Data Source=DESKTOP-8MU62OL\\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);
        public string giristcno, girissifre;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            baglanti.Open();
            if (comboBox1.Text == "Ad Soyad")
            {
                SqlCommand bilgiguncelle = new SqlCommand("Update kisiler set adsoyad=@yeniadsoyad where tcno=@tcno and sifre = @sifre", baglanti);
                bilgiguncelle.Parameters.AddWithValue("@yeniadsoyad", textBox1.Text);
                bilgiguncelle.Parameters.AddWithValue("@tcno", giristcno);
                bilgiguncelle.Parameters.AddWithValue("@sifre", girissifre);
                bilgiguncelle.ExecuteNonQuery();
                MessageBox.Show("Bilgi Guncellendi : Ad Soyad");
                ClearAll(this);
                }
            else if (comboBox1.Text == "Telefon Numarası")
            {
                SqlCommand bilgiguncelle = new SqlCommand("Update kisiler set telefon=@yenitelefon where tcno=@tcno and sifre=@sifre", baglanti);
                bilgiguncelle.Parameters.AddWithValue("@yenitelefon", textBox1.Text);
                bilgiguncelle.Parameters.AddWithValue("@tcno", giristcno);
                bilgiguncelle.Parameters.AddWithValue("@sifre", girissifre);
                bilgiguncelle.ExecuteNonQuery();
                MessageBox.Show("Bilgi Guncellendi : Telefon Numarası");
                ClearAll(this);
            }
            else if (comboBox1.Text=="Sifre")
            {
                SqlCommand bilgiguncelle = new SqlCommand("Update kisiler set sifre = @yenisifre where tcno=@tcno and sifre=@sifre",baglanti);
                bilgiguncelle.Parameters.AddWithValue("@yenisifre",textBox1.Text);
                bilgiguncelle.Parameters.AddWithValue("@tcno",giristcno);
                bilgiguncelle.Parameters.AddWithValue("@sifre",girissifre);
                bilgiguncelle.ExecuteNonQuery();
                MessageBox.Show("Bilgi Guncellendi : Sifre");
                Form1 frm1 = new Form1();
                frm1.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Guncellenecek Bilgi Bulunamadı 1 ");
            }
            baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata ! " + hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Islemsecimi islmscm = new Islemsecimi();
            islmscm.giristcno = giristcno;
            islmscm.girissifre = girissifre;
            islmscm.Show();
            Hide();
        }

        private void Bilgiguncelleme_Load(object sender, EventArgs e)
        {
            
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

    }
}
