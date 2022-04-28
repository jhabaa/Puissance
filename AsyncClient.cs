using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AroundTheWorld
{
    /* La classe AsyncServer et AsyncClient utilisent les méthodes asynchrones de la classe Socket. En effet, les méthodes
    * concernant la communication réseau (connexion, récéption, envoi, acceptation, etc...) peuvent prendre du temps, ou
    * même être totalement bloquantes car elles dépendent de ce qui se passe à l'autre bout de la communication. Afin
    * d'éviter que l'application se fige en attendant une réponse du réseau, on exécute ces fonctions dans un thread 
    * séparé. Comme on ne peut plus prévoir le moment où ces fonctions s'exécutent, on utilise le déclenchement d'events
    * pour en avertir les classes "abonnées".
    * En pratique on exécute une méthode "Begin..." de la classe Socket qui exécutera une méthode "callback" dans un nouveau
    * thread, dans laquelle on exécute la méthode, lente ou blocante, "End...".
    */
    public class AsyncClient
    {
        /* La classe AsyncClient a été créée pour simplifier la gestion des connexion grâce à la mise à disposition
         * d'events. Elle repose entièrement sur la classe Socket. Elle s'occupe principalement de l'envoi et la réception 
         * de données. Elle est utilisée dans la classe AsyncServer pour représenter les clients connéctés.
         * A titre d'exemple on déclare ses propres delegates. On aurait très bien pu se contenter des delegates existants
         * EventHandler<T> du namespace System.
         */

        public delegate void ClientConnectedHandler(AsyncClient client);
        public delegate void DataSendHandler(AsyncClient client);
        public delegate void DataReceivedHandler(AsyncClient client, object data);
        public delegate void ClientDisconnectedHandler(AsyncClient client, string message);
        public delegate void ConnectionRefusedHandler(AsyncClient client, string message);

        public event DataSendHandler DataSent;
        public event DataReceivedHandler DataReceived;
        public event ClientConnectedHandler ClientConnected;
        public event ClientDisconnectedHandler ClientDisconnected;
        public event ConnectionRefusedHandler ConnectionRefused;

        public Socket ClientSocket { get { return clientSocket; } set { clientSocket = value; } }

        private Socket clientSocket;

        #region Constructors
        /* La construction d'un AsyncClient revient à instancier un objet Socket sur lequel se fera la connexion et 
         * l'échange de connées. Il faudra attendre l'appel à la méthode Connect avant de commencer l'échange de données.
         * Si la construction se fait à partir d'un soket ou d'un autre AsyncClient, on tente tout de suite la réception 
         * de données, au cas ou la connexion était déjà établie.
         */
        public AsyncClient()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public AsyncClient(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            receiveData();
        }

        public AsyncClient(AsyncClient client)
        {
            clientSocket = client.ClientSocket;
            receiveData();
        }
        #endregion

        public void Connect(string address, int port)
        {
            if (!clientSocket.Connected)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(address), port);
                clientSocket.BeginConnect(ep, clientConnectedCallback, null);
            }
        }
        public void Disconnect()
        {
            if (clientSocket.Connected)
            {
                clientSocket.Close();
            }
        }

        public void Send(object data)
        {
            if (clientSocket.Connected)
            {
                /* Les données à envoyer doivent être transformés en tableau de byte (serialize).
                 */
                byte[] dataBuffer = serialize(data);

                try
                {
                    clientSocket.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, dataSendCallback, null);
                }
                catch (SocketException e)
                {
                    onClientDisconnected(e.Message);
                }
            }
        }

        public void receiveData()
        {
            if (clientSocket.Connected)
            {
                /* On crée un ReceiveBuffer qui contient un buffer temporaire dans lequel EndReceive écrira les données reçues et
                 * un système de reconstitution des données reçues. Il est important d'envoyer cet objet au callback pour pouvoir
                 * réceptionner la totalité des données en cas de dépassement du buffer temporaire (plus d'informations -> ReceiveBuffer.cs).
                 */
                ReceiveBuffer receiveBuffer = new ReceiveBuffer();
                clientSocket.BeginReceive(receiveBuffer.tempBuffer, 0, ReceiveBuffer.BufferSize, SocketFlags.None, receiveCallback, receiveBuffer);
            }
        }

        private byte[] serialize(object data)
        {
            /* L'objet à envoyer doit être sérialisé avant de pouvoir être envoyé (plus d'informations -> ReceiveBuffer.cs).
             * On utilise ici un BinaryFormatter qui nécessite l'utilisation d'un memoryStream dans lequel seront écrites
             * les données sérialisées.
             */
            BinaryFormatter bin = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            bin.Serialize(mem, data);
            byte[] buffer = mem.GetBuffer();
            mem.Close();
            return buffer;
        }

        #region Callbacks

        private void clientConnectedCallback(IAsyncResult ar)
        {
            /* On tente de se connecter au serveur. Si la connexion est établie, on démarre la réception de 
             * données et on déclenche l'event ClientConnected. Si la connexion est refusée ou prends trop
             * de temps, la méthode EndAccept lève un exception. On déclenche alors l'event ConnectionRefused
             * en transmettant le message de l'exception (contenant à priori la raison du problème).
             */
            try
            {
                clientSocket.EndConnect(ar);
            }
            catch (SocketException e)
            {
                onConnectionRefused(e.Message);
            }

            if (clientSocket.Connected)
            {
                receiveData();
                onClientConnected(this);
            }
        }

        private void dataSendCallback(IAsyncResult ar)
        {
            if (clientSocket.Connected)
            {
                clientSocket.EndSend(ar);
                onDataSent(this);
            }
            else
            {
                onClientDisconnected("unable to send data : client disconnected");
            }
        }

        private void receiveCallback(IAsyncResult ar)
        {
            /* La méthode EndReceive écrit les données reçues dans le buffer passé en paramètre de la méthode
             * BeginReceive et renvoie le nombre de bytes écrits. EndReceive est en attente de nouvelles données
             * provenant du client distant. Si la connexion est interrompue, une exception est levée et on déclenche
             * l'event ClientDisconnected.
             * On récupère l'objet receiveBuffer afin de pouvoir reconstituer les données reçue en une ou plusieurs
             * fois (plus d'explications dans le fichier ReceiveBuffer.cs)
             */
            int dataReceivedSize = 0;
            try
            {
                dataReceivedSize = clientSocket.EndReceive(ar);
            }
            catch (Exception e)
            {
                if (!clientSocket.Connected)
                {
                    onClientDisconnected(e.Message);
                }
            }
            ReceiveBuffer receiveBuffer = (ReceiveBuffer)ar.AsyncState;

            if (dataReceivedSize > 0)
            {
                /* Si des données ont été reçues, on les accumule dans le memoryStream, et si après réception il y en a encore,
                 * on on recommence la réception asynchrone en mettant le receiveBuffer en stateObject afin que ce soit toujours
                 * le même qui s'accumule.
                 * Si toutes les données ont été reçues (Available = false), on désérialise le buffer et on déclenche l'event
                 * DataReceived en mettant les données désérialisées en paramètre.
                 */
                receiveBuffer.Append(dataReceivedSize);
                if (clientSocket.Available > 0)
                    clientSocket.BeginReceive(receiveBuffer.tempBuffer, 0, ReceiveBuffer.BufferSize, SocketFlags.None, receiveCallback, receiveBuffer);
                else
                {
                    object data = receiveBuffer.Deserialize();
                    onDataReceived(data);
                    receiveData();
                }
            }
        }
        #endregion

        #region Raising event methods

        /* Pour les explications, voir AsyncServer.cs
         */

        private void onClientDisconnected(string message)
        {
            if (ClientDisconnected != null)
            {
                if (ClientDisconnected.Target is System.Windows.Forms.Control)
                {
                    ((System.Windows.Forms.Control)ClientDisconnected.Target).Invoke(ClientDisconnected, this, message);
                }
                else
                {
                    ClientDisconnected(this, message);
                }
            }
        }

        private void onConnectionRefused(string message)
        {
            if (ConnectionRefused.Target is System.Windows.Forms.Control)
            {
                ((System.Windows.Forms.Control)ConnectionRefused.Target).Invoke(ConnectionRefused, this, message);
            }
            else
            {
                ConnectionRefused(this, message);
            }
        }

        private void onDataReceived(object data)
        {
            if (DataReceived != null)
            {
                if (DataReceived.Target is System.Windows.Forms.Control)
                {
                    ((System.Windows.Forms.Control)DataReceived.Target).Invoke(DataReceived, this, data);
                }
                else
                {
                    DataReceived(this, data);
                }
            }
        }

        private void onClientConnected(AsyncClient asyncClient)
        {
            if (ClientConnected != null)
            {
                if (ClientConnected.Target is System.Windows.Forms.Control)
                {
                    ((System.Windows.Forms.Control)ClientConnected.Target).Invoke(ClientConnected, this);
                }
                else
                {
                    ClientConnected(this);
                }
            }
        }

        private void onDataSent(AsyncClient asyncClient)
        {
            if (DataSent != null)
            {
                if (DataSent.Target is System.Windows.Forms.Control)
                {
                    ((System.Windows.Forms.Control)DataSent.Target).Invoke(DataSent, this);
                }
                else
                {
                    DataSent(this);
                }
            }
        }
        #endregion


    }
}
