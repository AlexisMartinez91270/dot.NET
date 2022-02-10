using System;using System.IO;using System.Net.Sockets;namespace Chap9{
    class Program {
        static void Main(string[] args)
        {                       try
            {
                // on se connecte au service
                using (TcpClient tcpClient = new TcpClient("54.221.78.73", 80))
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
                                    string demande = Console.ReadLine();
                                    // fini ?
                                    if (demande.Trim().ToLower() == "bye")
                                        break;
                                    // on envoie la demande au serveur
                                    writer.WriteLine(demande);
                                    // on lit la réponse du serveur
                                    string reponse = reader.ReadLine();
                                    // on traite la réponse
                                }
                            }
                        }
                    }
                }
            } 
            catch (Exception e) { 
                // erreur
                //...
            }
        }
    }
}