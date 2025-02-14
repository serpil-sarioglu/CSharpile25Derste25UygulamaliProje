using System;
using System.Windows.Forms;

namespace Not_Kayit_Sistemi
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void btnGirişYap_Click(object sender, EventArgs e)
        {
            FrmOgrenciDetay frmOgrenciDetay = new FrmOgrenciDetay();
            frmOgrenciDetay.numara = mtbNumara.Text;
            frmOgrenciDetay.Show();
            
        }

        private void mtbNumara_TextChanged(object sender, EventArgs e)
        {
            if (mtbNumara.Text == "1111")
            {
                FrmOgretmenDetay frmOgretmenDetay = new FrmOgretmenDetay();
                frmOgretmenDetay.Show();
            }
        }
    }
}
