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

        public bool CheckingValidity(string path,string nazivDatoteke)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            string baza = "";
            if (nazivDatoteke.Split('_')[0].Equals("ostv"))
            {
                baza = "ostv_potrosnja.xml";
            }else if (nazivDatoteke.Split('_')[0].Equals("prog"))
            {
                baza = "prog_potrosnja.xml";
            }
            xmlDoc.Load(path + "\\" + baza);
            XmlNodeList fileName = xmlDoc.GetElementsByTagName("Document");
            for(int i = 0; i < fileName.Count; i++)
            {
                
                if (fileName[i].InnerText.Equals(nazivDatoteke))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
