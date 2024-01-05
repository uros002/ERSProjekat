using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject;

namespace ERSProject
{
    class Program
    {
        public static readonly WriteReadXMLImplement wrxml = new WriteReadXMLImplement();
        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("1 - Uvoz podataka");
                Console.WriteLine("2 - Ispis podataka o ostvarenoj potrosnji");
                Console.WriteLine("3 - Evidencija geografskih podrucja");
                Console.WriteLine("X - Izlazak");
                input = Console.ReadLine();


                switch (input)
                {
                    case "1":
                        wrxml.ReadFromXML("ostv_2020_05_07.xml");
                        break;
                    case "2":
                    
                        Console.WriteLine("Unesite datum (format: YYYY_MM_DD): ");
                        string date = Console.ReadLine();
                        Console.WriteLine("Unesite geografsku oblast: ");
                        string region = Console.ReadLine();
                        wrxml.IspisPodatakaOstvarenePotrosnje(date, region);
                        break;
                    case "3":
                        break;
                }

            } while (!input.ToLower().Equals("x"));
        }
    }
}
