using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using ERSProject;

namespace ERSProject
{
    public class ProveraBazePodataka
    {   
        public int ProveraBaza(string document)
        {
            
            
                XmlDocument xmlDoc = new XmlDocument();
                string path = "C:\\Users\\User\\OneDrive\\Dokumenti\\GitHub\\ERSProjekat\\ERSProject\\ERSProject\\Source\\";
                xmlDoc.Load(path + document);
                
                if(xmlDoc.DocumentElement.ChildNodes.Count == 0)
                {
                    return 0;
                } else
                {
                    return 1;
                }

                
                

        }

    }
}
