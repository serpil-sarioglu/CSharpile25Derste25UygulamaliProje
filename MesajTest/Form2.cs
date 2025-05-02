using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MesajTest
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string numara;
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDb;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

        void GelenKutusu() 
        {
            //SqlDataAdapter da1 = new SqlDataAdapter("Select * From TblMesajlar Where Alici = " + numara,baglanti);
            SqlDataAdapter da1 = new SqlDataAdapter(@"SELECT
                                                            m.MesajId,
                                                            (k.Ad + ' ' + k.Soyad) as 'Gonderen',
                                                            (k2.Ad + ' ' + k2.Soyad) as 'Alici',
                                                            m.Baslik,
                                                            m.Icerik
                                                      FROM TblMesajlar m
                                                      INNER JOIN TblKisiler k  ON  m.Gonderen = k.Numara
                                                      INNER JOIN TblKisiler k2 ON  m.Alici    = k2.Numara
                                                      WHERE m.Alici = " + numara + " ORDER BY MesajId DESC", baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        void GidenKutusu() 
        {
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * From TblMesajlar Where Gonderen = " + numara, baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(@"SELECT
                                                            m.MesajId,
                                                            (k.Ad + ' ' + k.Soyad) as 'Gonderen',
                                                            (k2.Ad + ' ' + k2.Soyad) as 'Alici',
                                                            m.Baslik,
                                                            m.Icerik
                                                      FROM TblMesajlar m
                                                      INNER JOIN TblKisiler k  ON  m.Gonderen = k.Numara
                                                      INNER JOIN TblKisiler k2 ON  m.Alici    = k2.Numara
                                                      WHERE m.Gonderen = " + numara + " ORDER BY MesajId DESC", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            GelenKutusu();
            GidenKutusu();
            //Ad Soyadı Çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select Ad, Soyad From TblKisiler Where Numara = " + numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close();
        }

        private void btnMesajGonder_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into TblMesajlar (Gonderen,Alici,Baslik,Icerik) values(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1",numara);
            komut.Parameters.AddWithValue("@p2",maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız İletildi.");
            GidenKutusu();

        }
    }
}
