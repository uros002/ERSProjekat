using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
    public class GeografskaPodrucjaUI
    {
        private PrikazGeografskihPodrucja prikaz = new PrikazGeografskihPodrucja();
        private UnosPodrucjaUI unosPodrucjaUI = new UnosPodrucjaUI();
        private IzmenaImenaUI izmena = new IzmenaImenaUI();
        
        public string path;

        
        public void GeoPodUI(string lokacija)
        {
            path = lokacija;
            string input;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju za rad sa geografskim podrucjima:");
                Console.WriteLine("1 - Prikaz svih podrucja");
                Console.WriteLine("2 - Unos novog podrucja");
                Console.WriteLine("3 - Promena imena podrucja");
                Console.WriteLine("X - Izlazak");
                input = Console.ReadLine();


                switch (input)
                {
                    case "1":
                        PrikazPodrucja();
                        break;
                    case "2":
                        UnosPodrucja();
                        break;
                    case "3":
                        IzmenaImena();
                        break;
                    
                }

            } while (!input.ToLower().Equals("x"));
        }

        void PrikazPodrucja()
        {
            prikaz.PrikazGeoPod(path);
        }

        void UnosPodrucja()
        {
            unosPodrucjaUI.UnosGeografskihPodrucja(path);
        }

        void IzmenaImena()
        {
            izmena.IzmenaImenaGeografskogPodrcuja(path);
        }

    }
}
