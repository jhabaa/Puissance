using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using AroundTheWorld; // Classe du prof
using Color = System.Drawing.Color;

namespace puissance_4
{
    public partial class client : Form
    {
        List<Pion> jeu = new List<Pion>();
        List<String> placements = new List<String>();
        AsyncClient serveur_distant; //Creation d'un client asynchrone. Nous allons l'instancier par la suite.

        public client()
        {
            InitializeComponent();
            new_game();
            connect_to_server();
        }

        private void new_game()
        {
                for (int b = 0; b < 7; b++)
                {
                    for (int c = 0; c < 6; c++)
                        
                    {
                        Pion grille = new Pion();
                        grille.pictureBox.Location = new Point(b * (grille.pictureBox.Width + 5), c * (grille.pictureBox.Height + 5));
                        grille.SetCoordonates(b, c);
                        grille.pictureBox.Tag = "(" + b + ":" + c + ")";
                        jeu.Add(grille); 
                        panel_jeu.Controls.Add(grille.pictureBox);
                        grille.pictureBox.MouseClick += new MouseEventHandler(place_yellow);
                    }
                }
        }

        public void place_yellow(object sender, MouseEventArgs e)
        {
            PictureBox placement_pion = (PictureBox)sender;
            if (placement_pion.BackColor != Color.Transparent)
            {
                MessageBox.Show("Placement non permis", "Veuillez rejouer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if ( Form1.jeton == true)
            {
                MessageBox.Show("", "Veuillez attendre votre tour", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Pion pion = GetPionByPictureBoxTag(placement_pion.Tag.ToString());
                pion.SetState("player2");
                placements.Add(pion.coordonates);
                serveur_distant.Send(placement_pion.Location);
                Form1.jeton = true; // Le jeton est public static pour limiter la taille du code xD
                serveur_distant.Send("A votre tour.");
            } 
        }

        
        public Pion GetPionByPictureBoxTag(string index)
        {
            Pion result = new Pion();
            foreach (Pion pion in jeu)
            {
                if (pion.coordonates == index)
                {
                    result = pion;
                }
            }

            return result;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void connect_to_server()
        {
            serveur_distant = new AsyncClient();
            serveur_distant.Connect("127.0.0.1", 12345);
            serveur_distant.ClientConnected += client_connected;
            serveur_distant.DataReceived += reception_data;
            serveur_distant.Send("La partie commence");
            
        }

        private void reception_data(AsyncClient client, object data)
        {
            String c = data +"";
            String x = getBetween(c, "X=", ",");
            String y = getBetween(c, "Y=", "}");
            monitor1.AddMessage("================");
            if (!data.ToString().Contains('{')){
                monitor1.AddMessage("Player 1 : " + data.ToString());
            }
            
                for (int a = 0; a < 42; a++)
                {
                    if (jeu[a].pictureBox.Location.X.ToString() == (x) && jeu[a].pictureBox.Location.Y.ToString() == (y))
                    {
                    monitor1.AddMessage("Player 1 : " + data.ToString());
                    jeu[a].SetState("player1");
                    }
                }
            
            
            
        }

        private void client_connected(AsyncClient client)
        {
            monitor1.AddMessage("Connected");
            label1.Text = ("Joueur 2");
        }
        public static string getBetween(string strSource, string strStart, string strEnd) //Methode qui recupère les valeurs X et Y de location dans un String de data.
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void messageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                serveur_distant.Send(messageBox.Text);
            }
        }
    }
}
