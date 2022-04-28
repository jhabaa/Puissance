using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AroundTheWorld
{
    /* Cette classe donner un comportement de type "command line" à un textBox. L'idée c'est de pouvoir 
     * afficher des informations ligne par ligne et de définir le nombre maximum de lignes. Cette classe
     * est uitilisée pour afficher ce qui se passe au niveau du serveur.
     */
    class Monitor : TextBox
    {
        public int NumberOfLines{get;set;}

        public Monitor()
        {
            Multiline = true;
            BackColor = Color.Black;
            ForeColor = Color.Green;
            NumberOfLines = 2;
        }
        public void AddMessage(string msg)
        {
            this.AppendText(msg + "\r\n");

            /*
             * La classe limite  le nombre de lignes affichées. Je commente ici pour afficher toutes les lignes en ajoutant un scroll dans le form. Voilà
             */
            /*while (Lines.Length > NumberOfLines + 1)
                Text = Text.Remove(0, Lines[0].Length+2);*/
        }
    }
}
