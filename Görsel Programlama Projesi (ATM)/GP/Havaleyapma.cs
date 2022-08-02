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
    public partial class Havaleyapma : Form
    {
        public Havaleyapma()
        {
            InitializeComponent();
        }
        static string conString = "Data Source=DESKTOP-8MU62OL\\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);
        public string giristekitcno, giristekisifre,ikincisahistcno;
        int bakiye,ikincisahisbakiye;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                int gonderilecekbakiye = Convert.ToInt32(textBox2.Text);
                SqlCommand komut = new SqlCommand("Select * from kisiler where tcno=@havaleyapilacaktcno",baglanti);
                komut.Parameters.AddWithValue("@havaleyapilacaktcno", textBox1.Text);
                SqlDataReader havalebakiyeoku = komut.ExecuteReader();
                while (havalebakiyeoku.Read())
                {
                    ikincisahisbakiye = Convert.ToInt32(havalebakiyeoku["bakiye"].ToString());
                    ikincisahistcno = (havalebakiyeoku["tcno"].ToString());
                }
                havalebakiyeoku.Close();
                int sonuc = (int)komut.ExecuteScalar();
                if (sonuc != 0)
                {
                    if (gonderilecekbakiye <= bakiye)
                    {
                        if(ikincisahistcno != giristekitcno)
                        { 
                        bakiye -= gonderilecekbakiye;
                        SqlCommand bakiyeUpdate = new SqlCommand("Update kisiler set bakiye=@bakiye where tcno=@tcno and sifre=@sifre", baglanti);
                        bakiyeUpdate.Parameters.AddWithValue("@bakiye", bakiye);
                        bakiyeUpdate.Parameters.AddWithValue("@tcno", giristekitcno);
                        bakiyeUpdate.Parameters.AddWithValue("@sifre", giristekisifre);
                        bakiyeUpdate.ExecuteNonQuery();

                        ikincisahisbakiye += gonderilecekbakiye;
                        SqlCommand havalebakiyeUpdate = new SqlCommand("Update kisiler set bakiye=@havalebakiye where tcno=@havaleyapilacaktcno", baglanti);
                        havalebakiyeUpdate.Parameters.AddWithValue("@havaleyapilacaktcno", textBox1.Text);
                        havalebakiyeUpdate.Parameters.AddWithValue("@havalebakiye",ikincisahisbakiye);
                        havalebakiyeUpdate.ExecuteNonQuery();
                        baglanti.Close();
                        havalebakiyeoku.Close();
                        ClearAll(this);
                        MessageBox.Show("Islem Basarili");
                        }
                        else
                        {
                            MessageBox.Show("Kendinize havale gerçekleştiremezsiniz !");
                            ClearAll(this);
                            baglanti.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hesabinizda bu miktarda bakiye bulunmamaktadir!");
                        baglanti.Close();
                        ClearAll(this);
                    }
                }
                else
                {
                    MessageBox.Show("Kisi Bulunamadi");
                    ClearAll(this);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata ! Kisi Bulunamadi");
                ClearAll(this);
                baglanti.Close();
            }
    }

        private void button2_Click(object sender, EventArgs e)
        {
            Islemsecimi islemsecimi = new Islemsecimi();
            islemsecimi.giristcno = giristekitcno;
            islemsecimi.girissifre = giristekisifre;
            islemsecimi.Show();
            Hide();
        }

        private void Havaleyapma_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand bakiyesorgu = new SqlCommand("select * from kisiler where tcno=@tcno and sifre=@sifre", baglanti);
            bakiyesorgu.Parameters.AddWithValue("@tcno", giristekitcno);
            bakiyesorgu.Parameters.AddWithValue("@sifre", giristekisifre);
            SqlDataReader bakiyeoku = bakiyesorgu.ExecuteReader();
            while (bakiyeoku.Read())
            {
                bakiye = Convert.ToInt32(bakiyeoku["bakiye"].ToString());
            }
            baglanti.Close();
            bakiyeoku.Close();
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
