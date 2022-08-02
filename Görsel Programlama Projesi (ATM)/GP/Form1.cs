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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kayitekleme kytekl = new Kayitekleme();
            kytekl.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kayitsilme kytslm = new Kayitsilme();
            kytslm.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Girisyapma grsypm = new Girisyapma();
            grsypm.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hakkinda hkknd = new Hakkinda();
            hkknd.Show();
            Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                string VeritataniAdi = "ATM";
                SqlConnection baglanti = new SqlConnection("server=.\\SQLEXPRESS;database=Master; Integrated Security=SSPI");
                SqlCommand komut = new SqlCommand("SELECT Count(name) FROM master.dbo.sysdatabases WHERE name=@prmVeritabani", baglanti);
                komut.Parameters.AddWithValue("@prmVeriTabani", VeritataniAdi);
                baglanti.Open();
                int sonuc = (int)komut.ExecuteScalar();
                if (sonuc != 0)
                {
                    
                }
                else
                {
                    komut.CommandText = "Create Database " + VeritataniAdi;
                    komut.ExecuteNonQuery();
                    komut.CommandText = "use ATM";
                    komut.ExecuteNonQuery();
                    komut.CommandText = "Create table kisiler(id int NOT NULL AUTO_INCREMENT,adsoyad varchar(max),tcno varchar(max),telefon varchar(max),sifre varchar(max),bakiye int,PRIMARY KEY(id));";
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Bu isimde bir veritabanı yaratıldı.");
                }
                baglanti.Close();       
            
        }
    }
}
