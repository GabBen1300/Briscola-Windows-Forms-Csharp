namespace WFBriscola
{
	partial class Menu
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
			this.btnConnetti = new System.Windows.Forms.Button();
			this.btnAvviaServer = new System.Windows.Forms.Button();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.aboutTXT = new System.Windows.Forms.TextBox();
			this.btnAbout = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblStato = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.label2 = new System.Windows.Forms.Label();
			this.nudNumGiocatori = new System.Windows.Forms.NumericUpDown();
			this.cbTipo = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnInizia = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudNumGiocatori)).BeginInit();
			this.SuspendLayout();
			// 
			// btnConnetti
			// 
			this.btnConnetti.Location = new System.Drawing.Point(292, 152);
			this.btnConnetti.Name = "btnConnetti";
			this.btnConnetti.Size = new System.Drawing.Size(222, 23);
			this.btnConnetti.TabIndex = 1;
			this.btnConnetti.Text = "Connetti";
			this.btnConnetti.UseVisualStyleBackColor = true;
			this.btnConnetti.Click += new System.EventHandler(this.btnConnetti_Click);
			// 
			// btnAvviaServer
			// 
			this.btnAvviaServer.Location = new System.Drawing.Point(292, 271);
			this.btnAvviaServer.Name = "btnAvviaServer";
			this.btnAvviaServer.Size = new System.Drawing.Size(222, 23);
			this.btnAvviaServer.TabIndex = 3;
			this.btnAvviaServer.Text = "Avvia server";
			this.btnAvviaServer.UseVisualStyleBackColor = true;
			this.btnAvviaServer.Click += new System.EventHandler(this.btnAvviaServer_Click);
			// 
			// txtNome
			// 
			this.txtNome.Location = new System.Drawing.Point(292, 97);
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(222, 20);
			this.txtNome.TabIndex = 4;
			// 
			// aboutTXT
			// 
			this.aboutTXT.BackColor = System.Drawing.SystemColors.ControlLight;
			this.aboutTXT.Enabled = false;
			this.aboutTXT.Location = new System.Drawing.Point(531, 103);
			this.aboutTXT.Multiline = true;
			this.aboutTXT.Name = "aboutTXT";
			this.aboutTXT.Size = new System.Drawing.Size(159, 191);
			this.aboutTXT.TabIndex = 5;
			// 
			// btnAbout
			// 
			this.btnAbout.Location = new System.Drawing.Point(531, 74);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(159, 23);
			this.btnAbout.TabIndex = 6;
			this.btnAbout.Text = "About Us";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(289, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 18);
			this.label1.TabIndex = 7;
			this.label1.Text = "Nome del giocatore:";
			// 
			// lblStato
			// 
			this.lblStato.Name = "lblStato";
			this.lblStato.Size = new System.Drawing.Size(0, 17);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStato});
			this.statusStrip1.Location = new System.Drawing.Point(0, 314);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(700, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label2.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(289, 218);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "N. giocatori:";
			// 
			// nudNumGiocatori
			// 
			this.nudNumGiocatori.Location = new System.Drawing.Point(380, 218);
			this.nudNumGiocatori.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.nudNumGiocatori.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.nudNumGiocatori.Name = "nudNumGiocatori";
			this.nudNumGiocatori.Size = new System.Drawing.Size(121, 20);
			this.nudNumGiocatori.TabIndex = 9;
			this.nudNumGiocatori.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// cbTipo
			// 
			this.cbTipo.FormattingEnabled = true;
			this.cbTipo.Items.AddRange(new object[] {
            "Trentine",
            "Napoletane"});
			this.cbTipo.Location = new System.Drawing.Point(380, 244);
			this.cbTipo.Name = "cbTipo";
			this.cbTipo.Size = new System.Drawing.Size(121, 21);
			this.cbTipo.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label3.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(289, 244);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 15);
			this.label3.TabIndex = 11;
			this.label3.Text = "Tipo carte:";
			// 
			// btnInizia
			// 
			this.btnInizia.Location = new System.Drawing.Point(292, 123);
			this.btnInizia.Name = "btnInizia";
			this.btnInizia.Size = new System.Drawing.Size(222, 23);
			this.btnInizia.TabIndex = 0;
			this.btnInizia.Text = "Nuova partita";
			this.btnInizia.UseVisualStyleBackColor = true;
			this.btnInizia.Click += new System.EventHandler(this.btnInizia_Click);
			// 
			// Menu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(700, 336);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbTipo);
			this.Controls.Add(this.nudNumGiocatori);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAbout);
			this.Controls.Add(this.aboutTXT);
			this.Controls.Add(this.txtNome);
			this.Controls.Add(this.btnAvviaServer);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnConnetti);
			this.Controls.Add(this.btnInizia);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Menu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Briscola Menù";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudNumGiocatori)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnConnetti;
		private System.Windows.Forms.Button btnAvviaServer;
		private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox aboutTXT;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel lblStato;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudNumGiocatori;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnInizia;
	}
}