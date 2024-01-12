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
        private string path = @"C:\Users\User\OneDrive\Dokumenti\GitHub\ERSProjekat\ERSProject\ERSProject\Source";
        //private string path = @"C:\Users\Win10\Documents\GitHub\ERSProjekat\ERSProject\ERSProject\Source";

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
            double Result = relativno.IzracunajRelativnoOdstupanje(2, 3);
            Assert.AreEqual(Result, 50);
        }

        [TestMethod]
        public void TestiranjeKlaseGeografskaPodrucja()
        {
            GeografskaPodrucja geoPod = new GeografskaPodrucja("TEST", "TEST123");
            Assert.AreEqual(geoPod.NazivPodrucja, "TEST");
            Assert.AreEqual(geoPod.SirinaPodrucja, "TEST123");

        }

        [TestMethod]
        public void TestiranjeKlaseNeispravniPodaci()
        {
            NeispravniPodaci podaci = new NeispravniPodaci(path,"imeFajla");
            Assert.AreEqual(podaci.imeFajla, "imeFajla");
            Assert.AreEqual(podaci.lokacija, path + "\\" + "imeFajla");
            Assert.AreEqual(podaci.sat, DateTime.Now.Hour);
            Assert.AreEqual(podaci.minuti, DateTime.Now.Minute);
            Assert.AreEqual(podaci.sekunde, DateTime.Now.Second);


        }

        [TestMethod]
        public void TestiranjeKlaseOstvarenaPotrosnja()
        {
            IPotrosnja potrosnja = new OstvarenaPotrosnja(path, "imeFajla_2020_05_07", 12,2000,"VOJ");
            Assert.AreEqual(potrosnja.FileName, "imeFajla_2020_05_07");
            Assert.AreEqual(potrosnja.Path, path + "\\" + "imeFajla_2020_05_07");
            Assert.AreEqual(potrosnja.Time, DateTime.Now.Hour.ToString() + ":" +
             DateTime.Now.Minute.ToString() + ":" +
            DateTime.Now.Second.ToString());
            Assert.AreEqual(potrosnja.Sat,12);
            Assert.AreEqual(potrosnja.Potrosnja,2000);
            Assert.AreEqual(potrosnja.Podrucje, "VOJ");
            string fileName = "imeFajla_2020_05_07";
            string year = fileName.Split('_')[1];
            string month = fileName.Split('_')[2];
            string day = fileName.Split('_')[3].Split('.')[0];
            Assert.AreEqual(potrosnja.Date, year + "/" + month + "/" + day);

        }

        [TestMethod]
        public void TestiranjeKlasePrognoziranaPotrosnja()
        {
            IPotrosnja potrosnja = new PrognoziranaPotrosnja(path, "imeFajla_2020_05_07", 12, 2000, "VOJ");
            Assert.AreEqual(potrosnja.FileName, "imeFajla_2020_05_07");
            Assert.AreEqual(potrosnja.Path, path + "\\" + "imeFajla_2020_05_07");
            Assert.AreEqual(potrosnja.Time, DateTime.Now.Hour.ToString() + ":" +
             DateTime.Now.Minute.ToString() + ":" +
            DateTime.Now.Second.ToString());
            Assert.AreEqual(potrosnja.Sat, 12);
            Assert.AreEqual(potrosnja.Potrosnja, 2000);
            Assert.AreEqual(potrosnja.Podrucje, "VOJ");
            string fileName = "imeFajla_2020_05_07";
            string year = fileName.Split('_')[1];
            string month = fileName.Split('_')[2];
            string day = fileName.Split('_')[3].Split('.')[0];
            Assert.AreEqual(potrosnja.Date, year + "/" + month + "/" + day);

        }
    } 
}
