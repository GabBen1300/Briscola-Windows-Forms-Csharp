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
using System.Diagnostics;

namespace WFBriscola
{
    public partial class Menu : Form
    {
        //Parte comunicazione
        byte[] dati = new byte[500];
        TcpClient client;
        IPEndPoint ipServer;
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Thread Ricevi;
        NetworkStream ns;

        Giocatore giocatore;

		//Cosstruttore

        public Menu()
        {
            InitializeComponent();
            btnInizia.Enabled = false;
            CheckForIllegalCrossThreadCalls = false;
            cbTipo.SelectedIndex = 0;
        }

		//Metodi


        private void btnConnetti_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("Immettere nome giocatore!");
                }
                if (!String.IsNullOrWhiteSpace(txtNome.Text))
                {
                    ipServer = new IPEndPoint(CercaServer().Address, 10009);
                    client = new TcpClient();
                    client.Connect(ipServer);
                    btnConnetti.Enabled = false;
                    lblStato.Text = "Connesso";
                    Ricevi = new Thread(new ParameterizedThreadStart(Ricezione));
                    Ricevi.IsBackground = true;
                    Ricevi.Start();
                    ns = client.GetStream();


                    ASCIIEncoding encoder = new ASCIIEncoding();
                    string nomeG = "GIOCATORE#" + txtNome.Text;
                    byte[] buffer = encoder.GetBytes(nomeG);
                    ns.Write(buffer, 0, buffer.Length);
                }
            }
            catch { MessageBox.Show("Errore, non mi sono connesso al server."); }
        }

        private void btnInizia_Click(object sender, EventArgs e)
        {
            giocatore = new Giocatore(txtNome.Text);

            this.Hide();
            Tavolo form2 = new Tavolo(client, giocatore);
            form2.Show();
        }

        private void btnAvviaServer_Click(object sender, EventArgs e)
        {
			string args = nudNumGiocatori.Value + " " + cbTipo.SelectedItem;

            Process[] pname = Process.GetProcessesByName("CAServer");
            if (pname.Length == 0)
                Process.Start("CAServer.exe", args);
            else
                MessageBox.Show("Già aperto");

            btnAvviaServer.Enabled = false;
        }

		/// <summary>
		/// Invia un messaggio in broadcast e si connette al primo server che risponde.
		/// </summary>
		/// <returns>Il server che ha risposto</returns>
        private IPEndPoint CercaServer()
        {
            ipServer = new IPEndPoint(IPAddress.Broadcast, 10008);
            UdpClient c = new UdpClient();
            ASCIIEncoding encoder = new ASCIIEncoding();
            string nomeG = "CERCA";
            byte[] buffer = encoder.GetBytes(nomeG);
            c.Send(buffer, buffer.Length, ipServer);
            ipServer = new IPEndPoint(IPAddress.Any, 10008);
			c.Client.ReceiveTimeout = 5000;
            c.Receive(ref ipServer);
            return ipServer;
        }

        private void Ricezione(object mio)
        {
            while (!btnInizia.Enabled)
            {
                int lettura = ns.Read(dati, 0, dati.Length);
                string msg = Encoding.ASCII.GetString(dati, 0, lettura);

                if (msg == "AVVIA")
                {
                    btnInizia.Enabled = true;
                }
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string aboutTesto = "Made with ♥ by StarByte...\r\n\r\n Ribaga Eros \r\n Benolli Gabriel \r\n Cobbe Roberto \r\n\r\n Copyright © 2018 · StarByte \r\n All rights reserved.";
            aboutTXT.Text = aboutTesto;
        }

		private void btnSingolo_Click(object sender, EventArgs e)
		{
			this.Hide();
			GiocatoreSingolo form2 = new GiocatoreSingolo();
			form2.Show();
		}
	}
}
