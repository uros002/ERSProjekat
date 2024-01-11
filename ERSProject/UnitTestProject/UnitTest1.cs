using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ERSProject;
using ERSProject.XML_FUNCTIONS;
using ERSProject.Classes;
using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private IspisiOstvarenePotrosnje ispisOstvarenePotrosnje;
        private string path = @"C:\Users\User\OneDrive\Dokumenti\GitHub\ERSProjekat\ERSProject\ERSProject\Source";
       // private string path = @"C:\Users\User\OneDrive\Dokumenti\GitHub\ERSProjekat\ERSProject\ERSProject\Source";
        [TestMethod]
        public void DodavanjePostojeceGeografskeOblasti()
        {
            GeografskaPodrucja geoPod = new GeografskaPodrucja("VOJ", "VOJ");
            WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod, path);
            Assert.AreEqual(exists, 1);
        }

        [TestMethod]
        public void ProveraBrojaIspisaGeografskihOblasti()
        {
            GeografskaPodrucja geoPod = new GeografskaPodrucja("VOJ", "VOJ");
            WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod, path);
            Assert.AreEqual(exists, 1);
            
        }



        [TestMethod]
        public void CalculateRelativeDeviation()
        {
            RelativnoOdstupanje relativno = new RelativnoOdstupanje();
            double Result = relativno.IzracunajRelativnoOdstupanje(2,3);
            Assert.AreEqual(Result, 50);
        }
    }
}
