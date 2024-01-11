using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
   public class IspisPotrosnje
    {
        private static IspisiOstvarenePotrosnje isp = new IspisiOstvarenePotrosnje();
        
        public void Ispisi(string path)
        {
            
            Console.WriteLine("Unesite datum (format: YYYY/MM/DD): ");
            string date = Console.ReadLine();
            Console.WriteLine("Unesite geografsku oblast: ");
            string region = Console.ReadLine();
            isp.IspisPodatakaOstvarenePotrosnje(path,date, region);
        }
    }
}
