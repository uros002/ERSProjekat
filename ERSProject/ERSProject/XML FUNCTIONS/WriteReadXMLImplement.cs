using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using ERSProject.Classes;
using ERSProject.FUNCTIONS;
using ERSProject.XML_FUNCTIONS;

namespace ERSProject
{
    class WriteReadXMLImplement : IReadWriteXML
    {

        private SettingUpPaths setUpPaths = new SettingUpPaths();
        //public  string PublicPath;
        //public  string PublicNazivFajla;

        //private SettingUpPaths setUpPathsWrite = new SettingUpPaths();
     //   private string sourcePath = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
        private string sourcePath = "C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
        private List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();
        private List<PrognoziranaPotrosnja> PrognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
        private WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
        private GeografskaPodrucjaUI geoPodUI = new GeografskaPodrucjaUI();
        //private ProveraBazePodataka provera = new ProveraBazePodataka();




        private void LogInvalidFile(string document, XmlDocument xmlDoc)
        {
            NeispravniPodaci neispravniPodaci = new NeispravniPodaci(document);

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
            XmlNodeList progStavke = progXmlDoc.SelectNodes($"//Stavka[Oblast='{region}' and starts-with(DatumUvoza, '{date}')]");

            foreach (XmlNode stavka in progStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
          //      prognoziranaPotrosnjaLista.Add(new PrognoziranaPotrosnja("prog_potrosnja.xml",sat, potrosnja, region));
            }

            string xpathExpression = $"//Stavka[Oblast='{region}' and starts-with(DatumUvoza, '{date}')]";
            XmlNodeList ostvStavke = ostvXmlDoc.SelectNodes(xpathExpression);

            foreach (XmlNode stavka in ostvStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
          //      ostvarenaPotrosnjaLista.Add(new OstvarenaPotrosnja("ostv_potrosnja.xml", sat, potrosnja, region));
            }

            // Postavite putanju za CSV fajl
            string putanjaZaCsv = "C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\podaci.csv";

            // Dodajte StreamWriter za upis u CSV fajl
            using (StreamWriter writer = new StreamWriter(putanjaZaCsv))
            {
                writer.WriteLine( "Sat,\tPrognozirana,\tOstvarena,\tRel. Odstupanje");
                Console.WriteLine( "Sat,\tPrognozirana,\tOstvarena,\tRel. Odstupanje");
                
                for (int i = 0; i < prognoziranaPotrosnjaLista.Count; i++)
                {
                    PrognoziranaPotrosnja prognoza = prognoziranaPotrosnjaLista[i];
                    OstvarenaPotrosnja ostvareno = ostvarenaPotrosnjaLista[i];

                    double relativnoOdstupanje = Math.Abs(ostvareno.Potrosnja - prognoza.Potrosnja) / (double)ostvareno.Potrosnja * 100;

                    string line = $"{prognoza.Sat},\t{prognoza.Potrosnja},\t{ostvareno.Potrosnja},\t{relativnoOdstupanje:F2}";
                    Console.WriteLine(line);

                    // Upisivanje linije u CSV fajl
                    writer.WriteLine(line);
                }
            }
        }



            public int ReadFromXML(string path,string vrstaDatoteke)
        {
            
            //List<string>Paths  = setUpPaths.SettUpPathsRead();
            int brojac = 0;
            //PublicPath = path;
            //PublicNazivFajla = vrstaDatoteke;
            //
            //geoPodUI.path = PublicPath;
           // int flagBaza = 0;
           // if (provera.ProveraBaza("ostv_potrosnja.xml") == 1 || provera.ProveraBaza("prog_potrosnja.xml") == 1)
            //{
             //   flagBaza++;
            //}
            
            //Console.WriteLine(flagBaza.ToString());
           // if (flagBaza != 0)
            //{
              //  Console.WriteLine("Vec su uvezeni podaci u baze");
            //}
            //else

            //foreach (string Path in Paths)
            //{
                XmlDocument xmlDoc = new XmlDocument();
            //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
            // xmlDoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + path);
            try
            {
                xmlDoc.Load(path + "\\" + vrstaDatoteke);
                // xmlDoc.Load(path);
                Console.WriteLine("Uspesno ste uvezli podatke iz \"" + vrstaDatoteke + "\"\n\n");
                

                //CheckAndLogInvalidFiles(path);    

                XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");
                //Console.WriteLine(Directory.GetCurrentDirectory());


                List<IPotrosnja> ostvarenaPotrosnjaLista = new List<IPotrosnja>();
                List<IPotrosnja> PrognoziranaPotrosnjaLista = new List<IPotrosnja>();

                //Console.WriteLine(document.Split('_')[0]);
                if (vrstaDatoteke.ToLower().Split('_')[0].Equals("ostv"))
                {



                    //Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        brojac++;
                        OstvarenaPotrosnja nova = new OstvarenaPotrosnja(path, vrstaDatoteke, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                        //   nova.Path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + nova.FileName;
                        //nova.Path = path + "\\" + nova.FileName;

                        //ostvarenaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0] + "_potrosnja.xml");
                        WriteToXML(path, vrstaDatoteke, nova);

                    }
                }
                else if (vrstaDatoteke.ToLower().Split('_')[0].Equals("prog"))
                {
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        brojac++;
                        PrognoziranaPotrosnja nova = new PrognoziranaPotrosnja(path, vrstaDatoteke, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                        // nova.Path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + nova.FileName;
                        //nova.Path = path + "\\" + nova.FileName;
                        WriteToXML(path, vrstaDatoteke, nova);
                        //PrognoziranaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0].ToString() + "_potrosnja.xml");
                        //int existsFlag = 0;



                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Nepostojeci fajl molimo unesite opet!" + ex.Message);
            }
       // }
    // Console.WriteLine(brojac.ToString());


            return brojac;
        }


        public void WriteToXML(string path,string nazivDatoteke,IPotrosnja potrosnja)

        {
            //List<string> Paths = setUpPaths.SettUpPathsWrite();
            string pathWrite = "";
            //StringBuilder pathw;
            //pathw.Append(path.Split('_')[0]);
            //pathw.Append("_potrosnja.xml");
            
           
                XmlDocument xmldoc = new XmlDocument();
            string typeOfBase = nazivDatoteke.Split('_')[0];
            if (typeOfBase.Equals("ostv"))
            {
                pathWrite = "ostv_potrosnja.xml";

            }
            else if (typeOfBase.Equals("prog")) {
                pathWrite = "prog_potrosnja.xml";
            }

            
                //string FullPath = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                //xmldoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
                 xmldoc.Load(path + "\\" + pathWrite);
                XmlNode Stavka = xmldoc.CreateElement("Stavka");


                XmlNode Sat = xmldoc.CreateElement("Sat");
                Sat.InnerText = potrosnja.Sat.ToString();
                Stavka.AppendChild(Sat);

                XmlNode Load = xmldoc.CreateElement("Load");
                Load.InnerText = potrosnja.Potrosnja.ToString();
                Stavka.AppendChild(Load);

                XmlNode Oblast = xmldoc.CreateElement("Oblast");
                Oblast.InnerText = potrosnja.Podrucje.ToString();
                Stavka.AppendChild(Oblast);

                XmlNode FileName = xmldoc.CreateElement("Document");
                FileName.InnerText = potrosnja.FileName.ToString();
                Stavka.AppendChild(FileName);


                XmlNode Date = xmldoc.CreateElement("DatumUvoza");
                Date.InnerText = potrosnja.Date.ToString();
                Stavka.AppendChild(Date);

                XmlNode Time = xmldoc.CreateElement("VremeUvoza");
                Time.InnerText = potrosnja.Time.ToString();
                Stavka.AppendChild(Time);

                
                XmlNode Path = xmldoc.CreateElement("Putanja");
                Path.InnerText = potrosnja.Path.ToString();
                Stavka.AppendChild(Path);



                xmldoc.DocumentElement.AppendChild(Stavka);

              //  xmldoc.Save("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);

            xmldoc.Save(path + "\\" + pathWrite);

            GeografskaPodrucja geoPod = new GeografskaPodrucja(potrosnja.Podrucje, potrosnja.Podrucje);
            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod,path);
            if(exists == 0)
            {
                RWGeografskaPodrucja.WriteGeografskaPodrucja(geoPod,path);
            }


        }
    }
}
