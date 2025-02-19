using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Rehber
{
    public partial class Form1: Form
    {  
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rehber;Integrated Security=True;TrustServerCertificate=True");
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kisiler", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;        
        }
        void Temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            maskedTxtTel.Text = "";
            pictureBox1.Image = null;
            txtAd.Focus();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into Kisiler (Ad, Soyad, Telefon, Mail, ResimYolu) Values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", maskedTxtTel.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.Parameters.AddWithValue("@p5", txtResimYolu.Text); 
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi rehbere kaydedildi!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();  
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            maskedTxtTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtResimYolu.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            if (!string.IsNullOrEmpty(txtResimYolu.Text))
            {
                pictureBox1.Image = Image.FromFile(txtResimYolu.Text);
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
            DialogResult result =  MessageBox.Show("Kişi rehberden silinecek onaylıyor musunuz?","Silme Onayı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From Kisiler Where Id = @p1 " , baglanti);
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi rehberden silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Silme işlemi iptal edildi!", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Kisiler Set Ad = @p1, Soyad = @p2, Telefon = @p3, Mail = @p4, ResimYolu = @p5  Where Id = @p6",baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", maskedTxtTel.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.Parameters.AddWithValue("@p5", txtResimYolu.Text);
            komut.Parameters.AddWithValue("@p6", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi rehberde güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (dosya.ShowDialog() == DialogResult.OK)
            {
                txtResimYolu.Text = dosya.FileName;
                pictureBox1.Image = Image.FromFile(dosya.FileName);
            }
        }
    }
}
