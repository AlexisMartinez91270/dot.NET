using System;
using System.IO;
using System.Net.Sockets;

namespace Chap9
{
    // se connecte à un serveur d'écho
    // toute ligne tapée au clavier est reçue en écho
    class ClientEcho
    {
    static void Main(string[] args)
        {
            // syntaxe
            const string syntaxe = "pg machine port";
            
            // nombre d'arguments
            if (args.Length != 2)
            {
                Console.WriteLine(syntaxe);
                return;
            }
            
            // on note le nom du serveur
            string serveur = args[0];
            
            // le port doit être entier >0
            int port = 0;
            if (!int.TryParse(args[1], out port) || port <= 0)
            {
                Console.WriteLine("{0}{1}port incorrect", syntaxe, Environment.NewLine);
                return;
            }
            
            // on peut travailler
            string demande = null; // demande du client
            string réponse = null; // réponse du serveur
            try
            {
                // on se connecte au service
                using (TcpClient tcpClient = new TcpClient(serveur, port))
                {
                    using (NetworkStream networkStream = tcpClient.GetStream())
                    {
                        using (StreamReader reader = new StreamReader(networkStream))
                        {
                            using (StreamWriter writer = new StreamWriter(networkStream))
                            {
                                // flux de sortie non bufferisé
                                writer.AutoFlush = true;
                                // boucle demande - réponse
                                while (true)
                                {
                                    // la demande vient du clavier
                                    Console.Write("Demande (bye pour arrêter) : ");
                                    demande = Console.ReadLine();
                                    // fini ?
                                    if (demande.Trim().ToLower() == "bye")
                                        break;
                                    // on envoie la demande au serveur
                                    writer.WriteLine(demande);
                                    // on lit la réponse du serveur
                                    réponse = reader.ReadLine();
                                    // on traite la réponse
                                    Console.WriteLine("Réponse : {0}", réponse);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // erreur
                Console.WriteLine("L'erreur suivante s'est produite : {0}", e.Message);
            }
        }
    }
}