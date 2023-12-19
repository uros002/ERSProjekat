﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    class PrognoziranaPotrosnja
    {
        
        public int Sat { get; set; }

        public int Potrosnja { get; set; }

        public string Podrucje { get; set; }

        public PrognoziranaPotrosnja(int sat, int potrosnja, string podrucje)
        {
            this.Sat = sat;
            this.Potrosnja = potrosnja;
            this.Podrucje = podrucje;
        }
    }
}
