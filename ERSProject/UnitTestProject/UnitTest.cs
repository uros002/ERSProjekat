using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ERSProject;
using ERSProject.XML_FUNCTIONS;
using ERSProject.Classes;
using ERSProject.FUNCTIONS;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        //private string path = @"C:\Users\User\OneDrive\Dokumenti\GitHub\ERSProjekat\ERSProject\ERSProject\Source";
        private string path = @"C:\Users\Win10\Documents\GitHub\ERSProjekat\ERSProject\ERSProject\Source";
        
        [TestMethod]
        public void DodavanjePostojeceGeografskeOblasti()
        {
            GeografskaPodrucja geoPod = new GeografskaPodrucja("TEST", "TEST");
            WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
            RWGeografskaPodrucja.WriteGeografskaPodrucja(geoPod, path);
            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod, path);
            Assert.AreEqual(exists, 1);
            DeleteFromXML del = new DeleteFromXML();
            del.Delete(path, "TEST");
        }

        [TestMethod]
        public void ProveraNevalidnogFajla()
        {
            CheckingValidityOfFiles checking = new CheckingValidityOfFiles();
            int result = checking.CheckingValidity(path, "ostvareno");
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void DodavanjeNovogGeografskogPodrucja()
        {
            GeografskaPodrucja geoPod = new GeografskaPodrucja("TEST", "TEST");
            WriteGeografskaPodrucjaDB RWGeografskaPodrucja = new WriteGeografskaPodrucjaDB();
            int exists = RWGeografskaPodrucja.ReadGeografskaPodrucja(geoPod, path);
            RWGeografskaPodrucja.WriteGeografskaPodrucja(geoPod, path);
            Assert.AreEqual(exists, 0);
            DeleteFromXML del = new DeleteFromXML();
            del.Delete(path, "TEST");
            
        }
        [TestMethod]
        public void RelativnoOdstupanjeTest()
        {
            RelativnoOdstupanje relativno = new RelativnoOdstupanje();
            double Result= relativno.IzracunajRelativnoOdstupanje(2,3);
            Assert.AreEqual(Result,50);
        }
    }
}
