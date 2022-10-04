using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CLCarta;
using CAServer;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace WFBriscola
{
	public partial class Tavolo : Form
	{
		byte[] dati = new byte[500];
		TcpClient client;
		NetworkStream ns;
		Carta misure = new Carta(0, EnSeme.Bastoni, EnTipo.Napoletane);

		Carta briscola;
		List<Carta> inGioco = new List<Carta>(2);
		List<Carta> carteMano;
		Giocatore[] giocatori;
		Image immagine;
		Image retro;
		EnTipo tipo;
		bool turno = true;
		bool giocata = false;
		bool finite = false;

		Giocatore giocatore;

		Thread ricezione;
		int nGiocatori;
		int idGiocatore;

		//Proprietà

		public bool Turno
		{
			get
			{
				return turno;
			}

			set
			{
				if (value == true)
					lblTurnoG1.BackColor = Color.Blue;
				else
					lblTurnoG1.BackColor = Color.Red;

				turno = value;
			}
		}

		//Costruttore

		public Tavolo(TcpClient client, Giocatore g)
		{
			InitializeComponent();

			this.client = client;
			giocatore = g;
			lblNome1.Text = giocatore.Nome;


			ns = client.GetStream();

			ricezione = new Thread(new ParameterizedThreadStart(Ricezione));
			ricezione.IsBackground = true;

			ricezione.Start(ns);

			CheckForIllegalCrossThreadCalls = false;
		}

		//Metodi

		private Image UrlImmagine(string tipo)
		{
			return Image.FromFile(@"Immagini\carte_" + tipo + ".jpg");
		}

		void Ricezione(object mio)
		{
			NetworkStream ns = mio as NetworkStream;
			string[] comando;

			carteMano = new List<Carta>();
			int t = 0;


			while (true)
			{
				int lettura = ns.Read(dati, 0, dati.Length);
				string msg = Encoding.ASCII.GetString(dati, 0, lettura);
				listBox1.Items.Add(msg);
				comando = msg.Split('#');
				lblmsg.Text = msg;

				if (lblCarteRimanenti.Text == "0" && pBriscola.Controls.Count > 0)
					pBriscola.Controls[0].Dispose();

				switch (comando[0])
				{
					case "INIZIO":

						string[] args;
						string[] carte;						

						args = msg.Split('*')[0].Split('#');
						carte = msg.Split('*')[1].Split('#');

						tipo = (EnTipo)Enum.Parse(typeof(EnTipo), args[1]);
						nGiocatori = Convert.ToInt32(args[2]); //Estraggo il numero di giocatori
						idGiocatore = Convert.ToInt32(args[3]); 

						immagine = UrlImmagine(tipo.ToString());
						retro = UrlImmagine(tipo + "_retro");

						briscola = new Carta(Convert.ToInt32(carte[0]), (EnSeme)Enum.Parse(typeof(EnSeme), carte[1]), EnTipo.Napoletane, immagine, retro);
						VisualizzaCartaInPanel(pBriscola, briscola);

						//Aggiungo le carte possedute dal client

						for (int i = 2; i < 8; i++)
						{
							if (i % 2 == 0)
								carteMano.Add(new Carta(Convert.ToInt32(carte[i]), (EnSeme)Enum.Parse(typeof(EnSeme), carte[i + 1]), tipo, immagine, retro));
						}

						Turno = Boolean.Parse(carte[8].ToLower());
						if (Turno)
							t = 1;
						else
							t = 2;

						ControllaLabel(t);

						foreach (Carta c in carteMano)
						{
							PreparaCarta(c);
						}

						//preparo la visualizzazione della schermata

						if(nGiocatori == 2)
						{
							lblNome3.Visible = false;
							lblTurnoG3.Visible = false;
							pG3.Visible = false;
							lblNome4.Visible = false;
							lblTurnoG4.Visible = false;
							pG4.Visible = false;
						}						
						else if (nGiocatori == 3)
						{
							lblNome4.Visible = false;
							lblTurnoG4.Visible = false;
							pG4.Visible = false;
							RiempiPanel(pG3);
						}
						else if (nGiocatori == 4)
						{							
							RiempiPanel(pG4);
							RiempiPanel(pG3);
						}		

						RiempiPanel(pG2);

						//Turno dei giocatori

						int x = idGiocatore;

						if (idGiocatore == 0)
							x++;

						pG1.Name = "pG" + idGiocatore;
						lblNome1.Name = "lblNome" + idGiocatore;
						lblTurnoG1.Name = "lblTurno" + idGiocatore;

						break;
					case "PESCA":

						Carta pescata = new Carta(Convert.ToInt32(comando[1]), (EnSeme)Enum.Parse(typeof(EnSeme), comando[2]), tipo, immagine, retro);
						carteMano.Add(pescata);
						lblCarteRimanenti.Text = comando[3];

						foreach (Carta c in carteMano)
						{
							PreparaCarta(c);
						}

						if (pescata.Seme == briscola.Seme && pescata.Valore == briscola.Valore)
							finite = true;

						if (comando.Length == 4 && comando[3] == "FINITE")
							finite = true;

						Console.WriteLine(carteMano[carteMano.Count - 1].Valore);

						break;					
					case "GIOCA":

						Thread.Sleep(100);
						Carta tmp;
						t++;
						ControllaLabel(t);


						foreach (Carta c in carteMano)
						{
							if (c.Valore == Convert.ToInt32(comando[1]) && c.Seme == (EnSeme)Enum.Parse(typeof(EnSeme), comando[2]))
							{
								carteMano.Remove(c);
								c.Dispose();
								break;
							}
						}

						foreach (Carta c in carteMano)
						{
							PreparaCarta(c);
						}

						tmp = new Carta(Convert.ToInt32(comando[1]), (EnSeme)Enum.Parse(typeof(EnSeme), comando[2]), tipo, immagine, retro);

						VisualizzaCartaInPanel(pGiocate, tmp);
						inGioco.Add(tmp);

						tmp.Location = new Point(tmp.Width * pGiocate.Controls.IndexOf(tmp), 0);
						tmp.Selezionata = tmp.Coperta = false;

						Invalidate();

						break;
					case "TURNO":

						if (comando[1] == "TRUE")
						{
							t = 1;
							Turno = true;
						}
						else if (comando[1] == "FALSE")
						{
							t = 2;
							Turno = false;
						}
						else
						if (comando[1] == "NUOVO")
						{
							Turno = false;
							InvioUnico("VERIFICA", ns);
							Thread.Sleep(1500);
							foreach (Carta c in inGioco)
							{
								c.Dispose();
							}

							if (!finite)
								InvioUnico("PESCA", ns);
						}
						else if (comando[1] == "VINTO")
						{
							Turno = true;
							t = 1;
							lblPunteggio.Text = "Punti: " + comando[2];
						}
						else if (comando[1] == "PERSO")
						{
							Turno = false;
							t = 2;
						}

						ControllaLabel(t);
						giocata = false;

						break;
					case "PARTITAFINITA":
						MessageBox.Show(comando[1]);
						break;
				}
			}
		}

		/// <summary>
		/// Cambia il colore delle label in base al turno
		/// </summary>
		/// <param name="t">Turno corrente</param>
		private void ControllaLabel(int t)
		{
			foreach (Control control in this.Controls)
			{
				if (control is Label)
				{
					if (control.Name.Contains("lblTurnoG"))
						control.BackColor = Color.Red;
					if (control.Name == "lblTurnoG" + t)
						control.BackColor = Color.Blue;
				}
			}
		}

		/// <summary>
		/// Aggiunge tre carte coperte sui panel degli altri giocatori
		/// </summary>
		/// <param name="p">Panel da riempire</param>
		private void RiempiPanel(Panel p)
		{
			p.Invoke((MethodInvoker)delegate
			{
				for (int i = 0; i < 3; i++)
				{
					Carta c = new Carta(5, EnSeme.Bastoni, tipo, immagine, retro);
					c.Coperta = true;
					p.Controls.Add(c);
					c.Location = new Point((c.Width + 15) * i, 0);
				}
			});

		}

		/// <summary>
		/// Aggiunge e prepara la carta pescata dal client
		/// </summary>
		/// <param name="c">Carta da preparare</param>
		private void PreparaCarta(Carta c)
		{
			VisualizzaCartaInPanel(pG1, c);
			c.Coperta = false;
			c.Location = new Point((c.Width + 15) * carteMano.IndexOf(c));
			c.Click += Carta_Click;
			c.MouseEnter += Carta_MouseEnter;
			c.MouseLeave += Carta_MouseLeave;
		}

		/// <summary>
		/// Visualizza una carta in un panel dato
		/// </summary>
		/// <param name="panel">Panel in cui visualizzare la carta</param>
		/// <param name="c">Carta da visualizzare</param>
		void VisualizzaCartaInPanel(Control panel, Carta c)
		{
			panel.Invoke((MethodInvoker)delegate
			{
				panel.Controls.Add(c);
			});
		}

		/// <summary>
		/// Invia un messaggio a un singolo client data una NetworkStream.
		/// </summary>
		/// <param name="mess">Messaggio da inviare</param>
		/// <param name="st">Networkstream a cui inviare il messaggio</param>
		static void InvioUnico(string mess, NetworkStream st)
		{
			ASCIIEncoding encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(mess);
			st.Write(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// Invia un messaggio con una carta a un singolo client data una NetworkStream.
		/// </summary>
		/// <param name="mess">Messaggio da inviare</param>
		/// <param name="c">Carta da inviare</param>
		/// <param name="st">Networkstream a cui inviare il messaggio</param>
		static void InvioUnico(string mess, Carta c, NetworkStream st)
		{
			mess += c.Valore + "#" + c.Seme;
			ASCIIEncoding encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(mess);
			st.Write(buffer, 0, buffer.Length);
		}

		private void Carta_Click(object sender, EventArgs e)
		{
			Carta tmp = (sender as Carta);

			if (Turno && !giocata)
			{
				if (tmp.Selezionata)
				{
					tmp.Selezionata = false;
					giocata = true;
				}

				InvioUnico("GIOCA#", tmp, ns);
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

		private void btnINIZIA_Click(object sender, EventArgs e)
		{
			Button b = (sender as Button);
			b.Visible = false;
			b.Enabled = false;

			InvioUnico("INIZIO", ns);
		}
	}
}
