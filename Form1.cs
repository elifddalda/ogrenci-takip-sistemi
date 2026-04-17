using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenciTkip
{
    public partial class Form1 : Form
    {

        SqlConnection baglanti = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT * FROM Ogrenciler", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                listBox1.Items.Add(oku["Ad"] + " - " + oku["Numara"]);
            }

            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtNo.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }
            baglanti.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO Ogrenciler (Ad, Numara) VALUES (@ad, @no)", baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@no", txtNo.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Veri eklendi");
            txtAd.Clear();
            txtNo.Clear();
            btnListele.PerformClick();
        }
        

        private void btnListele_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT * FROM Ogrenciler", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                listBox1.Items.Add(oku["Ad"] + " - " + oku["Numara"]);
            }

            baglanti.Close();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string secilen = listBox1.SelectedItem.ToString();

                string ad = secilen.Split('-')[0].Trim();
                string numara = secilen.Split('-')[1].Trim();

                baglanti.Open();

                SqlCommand komut = new SqlCommand("DELETE FROM Ogrenciler WHERE Ad=@ad AND Numara=@no", baglanti);
                komut.Parameters.AddWithValue("@ad", ad);
                komut.Parameters.AddWithValue("@no", numara);

                komut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Silindi");
                listBox1.Items.Clear();

                baglanti.Open();

                SqlCommand komut2 = new SqlCommand("SELECT * FROM Ogrenciler", baglanti);
                SqlDataReader oku2 = komut2.ExecuteReader();

                while (oku2.Read())
                {
                    listBox1.Items.Add(oku2["Ad"] + " - " + oku2["Numara"]);
                }

                baglanti.Close();
            }
        }

       
        
            private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedItem != null)
            {
                string secilen = listBox1.SelectedItem.ToString();

                txtAd.Text = secilen.Split('-')[0].Trim();
                txtNo.Text = secilen.Split('-')[1].Trim();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            baglanti.Open();

            SqlCommand komut = new SqlCommand("UPDATE Ogrenciler SET Numara=@no WHERE Ad=@ad", baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@no", txtNo.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Güncellendi");

            btnListele.PerformClick();
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
      
    


