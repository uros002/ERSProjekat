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

        public void WriteGeografskaPodrucja(GeografskaPodrucja geoPod)
        {
            XmlDocument xmlGeoPod = new XmlDocument();

           // xmlGeoPod.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");
            xmlGeoPod.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");

            XmlNode Podrucje = xmlGeoPod.CreateElement("Podrucje");

            XmlNode Sirina = xmlGeoPod.CreateElement("Sirina");
            Sirina.InnerText = geoPod.SirinaPodrucja;
            Podrucje.AppendChild(Sirina);

            XmlNode Naziv = xmlGeoPod.CreateElement("Naziv");
            Naziv.InnerText = geoPod.NazivPodrucja;
            Podrucje.AppendChild(Naziv);

            xmlGeoPod.DocumentElement.AppendChild(Podrucje);

          //  xmlGeoPod.Save("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");
            xmlGeoPod.Save("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");
        }

        public int ReadGeografskaPodrucja(GeografskaPodrucja geoPod)
        {
            int flag = 0;
            XmlDocument xmlGeoPod = new XmlDocument();

            //xmlGeoPod.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");
            xmlGeoPod.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");

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

        public void UpdateGeografskaPodrucja(GeografskaPodrucja geoPod)
        {
            XmlDocument xmlGeoPod = new XmlDocument();

            //xmlGeoPod.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");
            xmlGeoPod.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");

            XmlNodeList targetNodes = xmlGeoPod.SelectNodes("//GEOGRAFSKA_PODRUCJA/Podrucje");
            foreach(XmlNode node in targetNodes)
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


            //xmlGeoPod.Save("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");
            xmlGeoPod.Save("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\geografska_podrucja.xml");

        }

    }
}
