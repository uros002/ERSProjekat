using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using ERSProject.Classes;

namespace ERSProject
{
    public class PrikazGeografskihPodrucja
    {

        public void PrikazGeoPod(string path)
        {
            Console.WriteLine(path);
            XmlDocument xmlGeoPod = new XmlDocument();

            xmlGeoPod.Load(path + "\\" + "geografska_podrucja.xml");

            XmlNodeList sirina = xmlGeoPod.GetElementsByTagName("Sirina");
            XmlNodeList naziv = xmlGeoPod.GetElementsByTagName("Naziv");
            Console.WriteLine("   SIFRA\tNAZIV");
            for (int i = 0; i < sirina.Count; i++)
            {
                Console.WriteLine(i.ToString() + ". " + sirina[i].InnerText +"\t" + naziv[i].InnerText);
            }
            
        }
    }
}
