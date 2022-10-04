using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CAServer;
using CLCarta;

namespace WFBriscola
{
	public partial class GiocatoreSingolo : Form
	{
		const int NGIOCATORI = 2;
		Logica logica;
		Giocatore persona;
		Giocatore bot;
		int giocate;
		int turno;
		EventHandler CambiaTurno;

		private int Turno
		{
			get
			{
				return turno;
			}

			set
			{
				if (value == 0)
				{
					lblTurnoG1.BackColor = Color.Blue;
					lblTurnoG2.BackColor = Color.Red;
				}
				else
				{
					lblTurnoG1.BackColor = Color.Red;
					lblTurnoG2.BackColor = Color.Blue;
				}

				CambiaTurno?.Invoke(logica.Giocatori[value], EventArgs.Empty);
				turno = value;
			}
		}

		public GiocatoreSingolo()
		{
			InitializeComponent();
			CambiaTurno += g_CambiaTurno;
		}

		private void btnInizio_Click(object sender, EventArgs e)
		{
			logica = new Logica(NGIOCATORI, EnTipo.Trentine, true);
			logica.init();
			Turno = logica.Turno;
			btnInizio.Visible = btnInizio.Enabled = false;
			Init();

		}

		private void Init()
		{
			pBriscola.Controls.Add(logica.Briscola);
			logica.Briscola.Coperta = false;

			persona = logica.Giocatori[0] = new Giocatore("");
			bot = logica.Giocatori[1] = new IntelligenzaArtificiale("");

			for (int i = 0; i < 3; i++)
			{
				persona.CarteMano.Add(logica.Mazzo.Pesca());
				bot.CarteMano.Add(logica.Mazzo.Pesca());
			}

			//Configure le carte che possiedo
			VisualizzaCarteG1();

			//Cofiguro le carte possedute dall'avversario
			VisualizzaCarteBot();

		}

		private void Carta_Click(object sender, EventArgs e)
		{
			Carta c = sender as Carta;

			if (turno == 0 && giocate < NGIOCATORI - 1)
			{
				if (c.Selezionata)
				{
					c.Selezionata = false;
					c.Possessore = persona;
					logica.InGioco.Add(c);
					pGiocate.Controls.Add(c);
					giocate++;

					if (Turno == NGIOCATORI - 1)
						Turno = 0;
					else
						Turno++;
				}

			}

		}

		private void g_CambiaTurno(object sender, EventArgs e)
		{
			Carta tmp;
			IntelligenzaArtificiale g;
			if (sender as object is IntelligenzaArtificiale)
			{
				tmp = (sender as IntelligenzaArtificiale).Gioca();
				logica.InGioco.Add(tmp);
				pGiocate.Controls.Add(tmp);
				VisualizzaCarteBot();
				giocate++;
			}
				
		}

		private void Carta_MouseLeave(object sender, EventArgs e)
		{
			Carta c = sender as Carta;

			c.Selezionata = false;
			c.Invalidate();
		}

		private void Carta_MouseEnter(object sender, EventArgs e)
		{
			Carta c = sender as Carta;

			c.Selezionata = true;
			c.Invalidate();
		}

		private void VisualizzaCarteG1()
		{
			foreach (Carta c in persona.CarteMano)
			{
				pG1.Controls.Add(c);
				c.Location = new Point(pG1.Controls.IndexOf(c) * c.Width, 0);
				c.Coperta = false;
				c.MouseEnter += Carta_MouseEnter;
				c.MouseLeave += Carta_MouseLeave;
				c.Click += Carta_Click;
				c.Invalidate();
			}
		}

		private void VisualizzaCarteBot()
		{
			foreach (Carta c in bot.CarteMano)
			{
				pG2.Controls.Add(c);
				c.Location = new Point(pG2.Controls.IndexOf(c) * c.Width, 0);
				c.Coperta = false;
			}
		}
	}
}
