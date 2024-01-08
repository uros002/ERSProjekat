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
    class IspisiOstvarenePotrosnje
    {

        public void IspisPodatakaOstvarenePotrosnje(string path,string date, string region)
        {
            List<PrognoziranaPotrosnja> prognoziranaPotrosnjaLista = new List<PrognoziranaPotrosnja>();
            List<OstvarenaPotrosnja> ostvarenaPotrosnjaLista = new List<OstvarenaPotrosnja>();

            // Učitajte prognoziranu potrošnju
            XmlDocument progXmlDoc = new XmlDocument();
            progXmlDoc.Load(path+ "\\" + "prog_potrosnja.xml");

            // Učitajte ostvarenu potrošnju
            XmlDocument ostvXmlDoc = new XmlDocument();
            ostvXmlDoc.Load(path+"\\"+ "ostv_potrosnja.xml");

            // Filtrirajte podatke prema datumu i regionu
            XmlNodeList progStavke = progXmlDoc.SelectNodes($"//Stavka[Oblast='{region}' and starts-with(DatumUvoza, '{date}')]");

            foreach (XmlNode stavka in progStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
                prognoziranaPotrosnjaLista.Add(new PrognoziranaPotrosnja(path, "prog_2020_05_07.xml", sat, potrosnja, region));
            }

            string xpathExpression = $"//Stavka[Oblast='{region}' and starts-with(DatumUvoza, '{date}')]";
            XmlNodeList ostvStavke = ostvXmlDoc.SelectNodes(xpathExpression);

            foreach (XmlNode stavka in ostvStavke)
            {
                int sat = int.Parse(stavka.SelectSingleNode("Sat").InnerText);
                int potrosnja = int.Parse(stavka.SelectSingleNode("Load").InnerText);
                ostvarenaPotrosnjaLista.Add(new OstvarenaPotrosnja(path,"ostv_2020_05_07.xml", sat, potrosnja, region));
            }

            // Postavite putanju za CSV fajl
            string putanjaZaCsv = path+"\\"+"podaci.csv";

            // Dodajte StreamWriter za upis u CSV fajl
            using (StreamWriter writer = new StreamWriter(putanjaZaCsv))
            {
                writer.WriteLine("Sat,\tPrognozirana,\tOstvarena,\tRel. Odstupanje");
                Console.WriteLine("Sat,\tPrognozirana,\tOstvarena,\tRel. Odstupanje");

                for (int i = 0; i < prognoziranaPotrosnjaLista.Count; i++)
                {
                    PrognoziranaPotrosnja prognoza = prognoziranaPotrosnjaLista[i];
                    OstvarenaPotrosnja ostvareno = ostvarenaPotrosnjaLista[i];

                    double relativnoOdstupanje = Math.Abs(ostvareno.Potrosnja - prognoza.Potrosnja) / (double)ostvareno.Potrosnja * 100;

                    string line = $"{prognoza.Sat}\t\t{prognoza.Potrosnja}\t\t{ostvareno.Potrosnja}\t\t{relativnoOdstupanje:F2}";
                    Console.WriteLine(line);

                    // Upisivanje linije u CSV fajl
                    writer.WriteLine(line);
                }
            }
        }



    }
}
