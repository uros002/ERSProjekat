using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.XML_FUNCTIONS;
using ERSProject.Classes;

namespace ERSProject
{
    public class IzmenaImenaUI
    {

        private WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
        private UpdateGeografskaPodrucjaDB updateGeografskaPodrucja = new UpdateGeografskaPodrucjaDB();

        public void IzmenaImenaGeografskogPodrcuja(string path)
        {
            Console.WriteLine("\nUnesite sifru podrucja");
            string sirina = Console.ReadLine();
            Console.WriteLine("\nUnesite naziv koji zelite da postavite");
            string naziv = Console.ReadLine();

            GeografskaPodrucja geoPod = new GeografskaPodrucja(naziv, sirina);

            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod,path);
            if (exists != 0)
            {
                updateGeografskaPodrucja.UpdateGeografskaPodrucja(geoPod,path);
                Console.WriteLine("Uspesno ste postavili naziv " + geoPod.NazivPodrucja.ToUpper() + " za podrucje sa sifrom " + geoPod.SirinaPodrucja + "\n");
            }
            else
            {
                Console.WriteLine("Ne postoji geografsko podrucje sa takvom sifrom");
            }
            
        }
    }

}
