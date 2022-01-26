using System;
using System.Net;

namespace Chap9
{
    class Program
    {
        static string ToAddr(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
        }
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
                    Console.WriteLine("Tableau");

                    int lastindex = ipHostEntry.AddressList.Length - 1;
                    byte[] bytes = ipHostEntry.AddressList[lastindex].GetAddressBytes();

                    Console.Write("Adresses IP : {0}", bytes[0]); 
                    for (int i = 1; i < bytes.Length; i++)
                    {
                        Console.Write(".{0}", bytes[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    // les adresses IP de la machine String
                    Console.WriteLine("String");
                    Console.Write("Adresses IP : {0}", ipHostEntry.AddressList[0]);
                    for (int i = 1; i < ipHostEntry.AddressList.Length; i++)
                    {
                        Console.Write(", {0}", ipHostEntry.AddressList[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    // les adresses IP de la machine Int Convertion
                    Console.WriteLine("Int Convertion");
                    Console.Write("Adresses IP : {0}", Math.Abs(BitConverter.ToInt32(ipHostEntry.AddressList[0].GetAddressBytes())));
                    for (int i = 1; i < ipHostEntry.AddressList.Length; i++)
                    {
                        Console.Write(", {0}", Math.Abs(BitConverter.ToInt32(ipHostEntry.AddressList[i].GetAddressBytes())));
                        //Console.WriteLine(", {0}", new IPAddress(Convert.ToInt64(BitConverter.ToInt32(ipHostEntry.AddressList[i].GetAddressBytes()))));
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    // les adresses IP de la machine Int Calcul
                    Console.WriteLine("Int Calcul");
                    int IPint = (bytes[0] * 2 ^ 24 + bytes[1] * 2 ^ 16 + bytes[2] * 2 ^ 8 + bytes[3]) +Int32.MinValue;
                     IPint = (bytes[0] <<24 + bytes[1] <<16 + bytes[2] <<8+ bytes[3]) + Int32.MinValue;

                    int IPint2 = (bytes[0] * 2 ^ 24 + bytes[1] * 2 ^ 16 + bytes[2] * 2 ^ 8 + bytes[3]);
                    IPint2 = (bytes[0] << 24 + bytes[1] << 16 + bytes[2] << 8 + bytes[3]) ;

                   // int IPint2 = (int)3120562176;
                    Console.Write("Adresses IP : {0}", IPint);
                    Console.WriteLine();
                    Console.WriteLine();
                    // Convertit int en IP
                    Console.WriteLine("Adresse IP");
                    Console.Write("Adresses IP : {0}", ToAddr(Math.Abs(BitConverter.ToInt32(ipHostEntry.AddressList[0].GetAddressBytes()))));
                    for (int i = 1; i < ipHostEntry.AddressList.Length; i++)
                    {
                        Console.Write(", {0}", ToAddr(Math.Abs(BitConverter.ToInt32(ipHostEntry.AddressList[i].GetAddressBytes()))));
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
