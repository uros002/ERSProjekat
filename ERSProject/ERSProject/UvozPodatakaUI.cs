﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.XML_FUNCTIONS;

namespace ERSProject
{
    public class UvozPodatakaUI
    {
        
        WriteReadXMLImplement wrxml = new WriteReadXMLImplement();
        public void UvozPodataka(string path)
        {
            ;
            Console.WriteLine("Unesite naziv fajla iz kog zelite da uvezete podatke( ime fajla bi trebalo da bude tipa ostv/prog zatim datum merenja podatak u formatu YYYY_MM_DD odvojeno sa donjom crtom)!");
            string nazivFajla = Console.ReadLine();

            wrxml.ReadFromXML(path, nazivFajla);

        }
    }
}
