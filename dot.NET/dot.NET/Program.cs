using System;
using System.Net;

namespace Chap9
{
    class Program
    {
        static void Main(string[] args)
        {
            // affiche le nom de la machine locale
            // puis donne interactivement des infos sur les machines réseau
            // identifiées par un nom ou une adresse IP

            // machine locale
            Console.WriteLine("Machine Locale= {0}", Dns.GetHostName());
         
            // question-réponses interactives
            string machine;
            IPHostEntry ipHostEntry;
            while (true)
            {
                // saisie du nom ou de l'adresse IP de la machine recherchée
                Console.Write("Machine recherchée (rien pour arrêter) : ");
                machine = Console.ReadLine().Replace(" ", "").ToLower();

                // fini ?
                if (machine == "") return;
                // gestion exception
                try
                {
                    // recherche machine
                    ipHostEntry = Dns.GetHostEntry(machine);
                    // le nom de la machine
                    Console.WriteLine("Machine : " + ipHostEntry.HostName);
                    // les adresses IP de la machine liste
                    Console.WriteLine("Liste");
                    Console.Write("Adresses IP : {0}", ipHostEntry.AddressList[0]);
                    for (int i = 1; i < ipHostEntry.AddressList.Length; i++)
                    {
                        Console.Write(", {0}", ipHostEntry.AddressList[i]);
                    }
                    Console.WriteLine();
                    // les adresses IP de la machine String
                    Console.WriteLine("String");
                    Console.Write("Adresses IP : {0}", ipHostEntry.AddressList[0].ToString());
                    for (int i = 1; i < ipHostEntry.AddressList.Length; i++)
                    {
                        Console.Write(", {0}", ipHostEntry.AddressList[i].ToString());
                    }
                    Console.WriteLine();
                    // les adresses IP de la machine Int
                    Console.WriteLine("Int");
                    Console.Write("Adresses IP : {0}", BitConverter.ToInt32(ipHostEntry.AddressList[0].GetAddressBytes()));
                    for (int i = 1; i < ipHostEntry.AddressList.Length; i++)
                    {
                        Console.Write(", {0}", BitConverter.ToInt32(ipHostEntry.AddressList[i].GetAddressBytes()));
                        //Console.WriteLine(", {0}", new IPAddress(Convert.ToInt64(BitConverter.ToInt32(ipHostEntry.AddressList[i].GetAddressBytes()))));
                    }
                    Console.WriteLine();
                    // les alias de la machine
                    if (ipHostEntry.Aliases.Length != 0)
                    {
                        Console.Write("Alias : {0}", ipHostEntry.Aliases[0]);
                        for (int i = 1; i < ipHostEntry.Aliases.Length; i++)
                        {
                            Console.Write(", {0}", ipHostEntry.Aliases[i]);
                        }
                        Console.WriteLine();
                    }
                }   
                catch 
                { 
                    // la machine n'existe pas
                    Console.WriteLine("Impossible de trouver la machine [{0}]", machine);
                }
            }
        }
    }
}
