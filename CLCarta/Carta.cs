using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLCarta
{
	public enum EnSeme
	{
		Coppe,
		Denari,
		Bastoni,
		Spade
	}

	public partial class Carta : UserControl
	{
		const int ALT = 160;
		const int LARG = 100;

		Image immagine;
		Image retro;
		EnSeme seme;
		Giocatore possessore;

		int valore;
		int punteggio;
		bool coperta = false;
		bool selezionata = false;
		EnTipo tipo;

		//Costruttori

		/// <summary>
		/// Costruttore della carta che deve essere visualizzata
		/// </summary>
		/// <param name="valore"></param>
		/// <param name="seme"></param>
		/// <param name="tipo"></param>
		/// <param name="img"></param>
		public Carta(int valore, EnSeme seme, EnTipo tipo, Image img, Image coperta) : this(valore, seme, tipo)
		{
			Size = new Size(LARG, ALT);
			Location = new Point(0, 0);

			immagine = img;
			retro = coperta;

			Paint += UCCarta_Paint;
		}

        /// <summary>
        /// Costruttore della carta che non deve essere visualizzata
        /// </summary>
        /// <param name="valore"></param>
        /// <param name="seme"></param>
        /// <param name="tipo"></param>
        public Carta(int valore, EnSeme seme, EnTipo tipo)
		{
			Valore = valore;
			Seme = seme;
			this.tipo = tipo;
		}

		//Proprietà

		/// <summary>
		/// Ottiene o imposta lo stato della carta true = coperta, false = non coperta.
		/// </summary>
		public bool Coperta
		{
			get
			{
				return coperta;
			}
			set
			{
				coperta = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Ottiene o imposta il seme della carta.
		/// </summary>
		public EnSeme Seme
		{
			get { return seme; }
			set { seme = value; }
		}

		/// <summary>
		/// Ottiene o imposta il valore della carta.
		/// </summary>
		public int Valore
		{
			get { return valore; }
			set { valore = value; }
		}

		/// <summary>
		/// Ottiene o imposta il punteggio della carta.
		/// </summary>
		public int Punteggio
		{
			get { return punteggio; }
			set { punteggio = value; }
		}

        /// <summary>
        /// Ottiene o imposta il valore della variabile booleana che indica se la carta è selezionata o meno.
        /// </summary>
        public bool Selezionata { get => selezionata; set => selezionata = value; }

		/// <summary>
		/// Ottiene o imposta il giocatore che possiede questa carta.
		/// </summary>
		public Giocatore Possessore { get => possessore; set => possessore = value; }

		//Metodi

		/// <summary>
		/// Disegna la carta.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void UCCarta_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			Rectangle sorgente = new Rectangle((Valore - 1) * immagine.Width / 10,
									(int)Seme * immagine.Height / 4,
									immagine.Width / 10, immagine.Height / 4);

			if (Coperta)
			{
				g.DrawImage(retro,
							0,
							0,
							Width,
							Height);
			}
			else
			{
				//Point center = new Point(Width / 2, Height / 2);
				//g.TranslateTransform(center.X, center.Y);
				//g.RotateTransform(-90);				
				////g.ScaleTransform(.5f, .5f);
				//g.TranslateTransform(-center.X, -center.Y);			


				g.DrawImage(immagine,       //immagine da visualizzare
						new Rectangle(0, 0, Width, Height), //DOVE la visualizzo (tutto l'oggetto)
						sorgente, //TUTTA l'immagine
						GraphicsUnit.Pixel);

				if (Selezionata)
					g.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1);
			}
		}
	}
}
