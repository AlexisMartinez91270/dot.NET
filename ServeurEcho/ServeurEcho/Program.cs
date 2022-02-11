using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

// appel : serveurEcho port
// serveur d'écho
// renvoie au client la ligne que celui-ci lui a envoyée

namespace Chap9
{
    public class ServeurEcho
    {
        public const string syntaxe = "Syntaxe : [serveurEcho] port";

        // programme principal
        public static void Main(string[] args)
        {
            // y-a-t-il un argument ?
            if (args.Length != 1)
            {
                Console.WriteLine(syntaxe);
                return;
            }
            // cet argument doit être entier >0
            int port = 0;
            if (!int.TryParse(args[0], out port) || port <= 0)
            {
                Console.WriteLine("{0} : {1}Port incorrect", syntaxe, Environment.NewLine);
                return;
            }
            // on crée le service d'écoute
            TcpListener ecoute = null;
            int numClient = 0; // n° client suivant
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                // on crée le service - il écoutera sur toutes les interfaces réseau de la machine
                ecoute = new TcpListener(IPAddress.Any, port);
                // on le lance
                ecoute.Start();
                // suivi
                Console.WriteLine("Serveur d'écho lancé sur le port {0}", ecoute.LocalEndpoint);
                // threads de service
                ThreadPool.SetMinThreads(10, 10);
                ThreadPool.SetMaxThreads(10, 10);
                // boucle de service
                TcpClient tcpClient = null;
                // boucle infinie - sera arrêtée par Ctrl-C
                while (true)
                {
                    // attente d'un client
                    tcpClient = ecoute.AcceptTcpClient();
                    // le service est assuré par une autre tâche
                    ThreadPool.QueueUserWorkItem(RecupereClientDemandeEtRepondDemamndeDansFluxReseauSiPasNullOuBye, new Client() { CanalTcp = tcpClient, NumClient = numClient });
                    // client suivant
                    numClient++;
                }
            }
            catch (Exception ex)
            {
                // on signale l'erreur
                Console.WriteLine("L'erreur suivante s'est produite sur le serveur : {0}", ex.Message);
            }
            finally
            {
                // fin du service
                ecoute.Stop();
            }
        }

        // -------------------------------------------------------
        // assure le service à un client du serveur d'écho
        public static void RecupereClientDemandeEtRepondDemamndeDansFluxReseauSiPasNullOuBye(Object infos)
        {
            // on récupère le client qu'il faut servir
            Client client = infos as Client;
            // rend le service au client
            Console.WriteLine("Début de service au client {0}", client.NumClient);
            // exploitation liaison TcpClient
            try
            {
                using (TcpClient tcpClient = client.CanalTcp)
                {
                    using (NetworkStream networkStream = tcpClient.GetStream())
                    {
                        using (StreamReader reader = new StreamReader(networkStream))
                        {
                            using (StreamWriter writer = new StreamWriter(networkStream))
                            {
                                // flux de sortie non bufferisé
                                writer.AutoFlush = true;
                                // boucle lecture demande/écriture réponse
                                string demande = null;
                                while ((demande = reader.ReadLine()) != null)
                                {
                                    // suivi console
                                    Console.WriteLine("<--- Client {0} : {1}", client.NumClient, demande);
                                    // écho de la demande vers le client
                                    DirectoryInfo d = new DirectoryInfo(@"c:\Users\User\Documents\Serial_labs\dot.NET\Images");
                                    FileInfo[] Files = d.GetFiles("*." + demande);                                    
                                    string str = "";
                                    foreach (FileInfo file in Files)
                                    {
                                        str = str + ", " + file.Name;
                                    }

                                    writer.WriteLine("[{0}]", str);
                                    // suivi console
                                    Console.WriteLine("---> Client {0} : {1}", client.NumClient, demande);
                                    // le service s'arrête lorsque le client envoie "bye"
                                    if (demande.Trim().ToLower() == "bye")
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // erreur
                Console.WriteLine("L'erreur suivante s'est produite lors du service au client {0} : { 1}", client.NumClient, e.Message);
            }
            finally
            {
                // fin client
                Console.WriteLine("Fin du service au client {0}", client.NumClient);
            }
        }
    }

    // infos client
    internal class Client
    {
        public TcpClient CanalTcp { get; set; } // liaison avec le client
        public int NumClient { get; set; } // n° de client
    }
}