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

        private SettingUpPaths setUpPaths = new SettingUpPaths();
        private SettingUpPaths setUpPathsWrite = new SettingUpPaths();

        public int ReadFromXML(string document)
        {
            List<string>Paths  = setUpPaths.SettUpPathsRead();
            int brojac = 0;
            foreach (string path in Paths)
            {
                XmlDocument xmlDoc = new XmlDocument();
                //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                 xmlDoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + path);
               // xmlDoc.Load(path);
                XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");
               
               

                List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();
                List<PrognoziranaPotrosnja> PrognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();

                //Console.WriteLine(document.Split('_')[0]);
                if (path.Split('_')[0].Equals("ostv"))
                {
                    //Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        brojac++;
                        OstvarenaPotrosnja nova = new OstvarenaPotrosnja(Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                        ostvarenaPotrosnjaLista.Add(nova);

                        WriteToXML(path, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());

                    }

                    Console.WriteLine(OstvarenaPotrosnja.GetFormattedHeader());

                    foreach (OstvarenaPotrosnja it in ostvarenaPotrosnjaLista)
                    {
                        Console.WriteLine(it);
                    }
                }
                else if (path.Split('_')[0].Equals("prog"))
                {
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        brojac++;
                        PrognoziranaPotrosnja nova = new PrognoziranaPotrosnja(Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                        PrognoziranaPotrosnjaLista.Add(nova);

                        WriteToXML(path,Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());

                    }

                    Console.WriteLine(PrognoziranaPotrosnja.GetFormattedHeader());

                    foreach (PrognoziranaPotrosnja it in PrognoziranaPotrosnjaLista)
                    {
                        Console.WriteLine(it);
                    }
                }
            }

            Console.WriteLine(brojac.ToString());

            return brojac;
        }

        public void WriteToXML(string path,int sat,int load,string oblast)
        {
            //List<string> Paths = setUpPaths.SettUpPathsWrite();
            string pathWrite = "";
           
                XmlDocument xmldoc = new XmlDocument();
            if (path.Split('_')[0].Equals("ostv"))
            {
                pathWrite = "ostv_potrosnja.xml";
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
            }
            else if (path.Split('_')[0].Equals("prog")) {
                pathWrite = "prog_potrosnja.xml";
            }
                //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\prog_potrosnja.xml";
                xmldoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
                XmlNode Stavka = xmldoc.CreateElement("Stavka");

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

                xmldoc.Save("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
            
        }
    }
}
