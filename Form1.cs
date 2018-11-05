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

//DESKTOP-I7Q4J1V\\SQLEXPRESS01
namespace ErpStokTakip
{
    public partial class Form1 : Form
    {
        public SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-I7Q4J1V\\SQLEXPRESS01;Initial Catalog=StokTakip;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Urunler ", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable dtable = new DataTable();
            adapter.Fill(dtable);
            dataGridView1.DataSource = dtable;
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "Insert Into Urunler (stokSeriNo,stokAd,stokMarka,stokAdet,stokTarih) values ('" + txtSeriNo.Text.ToString() + "','" + txtAd.Text.ToString() + "','" + txtMarka.Text.ToString() + "','" + txtAdet.Text.ToString() + "','" + dateTimePicker1.Text.ToString() + "') ";
            SqlCommand cmd = new SqlCommand(query, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show(txtSeriNo.Text + " ürünü eklenmiştir");
            txtSeriNo.Text = " ";
            txtAd.Text = " ";
            txtMarka.Text = " ";
            txtAdet.Text = " ";
            dateTimePicker1.Text = "";

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Urunler ", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable dtable = new DataTable();
            adapter.Fill(dtable);
            dataGridView1.DataSource = dtable;
            baglanti.Close();


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 sincap = new Form2();
            sincap.ShowDialog();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 vosvos = new Form3();
            vosvos.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
