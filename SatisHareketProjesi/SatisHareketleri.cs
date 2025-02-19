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
    public partial class SatisHareketleri: Form
    {
        public SatisHareketleri()
        {
            InitializeComponent();
        }
         private string baglantiCumlesi = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = SatisHareketDb; Integrated Security = True; TrustServerCertificate=True";
        private void SatisHareketleri_Load(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Execute spSatisHareketleri", baglanti);                    
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
    }
}
