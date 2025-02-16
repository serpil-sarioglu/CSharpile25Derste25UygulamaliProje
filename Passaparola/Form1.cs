using System.Drawing;
using System.Windows.Forms;

namespace Passaparola
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int soruNo, dogru = 0, yanlis = 0;
        int toplamSure = 180; // 3 dakika Timer süresi
        private void Form1_Load(object sender, System.EventArgs e)
        {
            textBox1.Enabled = false;
            
            lblSure.Text = "Kalan Süre: 3:00";
            progressBar1.Maximum = toplamSure;
            progressBar1.Value = toplamSure;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            // Timer, 1 saniyede bir çalışıyor
            toplamSure--;

            int dakika = toplamSure / 60;
            int saniye = toplamSure % 60;
            lblSure.Text = $"Kalan Süre: {dakika}:{saniye:D2}";

            // ProgressBar güncelle
            progressBar1.Value = toplamSure;

            if (toplamSure == 0)
            {
                timer1.Stop(); // timer sonlandı
                MessageBox.Show("Süre doldu! Oyun bitti");
                textBox1.Enabled = false;
                linkLabel1.Enabled = false;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cevap = textBox1.Text.Trim().ToLower();

                // Doğru veya yanlış kontrolü
                bool dogruCevap = false;

                switch (soruNo)
                {                 
                    // Cevap kontrolü 
                    case 1: dogruCevap = cevap == "akdeniz"; button1.BackColor = dogruCevap ? Color.Green : Color.Red; break; 
                    case 2: dogruCevap = cevap == "bursa"; button2.BackColor = dogruCevap ? Color.Green : Color.Red; break; 
                    case 3: dogruCevap = cevap == "cuma"; button3.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 4: dogruCevap = cevap == "diyarbakır"; button4.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 5: dogruCevap = cevap == "eski"; button5.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 6: dogruCevap = cevap == "ferman"; button6.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 7: dogruCevap = cevap == "güneş"; button7.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 8: dogruCevap = cevap == "halı"; button8.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 9: dogruCevap = cevap == "ısparta"; button9.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 10: dogruCevap = cevap == "içel"; button10.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 11: dogruCevap = cevap == "jandarma"; button11.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 12: dogruCevap = cevap == "kayısı"; button12.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 13: dogruCevap = cevap == "lale"; button13.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 14: dogruCevap = cevap == "mart"; button14.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 15: dogruCevap = cevap == "ney"; button15.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 16: dogruCevap = cevap == "ozan"; button16.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 17: dogruCevap = cevap == "pırasa"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 18: dogruCevap = cevap == "ramazan"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 19: dogruCevap = cevap == "snake"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 20: dogruCevap = cevap == "tarkan"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 21: dogruCevap = cevap == "umut"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 22: dogruCevap = cevap == "van"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 23: dogruCevap = cevap == "yıldırım"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;
                    case 24: dogruCevap = cevap == "zeytin"; button17.BackColor = dogruCevap ? Color.Green : Color.Red; break;          
                    default: break;
                }

                if (dogruCevap)                
                    lblDogru.Text = (++dogru).ToString();                
                else                 
                    lblYanlis.Text = (++yanlis).ToString();                

                textBox1.Enabled = false;
                textBox1.Clear();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // timer1.Enabled = false olduğunda koşul true döner if bloğuna girer ve timer başlatılır 
            if (!timer1.Enabled) 
            {
                timer1.Start();
            }
            
            linkLabel1.Text = "SONRAKİ";
            soruNo++;
            this.Text = soruNo.ToString();

            if (soruNo == 1)
            {
                richTextBox1.Text = "Ülkemizin güney kısmındaki kıyı bölgesinin adı nedir?";
                button1.BackColor = Color.Yellow;

            }
            if (soruNo == 2)
            {
                richTextBox1.Text = "Yeşil doğasıyla ünlü Marmara Bölgesinde yer alan ilimiz neresidir?";
                button2.BackColor = Color.Yellow;
            }
            if (soruNo == 3)
            {
                richTextBox1.Text = "Müslümanların kutsal günü nedir?";
                button3.BackColor = Color.Yellow;
            }
            if (soruNo == 4)
            {
                richTextBox1.Text = "Karpuzuyla ünlü ilimiz nedir?";
                button4.BackColor = Color.Yellow;
            }
            if (soruNo == 5)
            {
                richTextBox1.Text = "Yeni kelimesinin zıt anlamlısı nedir?";
                button5.BackColor = Color.Yellow;
            }
            if (soruNo == 6)
            {
                richTextBox1.Text = "Padişahın emirlerinin yazılı hali nedir?";
                button6.BackColor = Color.Yellow;
            }
            if (soruNo == 7)
            {
                richTextBox1.Text = "Dünya'nın ısı kaynağı nedir?";
                button7.BackColor = Color.Yellow;
            }
            if (soruNo == 8)
            {
                richTextBox1.Text = "Öğrencilerin karnesinde kötü notlar getirince yerde serili bakıştığı nesnenin adı nedir?";
                button8.BackColor = Color.Yellow;
            }
            if (soruNo == 9)
            {
                richTextBox1.Text = "Gülüyle ünlü ilimiz?";
                button9.BackColor = Color.Yellow;
            }
            if (soruNo == 10)
            {
                richTextBox1.Text = "Mersin'in diğer ismi?";
                button10.BackColor = Color.Yellow;
            }
            if (soruNo == 11)
            {
                richTextBox1.Text = "Silahlı genel kolluk kuvveti?";
                button11.BackColor = Color.Yellow;
            }
            if (soruNo == 12)
            {
                richTextBox1.Text = "Malatya'nın meşhur meyvesi?";
                button12.BackColor = Color.Yellow;
            }
            if (soruNo == 13)
            {
                richTextBox1.Text = "Her yıl bahar aylarında düzenlenen çiçek festivalinin ismi nedir?";
                button13.BackColor = Color.Yellow;
            }
            if (soruNo == 14)
            {
                richTextBox1.Text = "Yılın 3. ayı nedir?";
                button14.BackColor = Color.Yellow;
            }
            if (soruNo == 15)
            {
                richTextBox1.Text = "Üflemeli bir müzik aletinin adı?";
                button15.BackColor = Color.Yellow;
            }
            if (soruNo == 16)
            {
                richTextBox1.Text = "Halk şairinin diğer adı?";
                button16.BackColor = Color.Yellow;
            }
            if (soruNo == 17)
            {
                richTextBox1.Text = "Çocukların pek sevmediği pirinç, havuç gibi sebzelerle yapılan yemek adı?";
                button17.BackColor = Color.Yellow;
            }
            if (soruNo == 18)
            {
                richTextBox1.Text = "11 ayın sultanı?";
                button18.BackColor = Color.Yellow;
            }
            if (soruNo == 19)
            {
                richTextBox1.Text = "İngilizce yılan?";
                button19.BackColor = Color.Yellow;
            }
            if (soruNo == 20)
            {
                richTextBox1.Text = "Türkiye'nin mega starı";
                button20.BackColor = Color.Yellow;
            }
            if (soruNo == 21)
            {
                richTextBox1.Text = "Ümit kelimesinin eş anlamlısı?";
                button21.BackColor = Color.Yellow;
            }
            if (soruNo == 22)
            {
                richTextBox1.Text ="Kahvaltısı ile ünlü ilimiz?";
                button22.BackColor = Color.Yellow;
            }
            if (soruNo == 23)
            {
                richTextBox1.Text = "Şimşek kelimesinin eş anlamlısı?";
                button23.BackColor = Color.Yellow;
            }
            if (soruNo == 24)
            {
                richTextBox1.Text = "Ege bölgesinin en çok ağacı bulunan yağıda yapılan bir kahvaltı besini?";
                button24.BackColor = Color.Yellow;
            }
            textBox1.Enabled = true;
        }
    }
}
