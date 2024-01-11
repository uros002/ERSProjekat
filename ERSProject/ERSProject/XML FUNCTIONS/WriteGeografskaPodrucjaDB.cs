using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.Classes;
using System.Xml;
using System.IO;

namespace ERSProject.XML_FUNCTIONS
{
    public class WriteGeografskaPodrucjaDB
    {

        public void WriteGeografskaPodrucja(GeografskaPodrucja geoPod, string path)
        {
            XmlDocument xmlGeoPod = new XmlDocument();

            xmlGeoPod.Load(path + "\\" + "geografska_podrucja.xml");

            XmlNode Podrucje = xmlGeoPod.CreateElement("Podrucje");

            XmlNode Sirina = xmlGeoPod.CreateElement("Sirina");
            Sirina.InnerText = geoPod.SirinaPodrucja;
            Podrucje.AppendChild(Sirina);

            XmlNode Naziv = xmlGeoPod.CreateElement("Naziv");
            Naziv.InnerText = geoPod.NazivPodrucja;
            Podrucje.AppendChild(Naziv);

            xmlGeoPod.DocumentElement.AppendChild(Podrucje);

            xmlGeoPod.Save(path + "\\" + "geografska_podrucja.xml");
        }

        public int ReadGeografskaPodrucja(GeografskaPodrucja geoPod,string path)
        {
            int flag = 0;
            XmlDocument xmlGeoPod = new XmlDocument();

            xmlGeoPod.Load(path + "\\" +  "geografska_podrucja.xml");

            XmlNodeList sirina = xmlGeoPod.GetElementsByTagName("Sirina");
            XmlNodeList naziv = xmlGeoPod.GetElementsByTagName("Naziv");

            for (int i = 0; i < sirina.Count; i++)
            {
                if (geoPod.SirinaPodrucja.Equals(sirina[i].InnerText))
                {
                    flag++;
                }
            }
            return flag;

        }


    }
}
