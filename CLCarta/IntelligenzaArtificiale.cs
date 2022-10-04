using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLCarta
{
	public class IntelligenzaArtificiale : Giocatore
	{
		bool turno;
		bool giocata;
		Random r = new Random();

		public IntelligenzaArtificiale(string nome) :base(nome)
		{

		}

		public bool Turno { get => turno; set => turno = value; }
		public bool Giocata { get => giocata; set => giocata = value; }

		public Carta Gioca()
		{
			Carta c = CarteMano[r.Next(CarteMano.Count)];
			CarteMano.Remove(c);
			return c;
		}
	}
}
