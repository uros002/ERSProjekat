using ERSProject.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ERSProject
{
    public class IspisiOstvarenePotrosnje
    {
        private ProveraBaza proveraBaze = new ProveraBaza();
        private RelativnoOdstupanje relOdstupanje = new RelativnoOdstupanje();
        public void IspisPodatakaOstvarenePotrosnje(string path,string date, string region)
        {

            // Provera da li je baza prognozirane potrošnje prazna
            if (proveraBaze.ProveraBazaPrazna(Path.Combine(path, "prog_potrosnja.xml")) == 0)
            {
                Console.WriteLine("Nije uvezena baza prognozirane potrošnje. Uvezite bazu pre nego što nastavite.");
                return;
            }

            // Provera da li je baza ostvarene potrošnje prazna
            if (proveraBaze.ProveraBazaPrazna(Path.Combine(path, "ostv_potrosnja.xml")) == 0)
            {
                Console.WriteLine("Nije uvezena baza ostvarene potrošnje. Uvezite bazu pre nego što nastavite.");
                return;
            }


            List<PrognoziranaPotrosnja> prognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
            List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();

            // Učitajte prognoziranu potrošnju
            XmlDocument progXmlDoc = new XmlDocument();
            progXmlDoc.Load(path+ "\\" + "prog_potrosnja.xml");

            // Učitajte ostvarenu potrošnju
            XmlDocument ostvXmlDoc = new XmlDocument();
            ostvXmlDoc.Load(path+"\\"+ "ostv_potrosnja.xml");

            // Filtrirajte podatke prema datumu i regionu
            XmlNodeList progStavke = progXmlDoc.SelectNodes($"//Stavka[Oblast='{region}' and starts-with(DatumMerenja, '{date}')]");

            foreach (XmlNode stavka in progStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
                prognoziranaPotrosnjaLista.Add(new PrognoziranaPotrosnja(path, "prog_2020_05_07.xml", sat, potrosnja, region));
            }

            string xpathExpression = $"//Stavka[Oblast='{region}' and starts-with(DatumMerenja, '{date}')]";
            XmlNodeList ostvStavke = ostvXmlDoc.SelectNodes(xpathExpression);

            foreach (XmlNode stavka in ostvStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
                ostvarenaPotrosnjaLista.Add(new OstvarenaPotrosnja(path,"ostv_2020_05_07.xml", sat, potrosnja, region));
            }
            if (prognoziranaPotrosnjaLista.Count == 0)
            {
                Console.WriteLine("Nema uvezenih podataka za prognoziranu potrosnju za taj datum");
            }else if (ostvarenaPotrosnjaLista.Count == 0)
            {
                Console.WriteLine("Nema uvezenih podataka za ostavrenu potrosnju za taj datum");
            }
            // Postavite putanju za CSV fajl
            string putanjaZaCsv = path+"\\"+"podaci.csv";

            // Dodajte StreamWriter za upis u CSV fajl
            using (StreamWriter writer = new StreamWriter(putanjaZaCsv))
            {
                writer.WriteLine("Sat,\tPrognozirana,\tOstvarena,\tRel. Odstupanje");
                Console.WriteLine("Sat,\t\tPrognozirana,\t\tOstvarena,\t\tRel. Odstupanje");

                for (int i = 0; i < prognoziranaPotrosnjaLista.Count; i++)
                {
                    PrognoziranaPotrosnja prognoza = prognoziranaPotrosnjaLista[i];
                    OstvarenaPotrosnja ostvareno = ostvarenaPotrosnjaLista[i];

                    double relativnoOdstupanje = relOdstupanje.IzracunajRelativnoOdstupanje(ostvareno.Potrosnja,prognoza.Potrosnja);


                    string line = $"{prognoza.Sat}\t\t{prognoza.Potrosnja}\t\t{ostvareno.Potrosnja}\t\t{relativnoOdstupanje:F2}";
                    Console.WriteLine(line);

                    // Upisivanje linije u CSV fajl
                    writer.WriteLine(line);
                }
            }
        }



    }
}
