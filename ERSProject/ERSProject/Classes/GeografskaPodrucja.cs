using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    public class GeografskaPodrucja
    {
        public int SifraPodrucja { get; set; }

        public string NazivPodrucja { get; set; }

        public string SirinaPodrucja { get; set; }

        public GeografskaPodrucja(int sifra,string naziv,string sirina)
        {
            this.SifraPodrucja = sifra;
            this.NazivPodrucja = naziv;
            this.SirinaPodrucja = sirina;
        }

    }
}
