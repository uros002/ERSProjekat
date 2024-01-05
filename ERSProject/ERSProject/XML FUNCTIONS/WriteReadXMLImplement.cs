using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using ERSProject.Classes;
using ERSProject.FUNCTIONS;

namespace ERSProject
{
    class WriteReadXMLImplement : IReadWriteXML
    {

        private SettingUpPaths setUpPaths = new SettingUpPaths();

        //private SettingUpPaths setUpPathsWrite = new SettingUpPaths();
        private string sourcePath = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
        private List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();
        private List<PrognoziranaPotrosnja> PrognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
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
          
         


        public int ReadFromXML()
        {
            
            List<string>Paths  = setUpPaths.SettUpPathsRead();
            int brojac = 0;
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

            foreach (string path in Paths)
            {
                XmlDocument xmlDoc = new XmlDocument();
                //string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                 xmlDoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + path);

                //xmlDoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + path);
                // xmlDoc.Load(path);

                //CheckAndLogInvalidFiles(path);    

                XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");
                //Console.WriteLine(Directory.GetCurrentDirectory());


                List<IPotrosnja> ostvarenaPotrosnjaLista = new List<IPotrosnja>();
                List<IPotrosnja> PrognoziranaPotrosnjaLista = new List<IPotrosnja>();

                //Console.WriteLine(document.Split('_')[0]);
                if (path.Split('_')[0].Equals("ostv"))
                {



                    //Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        brojac++;
                        OstvarenaPotrosnja nova = new OstvarenaPotrosnja(path, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                        nova.Path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + nova.FileName;

                        //ostvarenaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0] + "_potrosnja.xml");
                        WriteToXML(path, nova);
                        
                    }
                }else if (path.Split('_')[0].Equals("prog"))
                {
                    for (int i = 0; i < sat.Count; i++)
                    {
                        int tmp = Convert.ToInt32(sat[i].InnerText);
                        brojac++;
                        PrognoziranaPotrosnja nova = new PrognoziranaPotrosnja(path, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                        nova.Path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + nova.FileName;
                        WriteToXML(path, nova);
                        //PrognoziranaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0].ToString() + "_potrosnja.xml");
                        //int existsFlag = 0;

                    
                     
                    }
                }
        }
     Console.WriteLine(brojac.ToString());


            return brojac;
        }


        public void WriteToXML(string path,IPotrosnja potrosnja)

        {
            //List<string> Paths = setUpPaths.SettUpPathsWrite();
            string pathWrite = "";
            //StringBuilder pathw;
            //pathw.Append(path.Split('_')[0]);
            //pathw.Append("_potrosnja.xml");
            
           
                XmlDocument xmldoc = new XmlDocument();
            if (path.Split('_')[0].Equals("ostv"))
            {
                pathWrite = "ostv_potrosnja.xml";

            }
            else if (path.Split('_')[0].Equals("prog")) {
                pathWrite = "prog_potrosnja.xml";
            }


                //string FullPath = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
             xmldoc.Load("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
              // xmldoc.Load("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
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

            XmlNode Hour = xmldoc.CreateElement("SatUvoza");
            Hour.InnerText = potrosnja.DateHour.ToString();
            Stavka.AppendChild(Hour);

            XmlNode Minute = xmldoc.CreateElement("MinutUvoza");
            Minute.InnerText = potrosnja.DateMinute.ToString();
            Stavka.AppendChild(Minute);

            XmlNode Second = xmldoc.CreateElement("SekundUvoza");
            Second.InnerText = potrosnja.DateSecond.ToString();
            Stavka.AppendChild(Second);

            XmlNode Path = xmldoc.CreateElement("Putanja");
            Path.InnerText = potrosnja.Path.ToString();
            Stavka.AppendChild(Path);

            

            xmldoc.DocumentElement.AppendChild(Stavka);

                xmldoc.Save("C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
              //xmldoc.Save("C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + pathWrite);
               



        }
    }
}
