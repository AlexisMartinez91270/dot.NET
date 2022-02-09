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
                machine = "google.com";

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
                    /*byte[] bytes = ipHostEntry.AddressList[lastindex].GetAddressBytes();*/
                    byte[] bytes = { 255, 255, 255, 255 };

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
                    int p8 = (int) Math.Pow(2, 8);
                    int p16 = (int) Math.Pow(2, 16);
                    int p24 = (int) Math.Pow(2, 24);
                    
                    short s1, s2, s3, s4;
                    s1 = 30000;
                    s2 = 30001;
                    s3 = 30002;
                    s4 = 30003;

                    int i1, i2, i3, i4;
                    i1 = 2_000_000_000;
                    i2 = 2_000_000_001;
                    i3 = 2_000_000_002;
                    i4 = 2_000_000_003;

                    int is1 = s1 + s2 + s3 + s4;
                    long l1 = (long) i1 + i2 + i3 + i4;
                    long l2 = i1 + i2 + i3 + (long)i4;
                    long l3 = (long)i1 + i2 + (i3 + i4);
                    long l4 = (long)i1 + (long)i2 + (long)i3 + (long)i4;
                    int IPint = (bytes[0] * p24) + (bytes[1] * p16) + (bytes[2] * p8) + bytes[3] +Int32.MinValue;
                     IPint = (bytes[0] <<24) + (bytes[1] <<16) + (bytes[2] <<8)+ bytes[3] + Int32.MinValue;

                    long IPint2 = (bytes[0] * (long) p24) + (bytes[1] * p16) + (bytes[2] * p8) + bytes[3];
                    IPint2 = ((long) bytes[0] <<  24) + (bytes[1] << 16) + (bytes[2] << 8) + bytes[3];

                    int IPint3 = (bytes[0] * p24) + (bytes[1] * p16) + (bytes[2] * p8) + bytes[3]
                        ;
                    IPint3 = (bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + bytes[3];

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
