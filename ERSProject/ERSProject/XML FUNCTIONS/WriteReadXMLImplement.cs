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
    class WriteReadXMLImplement : IReadWriteXML
    {
        public int ReadFromXML(string document)
        {
            int brojac = 0;
            XmlDocument xmlDoc = new XmlDocument();
            string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
            xmlDoc.Load(path + document);
            XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
            XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
            XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");

            List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();
            List<PrognoziranaPotrosnja> PrognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();

            //Console.WriteLine(document.Split('_')[0]);
            if (document.Split('_')[0].Equals("ostv"))
            {
                for (int i = 0; i < sat.Count; i++)
                {
                    int tmp = Convert.ToInt32(sat[i].InnerText);
                    brojac++;
                    OstvarenaPotrosnja nova = new OstvarenaPotrosnja(Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                    ostvarenaPotrosnjaLista.Add(nova);

                    WriteToXML(Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());

                }

                Console.WriteLine(OstvarenaPotrosnja.GetFormattedHeader());

                foreach(OstvarenaPotrosnja it in ostvarenaPotrosnjaLista)
                {
                    Console.WriteLine(it);
                }
            }
            else if (document.Split('_')[0].Equals("prog"))
            {
                for (int i = 0; i < sat.Count; i++)
                {
                    int tmp = Convert.ToInt32(sat[i].InnerText);
                    brojac++;
                    PrognoziranaPotrosnja nova = new PrognoziranaPotrosnja(Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                    PrognoziranaPotrosnjaLista.Add(nova);

                    WriteToXML(Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());

                }

                Console.WriteLine(PrognoziranaPotrosnja.GetFormattedHeader());

                foreach (PrognoziranaPotrosnja it in PrognoziranaPotrosnjaLista)
                {
                    Console.WriteLine(it);
                }
            }

            Console.WriteLine(brojac.ToString());

            return brojac;
        }

        public void WriteToXML(int sat,int load,string oblast)
        {

            XmlDocument xmldoc = new XmlDocument();
            string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\prog_potrosnja.xml";
            xmldoc.Load(path);
            XmlNode Stavka = xmldoc.CreateElement("Stavka" );

            XmlNode Sat = xmldoc.CreateElement("Sat");
            Sat.InnerText = sat.ToString();
            Stavka.AppendChild(Sat);

            XmlNode Load = xmldoc.CreateElement("Load");
            Load.InnerText = load.ToString();
            Stavka.AppendChild(Load);

            XmlNode Oblast = xmldoc.CreateElement("Oblast");
            Oblast.InnerText = oblast.ToString();
            Stavka.AppendChild(Oblast);

            xmldoc.DocumentElement.AppendChild(Stavka);

            xmldoc.Save(path);
        }
    }
}
