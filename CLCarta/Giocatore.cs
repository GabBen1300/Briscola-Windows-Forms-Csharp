using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using CLCarta;

namespace CLCarta
{
	public class Giocatore
	{
		string nome;
		int punti; //AHIA CHE ERRORE
		List<Carta> carteMano;
		List<Carta> carteVinte;

		public Giocatore(string nome)
		{
			this.Nome = nome;
			carteMano = new List<Carta>();
			carteVinte = new List<Carta>();
		}

		public List<Carta> CarteMano { get => carteMano; set => carteMano = value; }
		public List<Carta> CarteVinte { get => carteVinte; set => carteVinte = value; }
		public string Nome { get => nome; set => nome = value; }
		public int Punteggio
		{
			get
			{
				punti = 0;

				foreach (Carta c in carteVinte)
				{
					punti += c.Punteggio;
				}

				return punti;
			}
		}
	}
}
