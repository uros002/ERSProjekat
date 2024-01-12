using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.XML_FUNCTIONS;
using ERSProject.Classes;

namespace ERSProject
{
    public class UnosPodrucjaUI
    {
        private WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
        public void UnosGeografskihPodrucja(string path)
        {
            Console.WriteLine("\nUnesite sifru podrucja");
            string sifra = Console.ReadLine();
            Console.WriteLine("\nUnesite naziv podrucja");
            string naziv = Console.ReadLine();

            GeografskaPodrucja geoPod = new GeografskaPodrucja(naziv, sifra.ToUpper());

            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod,path);
            if (exists != 0 )
            {
                Console.WriteLine("Vec postoji geografsko podrucje koje ste zeleli da unesete");
            }
            else
            {
                RWGeografskaPodrucja.WriteGeografskaPodrucja(geoPod,path);
            }

        }

    }
}
