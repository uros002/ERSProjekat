using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
    class Program
    {
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

                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                }

            } while (!input.ToLower().Equals("x"));
        }
    }
}
