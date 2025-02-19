using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Text;
using System.Drawing;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private readonly string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = FilmArsivimDb; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent = ReadWrite; MultiSubnetFailover=False";


        void Filmler()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select * from Filmler", con);                    
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Filmler();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Insert into Filmler (Ad,Kategori,Link) values (@p1,@p2,@p3)", con))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtFilmAdi.Text);
                        cmd.Parameters.AddWithValue("@p2", txtKategori.Text);
                        cmd.Parameters.AddWithValue("@p3", txtLinkAdresi.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Film listenize eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Filmler(); // Listeyi güncelle
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Film eklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex; //seçilen ilk hücrenin satır indeksi
            string filmLink = dataGridView1.Rows[secilenSatir].Cells[3].Value.ToString(); // seçilen satırın 3. hücresindeki değeri al    
            chromiumWebBrowser1.Load(filmLink);
        }

        private void btnHakkimizda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu proje Serpil Sarıoğlu tarafından kodlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRenkDegistir_Click(object sender, EventArgs e)
        {
            Color[] renkler = { Color.Bisque, Color.AliceBlue, Color.Lavender, Color.LimeGreen };
            Random random = new Random();
            int index = random.Next(renkler.Length); // Rastgele bir index seç 
            this.BackColor = renkler[index]; // Formun arka plan rengini değiştir           

        }

        private void btnTamEkran_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{            
            //    string secilenFilmLink = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //    TamEkranForm tamEkran = new TamEkranForm(secilenFilmLink);
            //    tamEkran.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Lütfen bir film seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex; // seçilen ilk hücrenin satır indeksi    
            string filmLink = dataGridView1.Rows[secilenSatir].Cells[3].Value.ToString(); // seçilen satırın 3. hücresindeki değeri al    
            TamEkranForm tamEkran = new TamEkranForm(filmLink);
            tamEkran.Show();
            
        }
    }
}
    
