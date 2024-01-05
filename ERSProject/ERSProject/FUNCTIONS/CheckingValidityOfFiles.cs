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

        
        private SettingUpPaths setUpPaths = new SettingUpPaths();
        public List<IPotrosnja> CheckingValidFiles(string document)
        {
            List<string> Paths = setUpPaths.SettUpPathsRead();
            
           
                XmlDocument xmlDoc = new XmlDocument();
                string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                 xmlDoc.Load(path + document);
                //xmlDoc.Load(path);
                XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");

                List<IPotrosnja> PotrosnjaLista = new List<IPotrosnja>();
                

                //Console.WriteLine(document.Split('_')[0]);
                if (document.Split('_')[0].Equals("ostv"))
                {
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        
                        OstvarenaPotrosnja nova = new OstvarenaPotrosnja(path,Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                    PotrosnjaLista.Add(nova);
                    Console.WriteLine(PotrosnjaLista.Count.ToString());
                        
                    }

                }
                else if (document.Split('_')[0].Equals("prog"))
                {
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        
                        PrognoziranaPotrosnja nova = new PrognoziranaPotrosnja(path,Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                    PotrosnjaLista.Add(nova);

                        
                    }

                }

            return PotrosnjaLista;
            
        

    }
    
        
    }
}
