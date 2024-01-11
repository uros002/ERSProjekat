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
    public class UpdateGeografskaPodrucjaDB
    {


        public void UpdateGeografskaPodrucja(GeografskaPodrucja geoPod, string path)
        {
            XmlDocument xmlGeoPod = new XmlDocument();

            xmlGeoPod.Load(path + "\\" + "geografska_podrucja.xml");

            XmlNodeList targetNodes = xmlGeoPod.SelectNodes("//GEOGRAFSKA_PODRUCJA/Podrucje");
            foreach (XmlNode node in targetNodes)
            {
                XmlNode sirina = node.SelectSingleNode("Sirina");
                if (sirina != null)
                {
                    if (sirina.InnerText.Equals(geoPod.SirinaPodrucja))
                    {
                        XmlNode naziv = node.SelectSingleNode("Naziv");
                        if (naziv != null)
                        {
                            naziv.InnerText = geoPod.NazivPodrucja.ToString();
                        }
                    }
                }
            }


            xmlGeoPod.Save(path + "\\" + "geografska_podrucja.xml");

        }
    }
}
