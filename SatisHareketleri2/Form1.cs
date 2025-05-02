using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SatisHareketleri2
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = TestDb; Integrated Security = True; TrustServerCertificate=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(@"select 
                                                    	h.HareketId,
                                                    	u.UrunAd,
                                                    	(m.Ad + ' ' + m.Soyad) as 'Musteri',
                                                    	p.AdSoyad as 'Personel', 
                                                    	h.Fiyat  
                                                    from TblHareket h
                                                    inner join TblUrunler  u  on h.Urun = u.UrunId 
                                                    inner join TblMusteri  m  on h.Musteri = m.Id
                                                    inner join TblPersonel p  on h.Personel = p.Id",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
