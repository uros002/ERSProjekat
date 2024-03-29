﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using ERSProject.Classes;

namespace ERSProject
{
    public class PrikazGeografskihPodrucja
    {

        public void PrikazGeoPod(string path)
        {
            
            XmlDocument xmlGeoPod = new XmlDocument();

            xmlGeoPod.Load(path + "\\" + "geografska_podrucja.xml");

            XmlNodeList sirina = xmlGeoPod.GetElementsByTagName("Sirina"); 
            XmlNodeList naziv = xmlGeoPod.GetElementsByTagName("Naziv");
            if (sirina.Count == 0)
            {
                Console.WriteLine("\nMolimo prvo uvezite neki od fajlova!\n");
            }
            else
            {
                Console.WriteLine("   SIFRA\tNAZIV");
                for (int i = 0; i < sirina.Count; i++)
                {
                    Console.WriteLine(i.ToString() + ". " + sirina[i].InnerText + "\t\t" + naziv[i].InnerText);
                }

            }
            
        }
    }
}
