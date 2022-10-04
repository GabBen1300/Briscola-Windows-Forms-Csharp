namespace WFBriscola
{
	partial class GiocatoreSingolo
	{
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Codice generato da Progettazione Windows Form

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			this.pG2 = new System.Windows.Forms.Panel();
			this.pG1 = new System.Windows.Forms.Panel();
			this.pBriscola = new System.Windows.Forms.Panel();
			this.btnInizio = new System.Windows.Forms.Button();
			this.lblNome = new System.Windows.Forms.Label();
			this.pGiocate = new System.Windows.Forms.Panel();
			this.lblTurnoG1 = new System.Windows.Forms.Label();
			this.lblTurnoG2 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblmsg = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblPunteggio = new System.Windows.Forms.Label();
			this.lblCarteRimanenti = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pG2
			// 
			this.pG2.Location = new System.Drawing.Point(12, 12);
			this.pG2.Name = "pG2";
			this.pG2.Size = new System.Drawing.Size(570, 187);
			this.pG2.TabIndex = 0;
			// 
			// pG1
			// 
			this.pG1.Location = new System.Drawing.Point(12, 499);
			this.pG1.Name = "pG1";
			this.pG1.Size = new System.Drawing.Size(570, 151);
			this.pG1.TabIndex = 1;
			// 
			// pBriscola
			// 
			this.pBriscola.Location = new System.Drawing.Point(702, 159);
			this.pBriscola.Name = "pBriscola";
			this.pBriscola.Size = new System.Drawing.Size(355, 316);
			this.pBriscola.TabIndex = 1;
			// 
			// btnInizio
			// 
			this.btnInizio.Location = new System.Drawing.Point(34, 269);
			this.btnInizio.Name = "btnInizio";
			this.btnInizio.Size = new System.Drawing.Size(75, 23);
			this.btnInizio.TabIndex = 2;
			this.btnInizio.Text = "INIZIA";
			this.btnInizio.UseVisualStyleBackColor = true;
			this.btnInizio.Click += new System.EventHandler(this.btnInizio_Click);
			// 
			// lblNome
			// 
			this.lblNome.AutoSize = true;
			this.lblNome.Location = new System.Drawing.Point(674, 12);
			this.lblNome.Name = "lblNome";
			this.lblNome.Size = new System.Drawing.Size(0, 13);
			this.lblNome.TabIndex = 3;
			// 
			// pGiocate
			// 
			this.pGiocate.Location = new System.Drawing.Point(156, 228);
			this.pGiocate.Name = "pGiocate";
			this.pGiocate.Size = new System.Drawing.Size(518, 236);
			this.pGiocate.TabIndex = 4;
			// 
			// lblTurnoG1
			// 
			this.lblTurnoG1.BackColor = System.Drawing.Color.Red;
			this.lblTurnoG1.Location = new System.Drawing.Point(598, 499);
			this.lblTurnoG1.Name = "lblTurnoG1";
			this.lblTurnoG1.Size = new System.Drawing.Size(35, 151);
			this.lblTurnoG1.TabIndex = 5;
			// 
			// lblTurnoG2
			// 
			this.lblTurnoG2.BackColor = System.Drawing.Color.Red;
			this.lblTurnoG2.Location = new System.Drawing.Point(598, 9);
			this.lblTurnoG2.Name = "lblTurnoG2";
			this.lblTurnoG2.Size = new System.Drawing.Size(35, 151);
			this.lblTurnoG2.TabIndex = 6;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblmsg});
			this.statusStrip1.Location = new System.Drawing.Point(0, 663);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1259, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblmsg
			// 
			this.lblmsg.Name = "lblmsg";
			this.lblmsg.Size = new System.Drawing.Size(118, 17);
			this.lblmsg.Text = "toolStripStatusLabel1";
			// 
			// lblPunteggio
			// 
			this.lblPunteggio.AutoSize = true;
			this.lblPunteggio.Location = new System.Drawing.Point(640, 499);
			this.lblPunteggio.Name = "lblPunteggio";
			this.lblPunteggio.Size = new System.Drawing.Size(65, 13);
			this.lblPunteggio.TabIndex = 8;
			this.lblPunteggio.Text = "lblPunteggio";
			// 
			// lblCarteRimanenti
			// 
			this.lblCarteRimanenti.AutoSize = true;
			this.lblCarteRimanenti.Location = new System.Drawing.Point(847, 498);
			this.lblCarteRimanenti.Name = "lblCarteRimanenti";
			this.lblCarteRimanenti.Size = new System.Drawing.Size(35, 13);
			this.lblCarteRimanenti.TabIndex = 9;
			this.lblCarteRimanenti.Text = "label1";
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(1085, 44);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(120, 446);
			this.listBox1.TabIndex = 10;
			// 
			// GiocatoreSingolo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1259, 685);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.lblCarteRimanenti);
			this.Controls.Add(this.lblPunteggio);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.lblTurnoG2);
			this.Controls.Add(this.lblTurnoG1);
			this.Controls.Add(this.pGiocate);
			this.Controls.Add(this.lblNome);
			this.Controls.Add(this.btnInizio);
			this.Controls.Add(this.pBriscola);
			this.Controls.Add(this.pG1);
			this.Controls.Add(this.pG2);
			this.Name = "GiocatoreSingolo";
			this.Text = "GicoatoreSingolo";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pG2;
		private System.Windows.Forms.Panel pG1;
		private System.Windows.Forms.Panel pBriscola;
		private System.Windows.Forms.Button btnInizio;
		private System.Windows.Forms.Label lblNome;
		private System.Windows.Forms.Panel pGiocate;
		private System.Windows.Forms.Label lblTurnoG1;
		private System.Windows.Forms.Label lblTurnoG2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblmsg;
		private System.Windows.Forms.Label lblPunteggio;
		private System.Windows.Forms.Label lblCarteRimanenti;
		private System.Windows.Forms.ListBox listBox1;
	}
}

