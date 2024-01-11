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

       
        //public  string PublicPath;
        //public  string PublicNazivFajla;

        //private SettingUpPaths setUpPathsWrite = new SettingUpPaths();
     //   private string sourcePath = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
       // private string sourcePath = "C:\\Users\\Win10\\Documents\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
        private List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();
        private List<PrognoziranaPotrosnja> PrognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
        private WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
        private GeografskaPodrucjaUI geoPodUI = new GeografskaPodrucjaUI();

        private ProveraNevalidnosti proveraNev = new ProveraNevalidnosti();
            //private ProveraBazePodataka provera = new ProveraBazePodataka();

        private CheckingValidityOfFiles check = new CheckingValidityOfFiles();
        //private ProveraBazePodataka provera = new ProveraBazePodataka();




      
            public void ReadFromXML(string path,string vrstaDatoteke)
        {
            
            //List<string>Paths  = setUpPaths.SettUpPathsRead();
            
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

            if (check.CheckingValidity(path, vrstaDatoteke) == false)
            {
                Console.WriteLine("Podaci iz fajla \"" + vrstaDatoteke + "\" su vec uvezeni. Ne mozete dva puta uvesti podatke iz istog fajla.");
            }
            else
            {


                

                XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");
                //Console.WriteLine(Directory.GetCurrentDirectory());


                List<IPotrosnja> ostvarenaPotrosnjaLista = new List<IPotrosnja>();
                List<IPotrosnja> PrognoziranaPotrosnjaLista = new List<IPotrosnja>();

                //Console.WriteLine(document.Split('_')[0]);
                if (vrstaDatoteke.ToLower().Split('_')[0].Equals("ostv"))

                try

                {
                    xmlDoc.Load(path + "\\" + vrstaDatoteke);
                    // xmlDoc.Load(path);
                    Console.WriteLine("Uspesno ste uvezli podatke iz \"" + vrstaDatoteke + "\"\n\n");


                    //CheckAndLogInvalidFiles(path);    

                //    XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                  //  XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                    //XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");
                    //Console.WriteLine(Directory.GetCurrentDirectory());


                    //List<IPotrosnja> ostvarenaPotrosnjaLista = new List<IPotrosnja>();
                    //List<IPotrosnja> PrognoziranaPotrosnjaLista = new List<IPotrosnja>();

                    //Console.WriteLine(document.Split('_')[0]);
                    if (vrstaDatoteke.ToLower().Split('_')[0].Equals("ostv"))
                    {


                        //ostvarenaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0] + "_potrosnja.xml");
                       // WriteToXML(path, vrstaDatoteke, nova);
                        proveraNev.CheckAndLogInvalidFiles(path, vrstaDatoteke);



                        //Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
                        for (int i = 0; i < sat.Count; i++)
                        {
                            int tmp = Convert.ToInt32(sat[i].InnerText);

                            OstvarenaPotrosnja nova = new OstvarenaPotrosnja(path, vrstaDatoteke, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                            //   nova.Path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + nova.FileName;
                            //nova.Path = path + "\\" + nova.FileName;

                            //ostvarenaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0] + "_potrosnja.xml");
                            WriteToXML(path, vrstaDatoteke, nova);
                           proveraNev.CheckAndLogInvalidFiles(path, vrstaDatoteke);
                            }
                    }
                    else if (vrstaDatoteke.ToLower().Split('_')[0].Equals("prog"))
                    {
                        for (int i = 0; i < sat.Count; i++)
                        {
                            int tmp = Convert.ToInt32(sat[i].InnerText);

                            PrognoziranaPotrosnja nova = new PrognoziranaPotrosnja(path, vrstaDatoteke, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                            // nova.Path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\" + nova.FileName;
                            //nova.Path = path + "\\" + nova.FileName;
                            WriteToXML(path, vrstaDatoteke, nova);
                            //PrognoziranaPotrosnjaLista = checking.CheckingValidFiles(path.Split('_')[0].ToString() + "_potrosnja.xml");
                            //int existsFlag = 0;



                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nepostojeci fajl molimo unesite opet!" + ex.Message);
                }
            }
       // }
    // Console.WriteLine(brojac.ToString());


            
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
