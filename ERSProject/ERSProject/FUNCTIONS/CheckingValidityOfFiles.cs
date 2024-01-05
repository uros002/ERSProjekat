using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.Classes;
using System.Xml;
using System.IO;




namespace ERSProject.FUNCTIONS
{
    class CheckingValidityOfFiles
    {

        /*
        private SettingUpPaths setUpPaths = new SettingUpPaths();
        public void CheckingValidFiles()
        {
            List<string> Paths = setUpPaths.SettUpPathsRead();
            int brojac = 0;
            foreach (string path in Paths)
            {
                XmlDocument xmlDoc = new XmlDocument();
                //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                // xmlDoc.Load(path + document);
                xmlDoc.Load(path);
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

                    foreach (OstvarenaPotrosnja it in ostvarenaPotrosnjaLista)
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
            }

            Console.WriteLine(brojac.ToString());

            
        

    }
    
        */
    }
}
