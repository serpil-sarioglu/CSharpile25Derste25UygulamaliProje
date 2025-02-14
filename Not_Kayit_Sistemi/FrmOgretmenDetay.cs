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

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-7A4VAITE;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TblDers' table. You can move, or remove it, as needed.
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("Select Count(*) From TblDers Where Durum = 0", baglanti);

            // 1. yol - ExecuteReader(), birden fazla satır ve sütun getirmek için kullanılan bir SqlCommand metodudur.
            //SqlDataReader dr1 = komut1.ExecuteReader();
            //while (dr1.Read())
            //{
            //    lblKalanSayisi.Text = dr1[0].ToString();
            //}

            // 2. yol - ExecuteScalar(), bir SQL sorgusunun tek bir değer döndürmesini sağlamak için kullanılan bir SqlCommand metodudur.
            lblKalanSayisi.Text = komut1.ExecuteScalar().ToString();

            SqlCommand komut2 = new SqlCommand("Select Count(*) From TblDers Where Durum = 1", baglanti);
            lblGecenSayisi.Text = komut2.ExecuteScalar().ToString();    
            baglanti.Close();

        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into TblDers (OgrNumara, OgrAd, OgrSoyad) values (@p1, @p2, @p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mtbNumara.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.ExecuteNonQuery(); // ExecuteNonQuery(), INSERT, UPDATE, DELETE gibi veri değiştiren sorgular için kullanılan bir SqlCommand metodudur.
            baglanti.Close();
            MessageBox.Show("Öğrenci sisteme eklendi");
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mtbNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            lblOrtalama.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;

            s1 = Convert.ToDouble(txtSinav1.Text);
            s2 = Convert.ToDouble(txtSinav2.Text);
            s3 = Convert.ToDouble(txtSinav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            lblOrtalama.Text = ortalama.ToString("0.00");

            if (ortalama >= 50)
                durum = "True";
            else
                durum = "False";


            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TblDers Set OgrS1 = @p1, OgrS2 = @p2, OgrS3 = @p3, Ortalama = @p4, Durum = @p5 Where OgrNumara = @p6", baglanti);
            komut.Parameters.AddWithValue("@p1", txtSinav1.Text);
            komut.Parameters.AddWithValue("@p2", txtSinav2.Text);
            komut.Parameters.AddWithValue("@p3", txtSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(lblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mtbNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci notları güncellendi");
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);
        }
    }
}
