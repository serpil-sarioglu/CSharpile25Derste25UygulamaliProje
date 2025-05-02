using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace SorguTest
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-7A4VAITE;Initial Catalog=DbNotKayit;Integrated Security=True;TrustServerCertificate=True");

        //sorgu -> select * from tblders where ogrnumara = '1993'
        //sorgu -> select * from tblders where ograd like 'S%'
        //sorgu -> select * from tblders where ogrnumara in ('1990','1991','1992')
        //sorgu -> select * from tblders
        private void btnCalistir_Click(object sender, EventArgs e)
        {
            string sorgu = richTextBox1.Text;

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch(Exception)
            {
                MessageBox.Show("Sorgu yazımı hatalı!","Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }           
        }
        //sorgu -> insert into tblders (ogrnumara,ograd,ogrsoyad) values ('1993','Serpil','Sarıoğlu')
        //sorgu -> update tblders set ograd = 'SERPİL', ogrsoyad='SARIOĞLU' where ogrnumara = '1993'
        private void btnSorguCalistir_Click(object sender, EventArgs e)
        {
            string sorgu = richTextBox1.Text;
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);                
                komut.ExecuteNonQuery();
                baglanti.Close();

                SqlDataAdapter da = new SqlDataAdapter("Select * from TblDers", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Sorgu yazımı hatalı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
