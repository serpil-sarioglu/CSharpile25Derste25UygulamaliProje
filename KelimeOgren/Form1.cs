using System;
using System.Data.OleDb;
using System.Windows.Forms;


namespace KelimeOgren
{
    public partial class Form1: Form
    {
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\dbSozluk.accdb");
        Random rastgele = new Random();
        int sure = 90;
        int dogruKelimeSayisi = 0;
        string kullaniciAdi;

        public Form1(string kullanici)
        {
            InitializeComponent();
            this.kullaniciAdi = kullanici;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            lblKullanici.Text = kullaniciAdi + " hoş geldiniz. Oyun başladı!";
            Getir();
            txtTurkce.Focus();
            timer1.Start();
        }

        private void txtTurkce_TextChanged(object sender, EventArgs e)
        {
            if (txtTurkce.Text == lblCevap.Text)
            {
                dogruKelimeSayisi++;
                lblDogruKelimeSayisi.Text = dogruKelimeSayisi.ToString();             
                Getir();
                txtTurkce.Clear();
            }

        }
        void Getir() 
        {
            int sayi;
            sayi = rastgele.Next(1,2490);

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * from sozluk where id = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtIngilizce.Text = dr[1].ToString();
                lblCevap.Text = dr[2].ToString();
                lblCevap.Text = lblCevap.Text.ToLower();
            }
            baglanti.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblSure.Text = sure.ToString();
            if (sure == 0)
            {
                txtTurkce.Enabled = false;
                txtIngilizce.Enabled = false;
                timer1.Stop();
                SkoruKaydet();
                Application.Exit();
            }
            
        }

        void SkoruKaydet()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Insert into Skorlar (KullaniciAdi, DogruSayisi, Tarih) Values (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1", kullaniciAdi);
            komut.Parameters.Add("@p2",OleDbType.Integer).Value = dogruKelimeSayisi;
            komut.Parameters.Add("@p3",OleDbType.Date).Value = DateTime.Now;
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Oyun bitti! Skor: " + dogruKelimeSayisi);
        }
    }
}
