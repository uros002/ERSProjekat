using ERSProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ERSProject
{
    class ProveraNevalidnosti
    {
        private void LogInvalidFile(string path,string document, XmlDocument xmlDoc)
        {
            NeispravniPodaci neispravniPodaci = new NeispravniPodaci(path,document);

            // Evidentirajte neispravne podatke u novi XML fajl
            XmlDocument invalidXmlDoc = new XmlDocument();
            invalidXmlDoc.Load(path+"\\"+"invalid_files.xml");


            XmlNode invalidFileNode = invalidXmlDoc.CreateElement("InvalidFile");
            // invalidXmlDoc.DocumentElement.AppendChild(invalidFileNode);

            XmlNode fileNameNode = invalidXmlDoc.CreateElement("FileName");
            fileNameNode.InnerText = neispravniPodaci.imeFajla;
            invalidFileNode.AppendChild(fileNameNode);

            XmlNode locationNode = invalidXmlDoc.CreateElement("Location");
            locationNode.InnerText = neispravniPodaci.lokacija;
            invalidFileNode.AppendChild(locationNode);

            XmlNode timeNode = invalidXmlDoc.CreateElement("Time");
            timeNode.InnerText = $"{neispravniPodaci.sat}:{neispravniPodaci.minuti}:{neispravniPodaci.sekunde}";
            invalidFileNode.AppendChild(timeNode);

            invalidXmlDoc.DocumentElement.AppendChild(invalidFileNode);

            invalidXmlDoc.Save(path+"\\"+"invalid_files.xml");

        }


        public void CheckAndLogInvalidFiles(string path,string document)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //  xmlDoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + document);
            xmlDoc.Load(path+"\\" + document);

            if (!IsValid(xmlDoc))
            {
                LogInvalidFile(path,document, xmlDoc);
            }
        }




        private bool IsValid(XmlDocument xmlDoc)
        {
            XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
            XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
            XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");

            // Provera broja sati u danu
            int brojSatiUDanu = 24;  // Možete prilagoditi ovo prema potrebi

            if (sat.Count < brojSatiUDanu - 1 || sat.Count > brojSatiUDanu + 1)
            {
                return false;
            }

            // Provera formata i unosa za svaku oblast
            Dictionary<string, int> oblasti = new Dictionary<string, int>();

            for (int i = 0; i < sat.Count; i++)
            {
                int satVrednost, potrosnjaVrednost;

                if (!int.TryParse(sat[i].InnerText, out satVrednost) ||
                    !int.TryParse(potrosnja[i].InnerText, out potrosnjaVrednost))
                {
                    return false;  // Ako format sati ili potrošnje nije ispravan
                }

                string oblastVrednost = oblast[i].InnerText;

                // Provera unosa za svaku oblast
                if (!oblasti.ContainsKey(oblastVrednost))
                {
                    oblasti[oblastVrednost] = 1;
                }
                else
                {
                    oblasti[oblastVrednost]++;
                }
            }

            // Provera da li svaka oblast ima tačno onoliko redova koliko ima sati u danu
            foreach (var pair in oblasti)
            {
                if (pair.Value != brojSatiUDanu)
                {
                    return false;
                }
            }

            return true;  // Ako sve provere prolaze
        }
    }
}
