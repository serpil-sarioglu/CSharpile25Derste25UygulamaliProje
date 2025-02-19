using System;
using System.Windows.Forms;

namespace Proje_Nesne_Hareketleri
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Top -= 5;
            if (pictureBox1.Top <= 25)
            {
               timer1.Stop();
               timer2.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left += 5;
            if (pictureBox1.Left >= 550)
            {
                timer2.Stop();               
                //MessageBox.Show($"Left: {pictureBox1.Left}");
                timer3.Start();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox1.Top += 5;
            if (pictureBox1.Top >= 380)
            {
                timer3.Stop();
                //MessageBox.Show($"Top: {pictureBox1.Top}");
                timer4.Start();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left -= 5;
            if (pictureBox1.Left <= 40)
            {
                timer4.Stop();
                timer1.Start();
            }
        }
    }
}
