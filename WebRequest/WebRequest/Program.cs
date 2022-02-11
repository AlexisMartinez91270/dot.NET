using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Chap9
{
    class WebRequestResponse
    {
    static void Main(string[] args)
        {
            // syntaxe
            const string syntaxe = "pg URI GET/HEAD";
            
            // nombre d'arguments
            if (args.Length != 2)
            {
                Console.WriteLine(syntaxe);
                return;
            }
            
            // on note l'URI demandée
            string stringURI = args[0];
            string commande = args[1].ToUpper();
            
            // vérification validité de l'URI
            Uri uri = null;
            try
            {
                uri = new Uri(stringURI);
            }
            catch (Exception ex)
            {
                // URI incorrecte
                Console.WriteLine("L'erreur suivante s'est produite : {0}", ex.Message);
                return;
            }
            // vérification de la commande
            if (commande != "GET" && commande != "HEAD")
            {
                // commande incorrecte
                Console.WriteLine("Le second paramètre doit être GET ou HEAD");
                return;
            }
            
            try
            {
                // on configure la requête
                HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
                httpWebRequest.Method = commande;
                httpWebRequest.Proxy = null;
                // on l'exécute
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                // résultat
                Console.WriteLine("---------------------");
                Console.WriteLine("Le serveur {0} a répondu : {1} {2}", httpWebResponse.ResponseUri,
                (int)httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
                // entêtes HTTP
                Console.WriteLine("---------------------");
                foreach (string clé in httpWebResponse.Headers.Keys)
                {
                    Console.WriteLine("{0}: {1}", clé, httpWebResponse.Headers[clé]);
                }
                Console.WriteLine("---------------------");
                // document
                using (Stream stream = httpWebResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        // on affiche la réponse sur la console
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
            catch (WebException e1)
            {
                // on récupère la réponse
                HttpWebResponse httpWebResponse = e1.Response as HttpWebResponse;
                Console.WriteLine("Le serveur {0} a répondu : {1} {2}", httpWebResponse.ResponseUri,
                (int)httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
            }
            catch (Exception e2)
            {
                // on affiche l'exception
                Console.WriteLine("L'erreur suivante s'est produite : {0}", e2.Message);
            }
        }
    }
}