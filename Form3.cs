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

namespace ErpStokTakip
{
    public partial class Form3 : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-I7Q4J1V\\SQLEXPRESS01;Initial Catalog=StokTakip;Integrated Security=True");


        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Urunler ", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable dtable = new DataTable();
            adapter.Fill(dtable);
            dataGridView1.DataSource = dtable;
            baglanti.Close();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 kirmizi = new Form1();
            kirmizi.ShowDialog();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "SELECT stokAd,stokMarka,stokAdet,stokTarih FROM Urunler Where stokSeriNo='" + txtSeriNo.Text + "'";
            SqlCommand cmd = new SqlCommand(query, baglanti);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                UrunBilgileri.Ad = Convert.ToString(dr["stokAd"]);
                UrunBilgileri.Marka = Convert.ToString(dr["stokMarka"]);
                UrunBilgileri.Adet = Convert.ToString(dr["stokAdet"]);
                UrunBilgileri.Tarih = Convert.ToString(dr["stokTarih"]);


            }
            baglanti.Close();
            txtAd.Text = UrunBilgileri.Ad;
            txtMarka.Text = UrunBilgileri.Marka;
            txtAdet.Text = UrunBilgileri.Adet;
            dateTimePicker1.Text = UrunBilgileri.Tarih;

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string stokserino = txtSeriNo.Text;
            baglanti.Open();
            string query2 = "SELECT * FROM Urunler where stokSeriNo='" + stokserino + "' ";
            SqlCommand cmd2 = new SqlCommand(query2, baglanti);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            int stoksayaci = 0;

            while (dr2.Read())
            {
                stoksayaci++;
            }
            baglanti.Close();
            if (stoksayaci == 0)
            {
                MessageBox.Show("Bu seri no ya ait ürün bulunmamaktadır");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Urunler where stokSeriNo='" + txtSeriNo.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(" ürün silinmiştir");

                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Urunler ", baglanti);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                adapter.Fill(dtable);
                dataGridView1.DataSource = dtable;
                baglanti.Close();
            }

        }
    }
}
