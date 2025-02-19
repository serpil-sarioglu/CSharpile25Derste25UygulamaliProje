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

namespace SatisHareketProjesi
{
    public partial class Satis: Form
    {
        public Satis()
        {
            InitializeComponent();
        }

        private string baglantiCumlesi = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = SatisHareketDb; Integrated Security = True; TrustServerCertificate=True";


        private void Satis_Load(object sender, EventArgs e)
        {
            // Ürünleri Yükleme
            cmbUrun.DataSource = GetUrunler();
            cmbUrun.DisplayMember = "Ad";
            cmbUrun.ValueMember = "Id";

            // Müşterileri Yükleme
            cmbMusteri.DataSource = GetMusteriler();
            cmbMusteri.DisplayMember = "AdSoyad";
            cmbMusteri.ValueMember = "Id";

            // Personelleri Yükleme
            cmbPersonel.DataSource = GetPersoneller();
            cmbPersonel.DisplayMember = "Ad";
            cmbPersonel.ValueMember = "Id";

        }
        private DataTable GetUrunler ()
        {
            DataTable dt = new DataTable();
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Id, Ad From Urunler", baglanti);
                da.Fill(dt);
            }
            return dt;
        }

        private DataTable GetMusteriler()
        {
            DataTable dt = new DataTable();
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Id, AdSoyad From Musteriler", baglanti);
                da.Fill(dt);
            }
            return dt;
        }

        private DataTable GetPersoneller()
        {
            DataTable dt = new DataTable();
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Id, Ad From Personeller", baglanti);
                da.Fill(dt);
            }
            return dt;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        { 
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();                
                using (SqlCommand cmd = new SqlCommand("Insert into Hareketler (Urun, Musteri, Personel, Fiyat) values (@p1, @p2, @p3, @p4)",baglanti))
                {
                    cmd.Parameters.AddWithValue("@p1", Convert.ToByte(cmbUrun.SelectedValue));
                    cmd.Parameters.AddWithValue("@p2", Convert.ToByte(cmbMusteri.SelectedValue));
                    cmd.Parameters.AddWithValue("@p3", Convert.ToByte(cmbPersonel.SelectedValue));
                    cmd.Parameters.AddWithValue("@p4", Convert.ToInt16(txtFiyat.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Satış işlemi başaryla kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }               
            }
        }

        private void btnSatisHareketleri_Click(object sender, EventArgs e)
        {
            SatisHareketleri form = new SatisHareketleri();
            form.Show();
        }
    }
}
