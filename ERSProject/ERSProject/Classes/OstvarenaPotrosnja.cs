using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    class OstvarenaPotrosnja : IPotrosnja
    {
        
        public int Sat { get; set; }

        public int Potrosnja { get; set; }

        public string Podrucje { get; set; }

        public DateTime Date { get; set; }

        public int DateHour { get; set; }

        public int DateMinute { get; set; }

        public int DateSecond { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }





        public OstvarenaPotrosnja(string document,int sat,int potrosnja,string podrucje)
        {
            this.Sat = sat;
            this.Potrosnja = potrosnja;
            this.Podrucje = podrucje;
            this.FileName = document;
            this.Date = DateTime.Now;
            this.DateHour = DateTime.Now.Hour;
            this.DateMinute = DateTime.Now.Minute;
            this.DateSecond = DateTime.Now.Second;
            
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
