using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    class NeispravniPodaci
    {
        public string imeFajla;
        public string lokacija;
        public int sat;
        public int minuti;
        public int sekunde;

        public NeispravniPodaci(string path,string filePath)
        {
            imeFajla = Path.GetFileName(filePath);
            lokacija = path+"\\"+filePath;
            DateTime vreme = DateTime.Now;
            sat = vreme.Hour;
            minuti = vreme.Minute;
            sekunde = vreme.Second;
        }
    }
}

