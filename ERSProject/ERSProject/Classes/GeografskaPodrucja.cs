using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    public class GeografskaPodrucja
    {

        public string NazivPodrucja { get; set; }

        public string SirinaPodrucja { get; set; }

        public GeografskaPodrucja(string naziv,string sirina)
        {
            this.NazivPodrucja = naziv;
            this.SirinaPodrucja = sirina;
        }

    }
}
