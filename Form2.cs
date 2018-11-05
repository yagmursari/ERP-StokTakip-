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
    public partial class Form2 : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-I7Q4J1V\\SQLEXPRESS01;Initial Catalog=StokTakip;Integrated Security=True");

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
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
            Form1 papatya = new Form1();
            papatya.ShowDialog();
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

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtSeriNo.Text == "" || txtAd.Text == "" || txtMarka.Text == "" || txtAdet.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurunuz!");
            }
            else
            { 
                baglanti.Open();
            string query = "update Urunler set stokAd='" + txtAd.Text + "',stokMarka='" + txtMarka.Text + "',stokAdet='" + txtAdet.Text + "',stokTarih='" + dateTimePicker1.Text + "'";
            SqlCommand cmd = new SqlCommand(query, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show(txtSeriNo.Text + " ürünü güncellenmistir");
            txtSeriNo.Text = " ";
            txtAd.Text = " ";
            txtMarka.Text = " ";
            txtAdet.Text = " ";
        }
    }
            
        }
    }

