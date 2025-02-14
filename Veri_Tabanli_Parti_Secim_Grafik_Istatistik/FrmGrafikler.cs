using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Veri_Tabanli_Parti_Secim_Grafik_Istatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }


        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            // Veri tabanı bağlantısı kuruldu
            using (SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-7A4VAITE;Initial Catalog=DBSECIMPROJE;Integrated Security=True"))
            {
                baglanti.Open();

                // İlçe adlarını ComboBox'a çekme
                using (SqlCommand komut = new SqlCommand("SELECT ILCEADI FROM TBLILCE", baglanti))
                {
                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            comboBox1.Items.Add(dr["ILCEADI"]);
                        }
                    }

                }

                // Grafiğe toplam sonuçları getirme
                using (SqlCommand komut2 = new SqlCommand("SELECT SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) FROM TBLILCE", baglanti))
                using (SqlDataReader dr2 = komut2.ExecuteReader())
                {
                    while (dr2.Read())
                    {
                        chart1.Series["Partiler"].Points.AddXY("A Parti", dr2[0]);
                        chart1.Series["Partiler"].Points.AddXY("B Parti", dr2[1]);
                        chart1.Series["Partiler"].Points.AddXY("C Parti", dr2[2]);
                        chart1.Series["Partiler"].Points.AddXY("D Parti", dr2[3]);
                        chart1.Series["Partiler"].Points.AddXY("E Parti", dr2[4]);
                    }

                }

            }
        }

        // Seçilen ilçe adına göre progresbar partilere ait oy değerlerini aldı
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-7A4VAITE;Initial Catalog=DBSECIMPROJE;Integrated Security=True"))
            {
                baglanti.Open();

                using (SqlCommand komut = new SqlCommand("SELECT * FROM TBLILCE WHERE ILCEADI=@P1", baglanti))
                {
                    komut.Parameters.AddWithValue("@P1", comboBox1.Text); //Önceden Items.Add() ile ilçe adlarını ekledik

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            progressBar1.Value = int.Parse(dr[2].ToString());
                            progressBar2.Value = int.Parse(dr[3].ToString());
                            progressBar3.Value = int.Parse(dr[4].ToString());
                            progressBar4.Value = int.Parse(dr[5].ToString());
                            progressBar5.Value = int.Parse(dr[6].ToString());

                            lblA.Text = dr[2].ToString();
                            lblB.Text = dr[3].ToString();
                            lblC.Text = dr[4].ToString();
                            lblD.Text = dr[5].ToString();
                            lblE.Text = dr[6].ToString();
                        }

                    }

                }
            }                
            
        }
    }
}
