﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject;

namespace ERSProject
{
    class Program
    {
        public static readonly WriteReadXMLImplement wrxml = new WriteReadXMLImplement();
        public static GeografskaPodrucjaUI geoPodUI = new GeografskaPodrucjaUI();
        public static IspisPotrosnje ispis = new IspisPotrosnje();
        
        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("1 - Uvoz podataka");
                Console.WriteLine("2 - Ispis podataka o ostvarenoj potrosnji");
                Console.WriteLine("3 - Evidencija geografskih podrucja");
                Console.WriteLine("X - Izlazak");
                input = Console.ReadLine();


                switch (input)
                {
                    case "1":

                        UvozPodataka();

                        break;
                    case "2":

                        IspisPoDatumu();
                        break;
                    case "3":
                        GeografskaPodrucjaUI();
                        break;
                }

            } while (!input.ToLower().Equals("x"));

            void IspisPoDatumu()
            {
                ispis.Ispisi();
            }
            void UvozPodataka()
            {
                
                    wrxml.ReadFromXML();
                  
            }

            void GeografskaPodrucjaUI()
            {
                geoPodUI.GeoPodUI();
            }
        }
    }
}
