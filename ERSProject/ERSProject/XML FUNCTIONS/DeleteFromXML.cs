using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace ERSProject.XML_FUNCTIONS
{
    public class DeleteFromXML
    {
        public void Delete(string path, string element)
        {
            XmlDocument xmlGeoPod = new XmlDocument();

            xmlGeoPod.Load(path + "\\" + "geografska_podrucja.xml");

            XmlNodeList targetNodes = xmlGeoPod.SelectNodes("//GEOGRAFSKA_PODRUCJA/Podrucje");
            foreach (XmlNode node in targetNodes)
            {
                XmlNode sirina = node.SelectSingleNode("Sirina");
                if (sirina != null)
                {
                    if (sirina.InnerText.Equals(element))
                    {
                        XmlNode parent = node.ParentNode;
                        parent.RemoveChild(node);
                    }
                }
            }


            xmlGeoPod.Save(path + "\\" + "geografska_podrucja.xml");
        }
    }
}
