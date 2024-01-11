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

       
       
        private List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();
        private List<PrognoziranaPotrosnja> PrognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
        private WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
        private GeografskaPodrucjaUI geoPodUI = new GeografskaPodrucjaUI();

        private ProveraNevalidnosti proveraNev = new ProveraNevalidnosti();
            

        private CheckingValidityOfFiles check = new CheckingValidityOfFiles();
       




      
            public void ReadFromXML(string path,string vrstaDatoteke)
        {
            
            
                XmlDocument xmlDoc = new XmlDocument();
            
            int iResult = check.CheckingValidity(path, vrstaDatoteke);
            if (iResult == 0)
            {
                Console.WriteLine("Podaci iz fajla \"" + vrstaDatoteke + "\" su vec uvezeni. Ne mozete dva puta uvesti podatke iz istog fajla.");
            }else if(iResult == -1)
            {
                Console.WriteLine("Molimo unesite ispravno postojecu datoteku.");
            }
            else
            {


                

                XmlNodeList sat = xmlDoc.GetElementsByTagName("SAT");
                XmlNodeList potrosnja = xmlDoc.GetElementsByTagName("LOAD");
                XmlNodeList oblast = xmlDoc.GetElementsByTagName("OBLAST");
               


                List<IPotrosnja> ostvarenaPotrosnjaLista = new List<IPotrosnja>();
                List<IPotrosnja> PrognoziranaPotrosnjaLista = new List<IPotrosnja>();

                

                try

                {
                    xmlDoc.Load(path + "\\" + vrstaDatoteke);
                    
                    Console.WriteLine("Uspesno ste uvezli podatke iz \"" + vrstaDatoteke + "\"\n\n");


                       

                

                   
                    if (vrstaDatoteke.ToLower().Split('_')[0].Equals("ostv"))
                    {


                       
                        proveraNev.CheckAndLogInvalidFiles(path, vrstaDatoteke);



                        //Console.WriteLine("AAAAAAAAAAAAAAAAAAAA");
                        for (int i = 0; i < sat.Count; i++)
                        {
                            int tmp = Convert.ToInt32(sat[i].InnerText);

                            OstvarenaPotrosnja nova = new OstvarenaPotrosnja(path, vrstaDatoteke, Convert.ToInt32(sat[i].InnerText), Convert.ToInt32(potrosnja[i].InnerText), oblast[i].InnerText.ToString());
                           
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
                           
                            WriteToXML(path, vrstaDatoteke, nova);

                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nepostojeci fajl molimo unesite opet!" + ex.Message);
                }
            }

            
        }


        public void WriteToXML(string path,string nazivDatoteke,IPotrosnja potrosnja)

        {
           
            string pathWrite = "";
            
           
                XmlDocument xmldoc = new XmlDocument();
            string typeOfBase = nazivDatoteke.Split('_')[0];
            if (typeOfBase.Equals("ostv"))
            {
                pathWrite = "ostv_potrosnja.xml";

            }
            else if (typeOfBase.Equals("prog")) {
                pathWrite = "prog_potrosnja.xml";
            }

            
               
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
