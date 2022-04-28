using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AroundTheWorld;

namespace puissance_4
{
    public partial class Form1 : Form
    {
        List<Pion> jeu = new List<Pion>();
        List<String> placements = new List<String>();
        List<AsyncClient> connected_Clients = new List<AsyncClient>();
        AsyncServer jeu_serveur;
        AsyncClient client_distant;
        public static Boolean jeton = true; //Le jeton est utilisé pour savoir à qui de jouer. Pas besoin de l'envoyer par réseau.... non.
        public Form1()
        {
            InitializeComponent();
            new_game();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void new_game()
        {
                for(int b=0; b<7; b++)
                {
                    for(int c=0; c<6; c++)
                    {
                        Pion grille = new Pion();
                        grille.pictureBox.Location = new Point(b * (grille.pictureBox.Width + 5), c * (grille.pictureBox.Height + 5));
                       
                        grille.SetCoordonates(b, c);
                        grille.pictureBox.Tag = "("+b+":"+c+")";
                        jeu.Add(grille);
                        panel_jeu.Controls.Add(grille.pictureBox);
                        grille.pictureBox.MouseClick +=new MouseEventHandler(place_red); 
                    }
                }
        }


        private void place_red(object sender, MouseEventArgs e)
        {
            PictureBox placement_pion = (PictureBox)sender;
            if(placement_pion.BackColor != Color.Transparent)
            {
                MessageBox.Show("Placement non permis", "Veuillez rejouer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else
            if (jeton == false)
            {
                MessageBox.Show("","Veuillez attendre votre tour", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //Si un déplacement est possible, on recupère le pion associé à la pictureBox sur laquelle nous avons cliqué
                Pion pion = GetPionByPictureBoxTag(placement_pion.Tag.ToString());
                //Ensuite nous changeons son état. Automatiqument la fonction SetState ajustera la couleur en fonction de l'état donné. player1 pour le joueur 1 et player2 pour le joueur 2
                pion.SetState("player1");
                //Une fois qu'une case n'est plus vide, nous pouvons l'ajouter à la liste de nos placements. Elle nous permettra de determiner si nous avons un 4
                placements.Add(pion.coordonates);
                //Nous envoyons ensuite la position de la picture box à travers le réseau. 
                //Je tiens à préciser qu'utiliser le réseau pour ça est un peu Overkill
                client_distant.Send(placement_pion.Location);
                //On met le jeton à faux pour empêcher au joueur de joeur 2 fois
                jeton = false;
                //Et nous envoyons le tour sur le moniteur. Classe donnée par le prof
                client_distant.Send("A votre tour");
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

        private void button_online_Click(object sender, EventArgs e)
        {
            jeu_serveur = new AsyncServer("127.0.0.1", 12345);
            jeu_serveur.ClientAccepted += nouveau_joueur;
            
            Button go_offline = (Button)sender;
            monitor1.AddMessage("Looking for a player");
            client host = new client();
            go_offline.Text = "go offline";
            host.Show();
            jeu_serveur.Start();

        }

        private void nouveau_joueur(AsyncClient client)
        {
            client_distant = client;
            client_distant.DataReceived += reception_données;
            client_distant.ClientDisconnected += fermeture;
            connected_Clients.Add(client);
            monitor1.AddMessage("New player : " + client.ClientSocket.RemoteEndPoint);
            label2.Text = "Joueur 1";
            if (jeton == true)
            {
                monitor1.AddMessage("Au tour du player1" );
            }
        }

        //Focntion qui actualise le jeton et l'affichage de tours
        public void GameUpdate(Boolean jeton, Label label)
        {
            
        }

        private void fermeture(AsyncClient client, string message)
        {
            connected_Clients.Remove(client);
            monitor1.AddMessage("Client disconnected");
        }

        private void reception_données(AsyncClient client, object data)
        {
            String c = data + "";
            String x = getBetween(c, "X=", ",");
            String y = getBetween(c, "Y=", "}");
            monitor1.AddMessage("================");
            if (!data.ToString().Contains('{'))
            {
                monitor1.AddMessage("Player 2 : " + data.ToString());
            }
            for (int a = 0; a < 42; a++)
            {
                if (jeu[a].pictureBox.Location.X.ToString() == (x) && jeu[a].pictureBox.Location.Y.ToString() == (y))
                {
                    monitor1.AddMessage("Player 2 : " + data.ToString());
                    jeu[a].SetState("player2");
                }
            }
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
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

        //Vérification du vainqueur
        public void GameOverCheck() { 
            
        }

        

        private void messageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                client_distant.Send(messageBox.Text);
            }
        }
    }
}
