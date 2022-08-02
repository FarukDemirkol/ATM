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
    public partial class Islemsecimi : Form
    {
        public Islemsecimi()
        {
            InitializeComponent();
        }
        static string conString = "Data Source=DESKTOP-8MU62OL\\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);
        public string giristcno, girissifre;

        private void button1_Click(object sender, EventArgs e)
        {
            Paracekme prckm = new Paracekme();
            prckm.girisintcno = giristcno;
            prckm.girisinsifre = girissifre;
            prckm.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Parayatirma prytrm = new Parayatirma();
            prytrm.giristcno = giristcno;
            prytrm.girissifre = girissifre;
            prytrm.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Havaleyapma hvlypm = new Havaleyapma();
            hvlypm.giristekitcno = giristcno;
            hvlypm.giristekisifre = girissifre;
            hvlypm.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 anasayfa = new Form1();
            anasayfa.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bilgiguncelleme blggncllm = new Bilgiguncelleme();
            blggncllm.girissifre = girissifre;
            blggncllm.giristcno = giristcno;
            blggncllm.Show();
            Hide();
        }

        private void Islemsecimi_Load(object sender, EventArgs e)
        {
            try
            {

            baglanti.Open();
            SqlCommand veriokuma = new SqlCommand("select * from kisiler where tcno=@tcno and sifre=@sifre", baglanti);
            veriokuma.Parameters.AddWithValue("@tcno", giristcno);
            veriokuma.Parameters.AddWithValue("@sifre", girissifre);
            SqlDataReader oku = veriokuma.ExecuteReader();

            while (oku.Read())
            {
                listBox1.Items.Add("Ad Soyad   : " + oku["adsoyad"].ToString());
                listBox1.Items.Add("Tcno          : " + oku["tcno"].ToString());
                listBox1.Items.Add("Bakiye        : " + oku["bakiye"].ToString() + " TL");
            }

            baglanti.Close();
            oku.Close();

            }
            catch(Exception hata)
            {
                MessageBox.Show("HATA ! " + hata.Message);
                baglanti.Close();
            }

            
        }
    }
}
