using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace Doviz_Burosu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string baglantiYolu = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DovizBurosuDb;Integrated Security=True;";

        private void Form1_Load(object sender, EventArgs e)
        {
            string bugunKurlar = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDosya = new XmlDocument();
            xmlDosya.Load(bugunKurlar);

            string dolarAlis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            lblDolarAlis.Text = dolarAlis.Replace(".", ",");

            string dolarSatis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lblDolarSatis.Text = dolarSatis.Replace(".", ",");

            string euroAlis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            lblEuroAlis.Text = euroAlis.Replace(".", ",");

            string euroSatis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            lblEuroSatis.Text = euroSatis.Replace(".", ",");

            KasayiGuncelle();

        }

        private void btnDolarAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarAlis.Text;
            btnDovizAl.Enabled = false;
            btnDovizSat.Enabled = true; 
        }

        private void btnDolarSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarSatis.Text;
            btnDovizSat.Enabled = false;
            btnDovizAl.Enabled = true;
        }

        private void btnEuroAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroAlis.Text;
            btnDovizAl.Enabled = false;
            btnDovizSat.Enabled = true;
        }

        private void btnEuroSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroSatis.Text;
            btnDovizSat.Enabled = false;
            btnDovizAl.Enabled = true;
        }

        // Müşteri döviz bozduruyor
        private void btnDovizSat_Click(object sender, EventArgs e)
        {
            double kur, miktar, tutar;
            kur = Convert.ToDouble(txtKur.Text);
            miktar = Convert.ToDouble(txtMiktar.Text);

            tutar = kur * miktar;
            txtTutar.Text = tutar.ToString();

            string dovizTuru = (txtKur.Text == lblDolarAlis.Text) ? "USD" : "EUR";
            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();

                string sorgu = @"
                    UPDATE Cash SET Miktar = Miktar + @tutar WHERE ParaBirimi = 'TL';
                    UPDATE Cash SET Miktar = Miktar - @miktar WHERE ParaBirimi = @dovizTuru;
                ";

                using (SqlCommand komut = new SqlCommand(sorgu,baglanti))
                {
                    komut.Parameters.AddWithValue("@tutar", tutar);
                    komut.Parameters.AddWithValue("@miktar", miktar);
                    komut.Parameters.AddWithValue("@dovizTuru", dovizTuru);
                    komut.ExecuteNonQuery();
                }
            }
            KasayiGuncelle();
            MessageBox.Show("Döviz bozdurma işlemi tamamlandı.");            
        }

        // Müşteri elindeki parayla döviz alıyor
        private void btnDovizAl_Click(object sender, EventArgs e)
        {
            double kur, verilenTL, alinanDoviz, kalan;
            kur = Convert.ToDouble(txtKur.Text);
            verilenTL = Convert.ToDouble(txtMiktar.Text); 

            alinanDoviz = Convert.ToDouble(verilenTL / kur); 
            txtTutar.Text = alinanDoviz.ToString();

            kalan = verilenTL % kur;
            txtKalan.Text = kalan.ToString();

            string dovizTuru = (txtKur.Text == lblDolarSatis.Text) ? "USD" : "EUR";
            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();

                // Müşteri elindeki parayla döviz alırsa döviz bürosu kasasında döviz azalır, TL artar
                string sorgu = @"
                    UPDATE Cash SET Miktar = Miktar - @alinanDoviz WHERE ParaBirimi = @dovizTuru;
                    UPDATE Cash SET Miktar = Miktar + (@verilenTL-@kalan) WHERE ParaBirimi = 'TL';
                ";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@alinanDoviz", alinanDoviz);
                    komut.Parameters.AddWithValue("@verilenTL", verilenTL);
                    komut.Parameters.AddWithValue("@kalan", kalan);
                    komut.Parameters.AddWithValue("@dovizTuru", dovizTuru);
                    komut.ExecuteNonQuery();
                }
            }
            KasayiGuncelle();
            MessageBox.Show("Döviz alım işlemi tamamlandı.");            
        }

        private void KasayiGuncelle()
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();
                string sorgu = "SELECT Id, ParaBirimi, Miktar FROM Cash";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string paraBirimi = dr["ParaBirimi"].ToString();
                            double miktar = Convert.ToDouble(dr["Miktar"]);

                            if (paraBirimi == "TL")
                                lblTL.Text = miktar.ToString("N2") + " TL";

                            if (paraBirimi == "USD")
                                lblUSD.Text = miktar.ToString("N2") + " USD";

                            if (paraBirimi == "EUR")
                                lblEUR.Text = miktar.ToString("N2") + " EUR";
                        }
                    }
                }
            }
        }
    }
}
