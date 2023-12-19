using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    class OstvarenaPotrosnja
    {
        
        public int Sat { get; set; }

        public int Potrosnja { get; set; }

        public string Podrucje { get; set; }

        public OstvarenaPotrosnja(int sat,int potrosnja,string podrucje)
        {
            this.Sat = sat;
            this.Potrosnja = potrosnja;
            this.Podrucje = podrucje;
        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-15} {2,-15}",
                Sat, Potrosnja, Podrucje);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-15} {2,-15} ",
                "SAT", "POTROSNJA", "PODRUCJE");
        }
    }
}
