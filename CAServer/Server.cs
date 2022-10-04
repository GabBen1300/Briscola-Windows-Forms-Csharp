using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing;
using CLCarta;

namespace CAServer
{
	public static class Server
	{
		static Logica logica;
		static TcpClient[] clients;
		static EnTipo tipo;		

		static int nGiocatori;
		static int inizia = 0; //Numero di INIZIA ricevuti
		static int giocate = 0; //Numero di GIOCA ricevuti
		static int pescate = 0; //Numero di PESCA ricevuti
		static int verificate = 0; //Numero di VERIFICA ricevuti

		static void Main(string[] args)
		{
			nGiocatori = Convert.ToInt32(args[0]);
			tipo = (EnTipo)Enum.Parse(typeof(EnTipo), args[1]);

			Console.WriteLine(args[0]);
			Console.WriteLine(args[1]);

			TcpListener server;
			clients = new TcpClient[nGiocatori];
			server = new TcpListener(IPAddress.Any, 10009); //Indirizzi ip da accettare
			int connessi = 0;

			//Attiviamo l'attesa sulla porta definita
			server.Start();

			logica = new Logica(clients.Length, tipo, false);
			logica.init();
			object[] argomenti;

			Thread cerca = new Thread(new ParameterizedThreadStart(RicercaServerRisposta));
			cerca.Start(server);

			for (int i = 0; i < clients.Length; i++)
			{
				Console.WriteLine("In attesa della connessione...");
				//attende una connessione
				clients[i] = server.AcceptTcpClient();

				argomenti = new object[] { clients[i], connessi };
				//appena ricevuta, attiva un Thread apposito
				Thread clientThread = new Thread(new ParameterizedThreadStart(Ricezione));
				//  passando il riferimento alla connessione come parametro
				clientThread.Start(argomenti);

				Console.WriteLine("Il client {0} si è unito alla partita.", i + 1);
				connessi++;
			}

			cerca.Abort();
			Invio("AVVIA", clients);

		}

		//Metodi

		/// <summary>
		/// Invia tramite l'utilizzo del protocollo UDP un messaggio in broadcast e si connette al primo server che risponde. 
		/// </summary>
		/// <param name="args"></param>
		static void RicercaServerRisposta(object args)
		{
			UdpClient c = new UdpClient(10008);
			while (true)
			{
				IPEndPoint ser = new IPEndPoint(IPAddress.Any, 10008);
				byte[] dati = c.Receive(ref ser);
				c.Send(dati, dati.Length, ser);
				string s = Encoding.ASCII.GetString(dati);
				Console.WriteLine(s);
			}
		}

		/// <summary>
		/// Riceve da un singolo client i diversi comandi e successivamente risponde.
		/// </summary>
		/// <param name="args"></param>
		static void Ricezione(object args)
		{
			object[] argomenti = (object[])args;

			TcpClient client = (TcpClient)argomenti[0];
			int idGiocatore = (int)argomenti[1];

			byte[] dati = new byte[500];
			NetworkStream clientStream = client.GetStream();

			int nGiocatori = clients.Length;
			Carta temp;
			Giocatore giocatore = new Giocatore("");
			bool fine = false;


			while (!fine)
			{
				int lettura = clientStream.Read(dati, 0, dati.Length);
				string msg = Encoding.ASCII.GetString(dati, 0, lettura);
				string[] comando = msg.Split('#');
				Console.WriteLine(msg);

				switch (comando[0])
				{
					case "GIOCATORE":

						giocatore = new Giocatore(comando[1]);

						logica.Giocatori[idGiocatore] = giocatore;

						for (int j = 0; j < 3; j++)
						{
							logica.Pesca(giocatore);
						}

						break;
					case "INIZIO":
						inizia++;

						while (inizia < nGiocatori)
						{ }

						string turno;

						if (logica.Turno == idGiocatore)
							turno = "TRUE";
						else
							turno = "FALSE";
						

						string carteMano = "";

						for (int i = 0; i < 3; i++)
						{
							carteMano += giocatore.CarteMano[i].Valore + "#" + giocatore.CarteMano[i].Seme + "#";
						}

						InvioUnico("INIZIO#" + tipo + "#" +
											   nGiocatori + "#" +
											   idGiocatore + "*" +									
											   logica.Briscola.Valore + "#" + logica.Briscola.Seme + "#" +
											   carteMano +
											   turno,
											   clientStream);


						break;
					case "PESCA":
						Thread.Sleep(100);

						pescate++;

						if (pescate == nGiocatori)
						{
							int n = logica.Turno;
							int numeroCarte;
							pescate = 0;

							if (logica.Mazzo.Count > 0)
							{
								numeroCarte = logica.Mazzo.Count - nGiocatori;
								for (int i = 0; i < nGiocatori; i++)
								{
									temp = logica.Mazzo.Pesca();
									logica.Giocatori[n].CarteMano.Add(temp);
									InvioUnico("PESCA#", temp, Convert.ToString(numeroCarte), clients[n].GetStream());						

									if (n == nGiocatori - 1)
										n = 0;
									else
										n++;
								}
							}
						}
						break;

					case "GIOCA":
						giocate++;

						foreach (Carta c in logica.Giocatori[idGiocatore].CarteMano)
						{
							Console.WriteLine(c.Valore + " " + c.Seme);
							if (c.Valore == Convert.ToInt32(comando[1]) && c.Seme == (EnSeme)Enum.Parse(typeof(EnSeme), comando[2]))
							{
								Invio("GIOCA#", c, clients);
								c.Possessore = logica.Giocatori[idGiocatore];
								logica.InGioco.Add(c);
								logica.Giocatori[idGiocatore].CarteMano.Remove(c);
								break;
							}
						}

						if (giocate != nGiocatori)
						{
							InvioUnico("TURNO#FALSE#" + logica.Turno, clients[logica.Turno].GetStream());
							if (logica.Turno == nGiocatori - 1)
								logica.Turno = 0;
							else
								logica.Turno++;
							InvioUnico("TURNO#TRUE", clients[logica.Turno].GetStream());
						}
						else if (giocate == nGiocatori)
						{
							pescate = 0;
							giocate = 0;
							Invio("TURNO#NUOVO", clients);
						}

						break;
					case "VERIFICA":
						verificate++;

						if (verificate == nGiocatori)
						{
							Giocatore vincitore = new Giocatore("");
							verificate = 0;

							if (logica.InGioco.Count > 0)
							{
								switch (nGiocatori)
								{
									case 2:
										vincitore = logica.VittoriaTurno(logica.InGioco[0], logica.InGioco[1]);
										break;
									case 3:
										vincitore = logica.VittoriaTurno(logica.InGioco[0], logica.InGioco[1], logica.InGioco[2]);
										break;
									case 4:
										vincitore = logica.VittoriaTurno(logica.InGioco[0], logica.InGioco[1], logica.InGioco[2], logica.InGioco[3]);
										break;
								}

								for (int i = 0; i < logica.Giocatori.Length; i++)
								{
									if (logica.Giocatori[i] == vincitore)
										logica.Turno = i;
								}

								foreach (Carta c in logica.InGioco)
								{
									logica.Giocatori[logica.Turno].CarteVinte.Add(c);
								}

								//Controllo che le carte non siano finite
								int x = 0;

								for (int i = 0; i < nGiocatori; i++)
								{
									if (logica.Giocatori[i].CarteMano.Count == 0)
										x++;

								}

								if (x == 0)
								{
									Invio("TURNO#PERSO", clients, logica.Turno);
									InvioUnico("TURNO#VINTO#" + Convert.ToString(logica.Giocatori[logica.Turno].Punteggio), clients[logica.Turno].GetStream());
								}
							}

							giocate = 0;

							logica.InGioco.Clear();

							foreach (Giocatore g in logica.Giocatori)
							{
								if (giocatore.CarteMano.Count == 0)
									fine = true;
							}

						}



						break;
				}
			}

			int max = 0;

			foreach (Giocatore g in logica.Giocatori)
			{
				if (g.Punteggio > max)
					max = g.Punteggio;
			}

			for (int i = 0; i < logica.Giocatori.Length; i++)
			{
				if (logica.Giocatori[i].Punteggio == max)
					InvioUnico("PARTITAFINITA#VINTO", clients[i].GetStream());
				else if (logica.Giocatori[i].Punteggio == (40 / nGiocatori))
					InvioUnico("PARTITAFINITA#PAREGGIO", clients[i].GetStream());
				else
					InvioUnico("PARTITAFINITA#PERSO", clients[i].GetStream());
			}
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

		/// <summary>
		/// Invia due messaggio con una carta a un singolo client data una NetworkStream.
		/// </summary>
		/// <param name="mess">Messaggio da inviare</param>
		/// <param name="c">Carta da inviare</param>
		/// <param name="mess2">Secondo messaggio da inviare</param>
		/// <param name="st">Networkstream a cui inviare il messaggio</param>
		static void InvioUnico(string mess, Carta c, string mess2, NetworkStream st)
		{
			mess += c.Valore + "#" + c.Seme + "#" + mess2;
			ASCIIEncoding encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(mess);
			st.Write(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// Invia un messaggio a tutti i client.
		/// </summary>
		/// <param name="mess">Messaggio da inviare</param>
		/// <param name="clients">Carta da inviare</param>
		static void Invio(string mess, TcpClient[] clients)
		{
			for (int i = 0; i < clients.Length; i++)
			{
				NetworkStream clientStream = clients[i].GetStream();
				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer = encoder.GetBytes(mess);

				clientStream.Write(buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// Invia un messaggio con una carta a tutti i client.
		/// </summary>
		/// <param name="mess">Messaggio da inviare</param>
		/// <param name="c">Carta da inviare</param>
		/// <param name="clients">Vettore dei client a cui viene inviato il messaggio</param>
		private static void Invio(string mess, Carta c, TcpClient[] clients)
		{
			mess += c.Valore + "#" + c.Seme;

			for (int i = 0; i < clients.Length; i++)
			{
				NetworkStream st = clients[i].GetStream();
				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer = encoder.GetBytes(mess);
				st.Write(buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// Invia un messaggio con una carta a tutti i client escluso uno. 
		/// </summary>
		/// <param name="mess">Messaggio da inviare</param>
		/// <param name="clients">Vettore dei client a cui viene inviato il messaggio</param>
		/// <param name="id">Indice del client a cui non viene inviato il messaggio</param>
		static void Invio(string mess, TcpClient[] clients, int id)
		{
			for (int i = 0; i < clients.Length; i++)
			{
				if (i != id)
				{
					NetworkStream clientStream = clients[i].GetStream();
					ASCIIEncoding encoder = new ASCIIEncoding();
					byte[] buffer = encoder.GetBytes(mess);

					clientStream.Write(buffer, 0, buffer.Length);
				}
			}
		}

	}
}
