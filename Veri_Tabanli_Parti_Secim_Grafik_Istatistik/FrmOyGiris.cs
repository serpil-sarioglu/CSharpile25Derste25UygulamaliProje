using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace Veri_Tabanli_Parti_Secim_Grafik_Istatistik
{
    public partial class FrmOyGiris : Form
    {
        public FrmOyGiris()
        {
            InitializeComponent();
        }
        
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-7A4VAITE;Initial Catalog=DBSECIMPROJE;Integrated Security=True");
        
        private void btnOyGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLILCE (ILCEADI,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
            komut.Parameters.AddWithValue("@P1",txtIlceAd.Text);
            komut.Parameters.AddWithValue("@P2",txtA.Text);
            komut.Parameters.AddWithValue("@P3",txtB.Text);
            komut.Parameters.AddWithValue("@P4",txtC.Text);
            komut.Parameters.AddWithValue("@P5",txtD.Text);
            komut.Parameters.AddWithValue("@P6",txtE.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Oy girişi gerçekleşti!");
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frmGrafikler = new FrmGrafikler();
            frmGrafikler.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
