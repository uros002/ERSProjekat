using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
     public class RelativnoOdstupanje
    {
        public double IzracunajRelativnoOdstupanje(int ostvarena,int prognozirana)
        {
            return Math.Abs(ostvarena - prognozirana) / (double)ostvarena * 100;
        }
    }
}
