using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLCarta;

namespace WFBriscola
{
    public partial class FormDinamica : Form
    {
        Carta c;
        Carta b;

        public FormDinamica(int numeroG)
        {
            InitializeComponent();
          
            foreach (Panel item in this.Controls)
            {
                if (item != pGiocate)
                {
                    item.BorderStyle = BorderStyle.Fixed3D;

                    if (item == pG1 || item == pG2)
                    {
                        item.Size = new Size(c.Width * 3, c.Height + 5);
                    }
                    else if (item == pBriscola)
                    {
                        item.Controls.Clear();
                        item.Controls.Add(b);
                    }
                    else
                    {
                        item.Size = new Size(c.Height, c.Width * 3);
                    }

                }

            }

            pG3.Visible = false;
            pG4.Visible = false;

            if (numeroG == 2)
            {
                pG1.Location = new Point(pG1.Left, 375);
                pG1.Size = new Size(c.Width * 3, c.Height + 5);
            }
            else if (numeroG == 3)
                pG3.Visible = true;
            else if (numeroG == 4)
                pG4.Visible = true;
        }

    }
}
