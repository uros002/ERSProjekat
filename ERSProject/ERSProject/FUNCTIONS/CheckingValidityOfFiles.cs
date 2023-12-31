using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERSProject.FUNCTIONS
{
    class CheckingValidityOfFiles
    {
        private readonly WriteReadXMLImplement wrimplement = new WriteReadXMLImplement();
        public void CheckingValidFiles()
        {
            wrimplement.ReadFromXML("ostv_2020_05_07.xml");


        }
    }
}
