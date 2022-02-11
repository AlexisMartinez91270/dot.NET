using System;
using System.IO;
using System.Net;
namespace Chap9
{
    public class Program
    {
    public static void Main(string[] args)
        {
            // syntaxe : [prog] Uri
            const string syntaxe = "pg URI";
            // nombre d'arguments
            if (args.Length != 1)
            {
                Console.WriteLine(syntaxe);
                return;
            }
       
            // on note l'URI demandée
            string stringURI = args[0];
            
            // vérification validité de l'URI
            if (!stringURI.StartsWith("http://"))
            {
                Console.WriteLine("Indiquez une Url de la forme http://machine[:port]/document");
                return;
            }
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
            
            try
            {
               // création client web
                using (WebClient client = new WebClient())
                {
                    // ajout d'un entête HTTP 
                    client.Headers.Add("user-agent", "st");
                    using (Stream stream = client.OpenRead(uri))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            // affichage réponse du serveur web
                            Console.WriteLine(reader.ReadToEnd());
                            // affichage entêtes réponse du serveur
                            Console.WriteLine("---------------------");
                            foreach (string clé in client.ResponseHeaders.Keys)
                            {
                                Console.WriteLine("{0}: {1}", clé, client.ResponseHeaders[clé]);
                            }
                            Console.WriteLine("---------------------");
                        }
                    }
                }
            }
            catch (WebException e1)
            {
                Console.WriteLine("L'exception suivante s'est produite : {0}", e1);
            }
            catch (Exception e2)
            {
                Console.WriteLine("L'exception suivante s'est produite : {0}", e2);
            }
        }
    }
}