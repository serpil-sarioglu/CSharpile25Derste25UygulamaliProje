using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace KelimeOgren
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\dbSozluk.accdb");
        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select Count(*) From Kullanicilar Where KullaniciAdi = @p1",baglanti);
            komut.Parameters.AddWithValue("@p1", kullaniciAdi);
            int sonuc = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();

            if (sonuc > 0)
            {
                Form1 frm = new Form1(kullaniciAdi);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı bulunamadı. Tekrar deneyin!");
            }
        }
    }
}
