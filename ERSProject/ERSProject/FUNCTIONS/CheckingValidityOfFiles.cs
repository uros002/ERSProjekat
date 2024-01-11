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

        public int CheckingValidity(string path,string nazivDatoteke)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                
                xmlDoc.Load(path + "\\" + nazivDatoteke.Split('_')[0] + "_potrosnja.xml");
                XmlNodeList fileName = xmlDoc.GetElementsByTagName("Document");
                for (int i = 0; i < fileName.Count; i++)
                {

                    if (fileName[i].InnerText.Equals(nazivDatoteke))
                    {
                        return 0;
                    }
                }
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
