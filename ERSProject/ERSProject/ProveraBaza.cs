using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ERSProject
{
  public  class ProveraBaza
    {
        public int ProveraBazaPrazna(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(path);
                return xmlDoc.DocumentElement?.ChildNodes.Count == 0 ? 0 : 1;
            }
            catch (XmlException)
            {
                // Ukoliko dođe do izuzetka, smatrajte da je baza neispravna ili nepostojeća
                return 0;
            }
        }
    }
}
