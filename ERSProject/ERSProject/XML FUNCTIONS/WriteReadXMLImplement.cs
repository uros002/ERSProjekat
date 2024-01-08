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
        private List<string> invalidFilesList = new List<string>();

        private void LogInvalidFile(string document, XmlDocument xmlDoc)
        {
            NeispravniPodaci neispravniPodaci = new NeispravniPodaci(document);
            invalidFilesList.Add(document);
            // Evidentirajte neispravne podatke u novi XML fajl
            XmlDocument invalidXmlDoc = new XmlDocument();
            invalidXmlDoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\invalid_files.xml");
           
            
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

                invalidXmlDoc.Save("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\invalid_files.xml");
            
        }
        
         private void FinalizeInvalidFiles()
        {
            XmlDocument invalidXmlDoc = new XmlDocument();
            string invalidFilePath = "C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\invalid_files.xml";
            invalidXmlDoc.Load(invalidFilePath);

            // Obriši postojeće nevažeće podatke
            invalidXmlDoc.DocumentElement.RemoveAll();

            // Dodaj nove nevažeće podatke na osnovu liste
            foreach (string invalidFile in invalidFilesList)
            {
                XmlNode invalidFileNode = invalidXmlDoc.CreateElement("InvalidFile");

                XmlNode fileNameNode = invalidXmlDoc.CreateElement("FileName");
                fileNameNode.InnerText = invalidFile;
                invalidFileNode.AppendChild(fileNameNode);

                // Dodajte dodatne informacije o neispravnim podacima ako je potrebno

                invalidXmlDoc.DocumentElement.AppendChild(invalidFileNode);
            }

            invalidXmlDoc.Save(invalidFilePath);

            // Obavezno čistite listu kako biste je pripremili za sledeće izvršavanje
            invalidFilesList.Clear();
        }
         

        public void CheckAndLogInvalidFiles(string document)
    {
        XmlDocument xmlDoc = new XmlDocument();
      //  xmlDoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + document);
        xmlDoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + document);

        if (!IsValid(xmlDoc))
        {
            LogInvalidFile(document, xmlDoc);
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
          
         

        public int ReadFromXML(string document)
        {
            List<string>Paths  = setUpPaths.SettUpPathsRead();
            int brojac = 0;
            foreach (string path in Paths)
            {
                XmlDocument xmlDoc = new XmlDocument();
                //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                 //xmlDoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + path);

                xmlDoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + path);
                // xmlDoc.Load(path);

                CheckAndLogInvalidFiles(path);    

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


        public void IspisPodatakaOstvarenePotrosnje(string date, string region)
        {
            List<PrognoziranaPotrosnja> prognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
            List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();

            // Učitajte prognoziranu potrošnju
            XmlDocument progXmlDoc = new XmlDocument();
            progXmlDoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\prog_potrosnja.xml");

            // Učitajte ostvarenu potrošnju
            XmlDocument ostvXmlDoc = new XmlDocument();
            ostvXmlDoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\ostv_potrosnja.xml");

            // Filtrirajte podatke prema datumu i regionu
            XmlNodeList progStavke = progXmlDoc.SelectNodes($"//Stavka[Oblast='{region}' and starts-with(Sat, '{date}')]");
            foreach (XmlNode stavka in progStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
                prognoziranaPotrosnjaLista.Add(new PrognoziranaPotrosnja(sat, potrosnja, region));
            }

            string xpathExpression = $"//Stavka[Oblast='{region}' and starts-with(Sat, '{date}')]";
            XmlNodeList ostvStavke = ostvXmlDoc.SelectNodes(xpathExpression);

            foreach (XmlNode stavka in ostvStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
                ostvarenaPotrosnjaLista.Add(new OstvarenaPotrosnja(sat, potrosnja, region));
            }

            // Postavite putanju za CSV fajl
            string putanjaZaCsv = "C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\podaci.csv";

            // Dodajte StreamWriter za upis u CSV fajl
            using (StreamWriter writer = new StreamWriter(putanjaZaCsv))
            {
                writer.WriteLine(PrognoziranaPotrosnja.GetFormattedHeader() + ",Ostvareno,Rel. Odstupanje");

                for (int i = 0; i < prognoziranaPotrosnjaLista.Count; i++)
                {
                    PrognoziranaPotrosnja prognoza = prognoziranaPotrosnjaLista[i];
                    OstvarenaPotrosnja ostvareno = ostvarenaPotrosnjaLista[i];

                    double relativnoOdstupanje = Math.Abs(ostvareno.Potrosnja - prognoza.Potrosnja) / (double)ostvareno.Potrosnja * 100;

                    string line = $"{prognoza},{ostvareno.Potrosnja},{relativnoOdstupanje:F2}";
                    Console.WriteLine(line);

                    // Upisivanje linije u CSV fajl
                    writer.WriteLine(line);
                }
            }
        }



        public void WriteToXML(string path,int sat,int load,string oblast)
        {
            //List<string> Paths = setUpPaths.SettUpPathsWrite();
            string pathWrite = "";
           
                XmlDocument xmldoc = new XmlDocument();
            if (path.Split('_')[0].Equals("ostv"))
            {
                pathWrite = "ostv_potrosnja.xml";
              //  Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
            }
            else if (path.Split('_')[0].Equals("prog")) {
                pathWrite = "prog_potrosnja.xml";
            }
                //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\prog_potrosnja.xml";
            // xmldoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
               xmldoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
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

              //  xmldoc.Save("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
              xmldoc.Save("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);

        }
    }
}
