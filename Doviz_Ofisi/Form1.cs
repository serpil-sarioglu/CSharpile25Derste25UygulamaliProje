using System;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Doviz_Ofisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string baglantiYolu = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = CurrencyExchangeOfficeDb; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent = ReadWrite; MultiSubnetFailover=False";

        private void Form1_Load(object sender, EventArgs e)
        {
            string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDosya = new XmlDocument();
            xmlDosya.Load(bugun);

            string dolarAlis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            lblDolarAlis.Text = dolarAlis.Replace(".", ",");

            string dolarSatis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lblDolarSatis.Text = dolarSatis.Replace(".", ",");

            string euroAlis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            lblEuroAlis.Text = euroAlis.Replace(".", ",");

            string euroSatis = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            lblEuroSatis.Text = euroSatis.Replace(".", ",");

        }    
        

        private void btnDolarAl_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarAlis.Text;
            btnSatisYap2.Enabled = false;
            btnSatisYap.Enabled = true;
        }

        private void btnDolarSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarSatis.Text;
            btnSatisYap.Enabled = false;
            btnSatisYap2.Enabled = true;
        }

        private void btnEuroAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroAlis.Text;
            btnSatisYap2.Enabled = false;
            btnSatisYap.Enabled = true;

        }

        private void btnEuroSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroSatis.Text;
            btnSatisYap.Enabled = false;
            btnSatisYap2.Enabled = true;
        }

        // Döviz bozdurma  ~ Müşteri kaç USD/EUR miktarı için kaç TL alacak
        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            double kur, miktar, tutar;
            
            kur = Convert.ToDouble(txtKur.Text);
            miktar = Convert.ToDouble(txtMiktar.Text); //Döviz
            tutar = kur * miktar ;                    //TL
            txtTutar.Text = tutar.ToString();


            // İşlemi Transactions tablosuna kaydet
            // Transactions tablosunda Miktar sütunu Döviz tutar, Tutar sütunu TL tutar. 
            string dovizTuru = (txtKur.Text == lblDolarAlis.Text || txtKur.Text == lblDolarSatis.Text) ? "USD" : "EUR";
            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();

                string sorgu = "INSERT INTO Transactions (DovizTuru,IslemTuru,Miktar,Kur,Tutar) VALUES (@doviz,'Satış',@miktar,@kur,@tutar)";

                using (SqlCommand komut = new SqlCommand(sorgu,baglanti))
                {
                    komut.Parameters.AddWithValue("@doviz", dovizTuru);
                    komut.Parameters.AddWithValue("@miktar", miktar);
                    komut.Parameters.AddWithValue("@kur", kur);
                    komut.Parameters.AddWithValue("@tutar", tutar);
                    komut.ExecuteNonQuery();
                }

                // Kasada güncelleme yap ~ Kasadan TL düş, döviz ekle
                // Cash tablosundaki Miktar sütunu değişecek
                string sorgu2 = @"
                UPDATE Cash SET Miktar = Miktar - @tutar WHERE ParaBirimi = 'TL';
                UPDATE Cash SET Miktar = Miktar + @miktar WHERE ParaBirimi = @doviz;              
                ";

                using (SqlCommand komut = new SqlCommand(sorgu2,baglanti))
                {
                    komut.Parameters.AddWithValue("@doviz", dovizTuru);
                    komut.Parameters.AddWithValue("@miktar", miktar);//Döviz
                    komut.Parameters.AddWithValue("@tutar", tutar); //TL
                    komut.ExecuteNonQuery();
                }
                MessageBox.Show("Müşterinin döviz satış/bozdurma işlemi[Döviz->TL] tamamlandı.");
            }
        }      

        // TL'den döviz satışı ~ Müşteri elindeki TL ile döviz almak istiyorsa
        private void btnSatisYap2_Click(object sender, EventArgs e)
        {
            double kur, verilenTLMiktar, alinanDovizMiktar, kalan;
            kur = Convert.ToDouble(txtKur.Text);
            verilenTLMiktar = Convert.ToDouble(txtMiktar.Text); // Müşterinin elindeki TL tutarı
            alinanDovizMiktar = Convert.ToDouble(verilenTLMiktar / kur); // Müşterinin alabileceği döviz miktarı
            txtTutar.Text = alinanDovizMiktar.ToString();
            kalan = verilenTLMiktar % kur;
            txtKalan.Text = kalan.ToString();

            string dovizTuru = (txtKur.Text == lblDolarAlis.Text || txtKur.Text == lblDolarSatis.Text) ? "USD" : "EUR";
            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();

                string sorgu = "INSERT INTO Transactions (DovizTuru,IslemTuru,Miktar,Kur,Tutar) VALUES (@doviz,'Alış',@alinanDovizMiktar,@kur,@verilenTLMiktar)";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@doviz", dovizTuru);
                    komut.Parameters.AddWithValue("@alinanDovizMiktar", alinanDovizMiktar);
                    komut.Parameters.AddWithValue("@kur", kur);
                    komut.Parameters.AddWithValue("@verilenTLMiktar", verilenTLMiktar);
                    komut.ExecuteNonQuery();
                }


                // Kasaya TL ekle, döviz düşür
                string sorgu2 = @"
                    UPDATE Cash SET Miktar = Miktar + (@verilenTLMiktar - @kalan) WHERE ParaBirimi = 'TL'; 
                    UPDATE Cash SET Miktar = Miktar - @alinanDovizMiktar WHERE ParaBirimi = @doviz; 
                ";

                using (SqlCommand cmd = new SqlCommand(sorgu2, baglanti))
                {
                    cmd.Parameters.AddWithValue("@doviz", dovizTuru);
                    cmd.Parameters.AddWithValue("@alinanDovizMiktar", alinanDovizMiktar);  //Alınan döviz
                    cmd.Parameters.AddWithValue("@kalan", kalan);   //Para üstü TL
                    cmd.Parameters.AddWithValue("@verilenTLMiktar", verilenTLMiktar);  //Elimizdeki TL
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Müşterinin döviz alım işlemi [TL->Döviz] tamamlandı.");
        }

        private void txtTutar_TextChanged(object sender, EventArgs e)
        {
            txtTutar.Text = txtTutar.Text.Replace(".", ",");
        }

        private void txtKalan_TextChanged(object sender, EventArgs e)
        {
            txtKalan.Text = txtKalan.Text.Replace(".", ",");
        }
    }
}
// tl eur usd
// dolar bozdurulacak kasadaki tl azalıcak usd artıcak