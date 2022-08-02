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
    public partial class Adminpanel : Form
    {
        public Adminpanel()
        {
            InitializeComponent();
        }

        static string conString = "Data Source=DESKTOP-8MU62OL\\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand veriokuma = new SqlCommand("Select * from kisiler", baglanti);
            SqlDataReader verilerioku = veriokuma.ExecuteReader();
            while (verilerioku.Read())
            {
                listBox1.Items.Add("Ad Soyad    :   " + verilerioku["adsoyad"].ToString());
                listBox1.Items.Add("Tcno           :   " + verilerioku["tcno"].ToString());
                listBox1.Items.Add("Tel. No.       :   " + verilerioku["telefon"].ToString());
                listBox1.Items.Add("Bakiye         :   " + verilerioku["bakiye"].ToString() + " TL");
                listBox1.Items.Add("\n\n");
            }
            verilerioku.Close();
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
