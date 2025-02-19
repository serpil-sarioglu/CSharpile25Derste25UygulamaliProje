namespace KelimeOgren
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblIngilizce = new System.Windows.Forms.Label();
            this.txtIngilizce = new System.Windows.Forms.TextBox();
            this.lblTurkce = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSure = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDogruKelimeSayisi = new System.Windows.Forms.Label();
            this.txtTurkce = new System.Windows.Forms.TextBox();
            this.lblCevap = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblKullanici = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblIngilizce
            // 
            this.lblIngilizce.AutoSize = true;
            this.lblIngilizce.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblIngilizce.Location = new System.Drawing.Point(43, 46);
            this.lblIngilizce.Name = "lblIngilizce";
            this.lblIngilizce.Size = new System.Drawing.Size(83, 21);
            this.lblIngilizce.TabIndex = 0;
            this.lblIngilizce.Text = "İngilizce:";
            // 
            // txtIngilizce
            // 
            this.txtIngilizce.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtIngilizce.Location = new System.Drawing.Point(147, 46);
            this.txtIngilizce.Name = "txtIngilizce";
            this.txtIngilizce.Size = new System.Drawing.Size(207, 28);
            this.txtIngilizce.TabIndex = 10;
            // 
            // lblTurkce
            // 
            this.lblTurkce.AutoSize = true;
            this.lblTurkce.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTurkce.Location = new System.Drawing.Point(50, 95);
            this.lblTurkce.Name = "lblTurkce";
            this.lblTurkce.Size = new System.Drawing.Size(72, 21);
            this.lblTurkce.TabIndex = 2;
            this.lblTurkce.Text = "Türkçe:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(534, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Süre:";
            // 
            // lblSure
            // 
            this.lblSure.AutoSize = true;
            this.lblSure.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSure.Location = new System.Drawing.Point(617, 53);
            this.lblSure.Name = "lblSure";
            this.lblSure.Size = new System.Drawing.Size(31, 21);
            this.lblSure.TabIndex = 4;
            this.lblSure.Text = "90";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(410, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "Doğru Kelime Sayısı:";
            // 
            // lblDogruKelimeSayisi
            // 
            this.lblDogruKelimeSayisi.AutoSize = true;
            this.lblDogruKelimeSayisi.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDogruKelimeSayisi.Location = new System.Drawing.Point(617, 91);
            this.lblDogruKelimeSayisi.Name = "lblDogruKelimeSayisi";
            this.lblDogruKelimeSayisi.Size = new System.Drawing.Size(21, 21);
            this.lblDogruKelimeSayisi.TabIndex = 6;
            this.lblDogruKelimeSayisi.Text = "0";
            // 
            // txtTurkce
            // 
            this.txtTurkce.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTurkce.Location = new System.Drawing.Point(147, 88);
            this.txtTurkce.Name = "txtTurkce";
            this.txtTurkce.Size = new System.Drawing.Size(207, 28);
            this.txtTurkce.TabIndex = 1;
            this.txtTurkce.TextChanged += new System.EventHandler(this.txtTurkce_TextChanged);
            // 
            // lblCevap
            // 
            this.lblCevap.AutoSize = true;
            this.lblCevap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCevap.Location = new System.Drawing.Point(143, 133);
            this.lblCevap.Name = "lblCevap";
            this.lblCevap.Size = new System.Drawing.Size(55, 21);
            this.lblCevap.TabIndex = 8;
            this.lblCevap.Text = "cevap";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblKullanici
            // 
            this.lblKullanici.AutoSize = true;
            this.lblKullanici.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullanici.Location = new System.Drawing.Point(43, 9);
            this.lblKullanici.Name = "lblKullanici";
            this.lblKullanici.Size = new System.Drawing.Size(79, 21);
            this.lblKullanici.TabIndex = 11;
            this.lblKullanici.Text = "kullanıcı";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 173);
            this.Controls.Add(this.lblKullanici);
            this.Controls.Add(this.lblCevap);
            this.Controls.Add(this.txtTurkce);
            this.Controls.Add(this.lblDogruKelimeSayisi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblSure);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTurkce);
            this.Controls.Add(this.txtIngilizce);
            this.Controls.Add(this.lblIngilizce);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIngilizce;
        private System.Windows.Forms.TextBox txtIngilizce;
        private System.Windows.Forms.Label lblTurkce;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSure;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDogruKelimeSayisi;
        private System.Windows.Forms.TextBox txtTurkce;
        private System.Windows.Forms.Label lblCevap;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblKullanici;
    }
}

