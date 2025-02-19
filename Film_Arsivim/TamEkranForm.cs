using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Film_Arsivim
{
    // Form1'den gelen film linki ChromiumWebBrowser'da tam ekran görüntüleniyor
    // TamEkranForm için WindowsState = Maximized, chromiumWebBrowser1 için Dock = Fill değişiklikleri yapıldı

    public partial class TamEkranForm : Form
    {
        public TamEkranForm(string filmLink)
        {
            InitializeComponent();
            
            chromiumWebBrowser1.Load(filmLink);

        }
    }
}
