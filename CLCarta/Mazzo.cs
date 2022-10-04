using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CLCarta
{
	public enum EnTipo
	{
		Trentine,
		Napoletane,
		Siciliane
	}

	public class Mazzo : List<Carta>
	{
		Random r = new Random();
		Carta c;
		Image immagine;
		Image retro;

		public Image Immagine { get => immagine; }

		//Costruttori

		public Mazzo(EnTipo tipo, bool visibile)
		{
			

			if (visibile)
			{
				immagine = UrlImmagine(tipo.ToString());
				retro = UrlImmagine(tipo + "_retro");
				CreaMazzo(tipo, immagine, retro);
			}				
			else
				CreaMazzo(tipo);

		}

		//Metodi

		/// <summary>
		/// Ritorna l'immagine dato il tipo di mazzo.
		/// </summary>
		/// <param name="tipo">Tipo di mazzo</param>
		/// <returns>Immagine delle carte</returns>
		private Image UrlImmagine(string tipo)
		{
			return Image.FromFile(@"Immagini\carte_" + tipo + ".jpg");
		}


		/// <summary>
		/// Mescola in modo casuale le carte nel mazzo.
		/// </summary>
		public void Mescola()
		{
			for (int i = 0; i < 100; i++)
			{
				Carta temp = this[r.Next(0, 40)];
				this.Remove(temp);
				this.Add(temp);
			}
		}

		/// <summary>
		/// Crea un mazzo di un tipo dato.
		/// </summary>
		/// <param name="tipo">Tipo di carte</param>
		private void CreaMazzo(EnTipo tipo)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					c = new Carta(j + 1, (EnSeme)i, tipo);

					switch (c.Valore)
					{
						case 1: c.Punteggio = 11; break;
						case 3: c.Punteggio = 10; break;
						case 8: c.Punteggio = 2; break;
						case 9: c.Punteggio = 3; break;
						case 10: c.Punteggio = 4; break;
					}

					Add(c);
				}
			}
		}

		private void CreaMazzo(EnTipo tipo, Image img, Image coperta)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					c = new Carta(j + 1, (EnSeme)i, tipo, img, coperta);

					switch (c.Valore)
					{
						case 1: c.Punteggio = 11; break;
						case 3: c.Punteggio = 10; break;
						case 8: c.Punteggio = 2; break;
						case 9: c.Punteggio = 3; break;
						case 10: c.Punteggio = 4; break;
					}

					Add(c);
				}
			}
		}

		/// <summary>
		/// Permette di pescare la prima carta del mazzo.
		/// </summary>
		/// <returns>Carta pescata</returns>
		public Carta Pesca()
		{
			Carta c = this[0];
			Remove(this[0]);
			return c;
		}

		/// <summary>
		/// Permette di pescare una carta di un determinato valore ma di seme casuale.
		/// </summary>
		/// <param name="valore">Valore della carta che si vuole pescare</param>
		/// <returns>Carta pescata</returns>
		public Carta Pesca(int valore)
		{
			foreach (Carta carta in this)
			{
				if (carta.Valore == valore)
				{
					Remove(carta);
					break;
				}
			}
			return c;
		}

	}
}
