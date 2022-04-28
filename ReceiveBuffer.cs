using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AroundTheWorld
{
    /* La classe ReceiveBuffer a été créée pour la gestion de la récéption de données provenant de clients distants.
     * La réception de données se fait à l'aide des méthodes asynchrones Begin et EndReceive. La méthode BeginReceive
     * prends en paramètre un buffer d'une taille fixe qui sera remplie lors de l'appel à la méthode EndReceive.
     * Si la taille des données transmises dépasse la taille du buffer, la réception de la totalité des données doit
     * se faire en plusieurs fois. Cette classe offre les outils nécessaires permettant de reconstituer les données 
     * complètes.
     * Les données transmises arrivent sous forme d'un tableau de byte. Ce tableau de byte doit ensuite être transformée
     * en données interprétables. Par exemple si les données sont une chaine de caractères, chaque byte correspondra à 
     * un caractère de cette chaine. Mais lorse que les données représentent des données plus complexes (une instance
     * de classe), l'interprétation du tableau de byte n'est plus trivial. Il faut à l'envoi des données, transformer 
     * l'objet en tableau de byte (sérialisation) et à la réception, transformer le tableau de byte en objet (Désérialisation).
     * La sérialisation et la désérialisation doivent se faire selon un certain format. Les plus connus sont l'XMl, le JSON
     * et le binaire. Dans notre cas on utilisera le format binaire (BinayFormatter).
     */
    class ReceiveBuffer
    {
        public const int BufferSize = 4096;
        public byte[] tempBuffer = new byte[BufferSize];
        private MemoryStream memStream = new MemoryStream();

        public int Length
        {
            get { return memStream.GetBuffer().Length; }
        }
        
        public void Append(int length)
        {
            /* Pour reconstituer la totalité des données, on utilise un memoryStream qui permet d'écrire 
             * des données dans la RAM de l'ordinateur. tempBuffer contient les données écrites par la méthode 
             * EndReceive. A chaque appel de la méthode Append, on accumule les données de tempsBuffer dans 
             * le memoryStream.
             */
            memStream.Write(tempBuffer, 0, length);
        }

        public object Deserialize()
        {
            /* Avant de désérialiser le contenu du memoryStream, il est nécessaire de définir la position du 
             * memoryStream à l'origine de celui-ci. En effet le memoryStream fonctionne avec un système de 
             * curseur que l'on peut placer où l'on veut dans le tableau de données qu'il représente. La position
             * désigne à quel byte on lit ou on écrit dans ce tableau. La méthode Write déplace automatiquement 
             * ce curseur à la fin du tableau, afin d'éviter d'écraser des données lors d'un prochain Write.
             */
            BinaryFormatter formatter = new BinaryFormatter();
            object data;
            memStream.Seek(0, SeekOrigin.Begin);

            data = formatter.Deserialize(memStream);

            memStream.Close();

            return data;

        }
    }
}
