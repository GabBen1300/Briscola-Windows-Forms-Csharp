using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLCarta;
using System.Windows.Forms;

namespace CAServer
{
	public class Logica
	{
		Carta briscola;
		Giocatore[] giocatori;
		Mazzo mazzo;
		List<Carta> inGioco;
		int nGiocatori;
		int turno;
		Random r;
		EnTipo tipo;
		bool visibile;

		/// <summary>
		/// Costruttore che instanzia un oggetto di tipo logica.
		/// </summary>
		/// <param name="numGiocatori">Numero dei giocatori</param>
		/// <param name="tipo">Tipo delle carte</param>
		/// <param name="visibile">True: le carte sono dotate di eventi e immagini 
		///						   False: le carte hanno solo valore, seme e punteggio</param>
		public Logica(int numGiocatori, EnTipo tipo, bool visibile)
		{
			nGiocatori = numGiocatori;
			Giocatori = new Giocatore[nGiocatori];
			InGioco = new List<Carta>();
			r = new Random();

			this.tipo = tipo;
			this.visibile = visibile;
		}

		//Proprietà

		public Mazzo Mazzo { get => mazzo; set => mazzo = value; }

		public Giocatore[] Giocatori { get => giocatori; set => giocatori = value; }

		public List<Carta> InGioco { get => inGioco; set => inGioco = value; }

		public Carta Briscola { get => briscola; }
		public int Turno
		{
			get
			{
				return turno;
			}

			set
			{
				turno = value;
			}
		}

		//Metodi


		/// <summary>
		/// Inizializza la partita e distribuisce le carte.
		/// </summary>
		/// <param name="nGiocatori"></param>
		public void init()
		{
			if (visibile)
				Mazzo = new Mazzo(tipo, true);
			else
				Mazzo = new Mazzo(tipo, false);


			Mazzo.Mescola();
			briscola = Mazzo[Mazzo.Count - 1];
			turno = r.Next(0, nGiocatori);

			if (nGiocatori == 3)
			{
				mazzo.Pesca(2);
			}
		}

		public void Pesca(Giocatore g)
		{
			g.CarteMano.Add(Mazzo.Pesca());
		}

		/// <summary>
		/// Vincita tra due carte.
		/// </summary>
		/// <param name="c1">Prima carta giocata</param>
		/// <param name="c2">Seconda carta giocata</param>
		/// <returns>Il giocatore che vince</returns>
		public Giocatore VittoriaTurno(Carta c1, Carta c2)
		{
			if (c1.Seme == c2.Seme)
			{
				if (c1.Punteggio == 0 && c2.Punteggio == 0)
				{
					if (c1.Valore > c2.Valore)
					{
						return c1.Possessore;
					}
					else
					{
						return c2.Possessore;
					}
				}
				else if (c1.Punteggio > c2.Punteggio)
				{
					return c1.Possessore;
				}
				else
				{
					return c2.Possessore;
				}
			}
			else
			{
				if (c1.Seme == Briscola.Seme)
				{
					return c1.Possessore;
				}
				else if (c2.Seme == Briscola.Seme)
				{
					return c2.Possessore;
				}
				else
				{
					return c1.Possessore;
				}
			}
		}


		/// <summary>
		/// Vincita tra tre carte.
		/// </summary>
		/// <param name="c1">Prima carta giocata</param>
		/// <param name="c2">Seconda carta giocata</param>
		/// <param name="c3">Terza carta giocata</param>
		/// <returns>Il giocatore che vince</returns>
		public Giocatore VittoriaTurno(Carta c1, Carta c2, Carta c3)
		{
			if (c1.Seme == c2.Seme && c2.Seme == c3.Seme)
			{
				if (c1.Punteggio == c2.Punteggio && c1.Punteggio == c3.Punteggio && c2.Punteggio == c3.Punteggio && c1.Punteggio == 0)
				{
					if (c1.Valore > c2.Valore)
					{
						if (c1.Valore > c3.Valore)
						{
							return c1.Possessore;
						}
						else
						{
							if (c2.Valore > c3.Valore)
							{
								return c2.Possessore;
							}
							else
							{
								return c3.Possessore;
							}
						}
					}
					else if (c2.Valore > c3.Valore)
					{
						return c2.Possessore;
					}
					else
					{
						return c3.Possessore;
					}
				}
				else
				{
					if (c1.Punteggio > c2.Punteggio)
					{
						if (c1.Punteggio > c3.Punteggio)
						{
							return c1.Possessore;
						}
						else
						{
							if (c2.Punteggio > c3.Punteggio)
							{
								return c2.Possessore;
							}
							return c3.Possessore;
						}
					}
					else
					{
						if (c2.Punteggio > c3.Punteggio)
						{
							return c2.Possessore;
						}
						return c3.Possessore;
					}
				}
			}
			else if(c1.Seme == briscola.Seme && c2.Seme == briscola.Seme)
			{
				return VittoriaTurno(c1, c2);
			}
			else if (c1.Seme == briscola.Seme && c3.Seme == briscola.Seme)
			{
				return VittoriaTurno(c1, c3);
			}
			else if (c3.Seme == briscola.Seme && c3.Seme == briscola.Seme)
			{
				return VittoriaTurno(c3, c2);
			}
			else if (c1.Seme == briscola.Seme)
			{
				return c1.Possessore;
			}
			else if (c2.Seme == briscola.Seme)
			{
				return c2.Possessore;
			}
			else if (c3.Seme == briscola.Seme)
			{
				return c3.Possessore;
			}
			else
			{
				return c1.Possessore;
			}
		}


		/// <summary>
		/// Vincita tra quattro carte.
		/// </summary>
		/// <param name="c1">Prima carta giocata</param>
		/// <param name="c2">Seconda carta giocata</param>
		/// <param name="c3">Terza carta giocata</param>
		/// <param name="c4">Quarta carta giocata</param>
		/// <returns>Il giocatore che vince</returns>
		public Giocatore VittoriaTurno(Carta c1, Carta c2, Carta c3, Carta c4)
		{
			if (c1.Seme == c2.Seme && c2.Seme == c3.Seme && c3.Seme == c4.Seme)
			{
				if (c1.Punteggio == c2.Punteggio && c2.Punteggio == c3.Punteggio && c1.Punteggio == 0)
				{
					if (c1.Valore > c2.Valore)
					{
						if (c1.Valore > c3.Valore)
						{
							if (c1.Valore > c4.Valore)
							{
								return c1.Possessore;
							}
							else
							{
								return c4.Possessore;
							}
						}
						else if (c3.Valore > c4.Valore)
						{
							return c3.Possessore;
						}
						return c4.Possessore;
					}
					else if (c2.Valore > c3.Valore)
					{
						if (c2.Valore > c4.Valore)
						{
							return c2.Possessore;
						}
						return c4.Possessore;
					}
					else if (c3.Valore > c4.Valore)
					{
						return c3.Possessore;
					}
					return c4.Possessore;
				}
				else
				{
					if (c1.Punteggio > c2.Punteggio)
					{
						if (c1.Punteggio > c3.Punteggio)
						{
							if (c1.Punteggio > c4.Punteggio)
							{
								return c1.Possessore;
							}
							else
							{
								return c4.Possessore;
							}
						}
						else if (c3.Punteggio > c4.Punteggio)
						{
							return c3.Possessore;
						}
						return c4.Possessore;
					}
					else if (c2.Punteggio > c3.Punteggio)
					{
						if (c2.Punteggio > c4.Punteggio)
						{
							return c2.Possessore;
						}
						return c4.Possessore;
					}
					else if (c3.Punteggio > c4.Punteggio)
					{
						return c3.Possessore;
					}
					return c4.Possessore;
				}
			}
			else if (c1.Seme == c2.Seme && c2.Seme == c3.Seme)
			{
				if (c3.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c2, c3);
				}
				return c3.Possessore;
			}
			else if (c1.Seme == c2.Seme && c2.Seme == c4.Seme)
			{
				if (c2.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c2, c4);
				}
				return c2.Possessore;
			}
			else if (c2.Seme == c3.Seme && c3.Seme == c4.Seme)
			{
				if (c1.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c2, c3, c4);
				}
				return c1.Possessore;
			}
			else if (c1.Seme == c2.Seme)
			{
				if (c3.Seme == c4.Seme && c4.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c2);
				}
				return VittoriaTurno(c3, c4);
			}
			else if (c1.Seme == c3.Seme)
			{
				if (c2.Seme == c4.Seme && c4.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c3);
				}
				return VittoriaTurno(c2, c4);
			}
			else if (c1.Seme == c4.Seme)
			{
				if (c2.Seme == c4.Seme && c4.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c2, c4);
				}
				return VittoriaTurno(c1, c3);
			}
			else if (c2.Seme == c3.Seme)
			{
				if (c1.Seme == c4.Seme && c4.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c3);
				}
				return VittoriaTurno(c2, c3);
			}
			else if (c2.Seme == c4.Seme)
			{
				if (c1.Seme == c2.Seme && c2.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c3);
				}
				return VittoriaTurno(c2, c4);
			}
			else if (c3.Seme == c4.Seme)
			{
				if (c1.Seme == c2.Seme && c2.Seme != Briscola.Seme)
				{
					return VittoriaTurno(c1, c2);
				}
				return VittoriaTurno(c3, c4);
			}
			else if (c1.Seme == Briscola.Seme)
			{
				return c1.Possessore;
			}
			else if (c2.Seme == Briscola.Seme)
			{
				return c2.Possessore;
			}
			else if (c3.Seme == Briscola.Seme)
			{
				return c3.Possessore;
			}
			else if (c4.Seme == Briscola.Seme)
			{
				return c4.Possessore;
			}
			return c1.Possessore;
		}
	}
}
