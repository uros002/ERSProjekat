using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
  public class OstvarenaPotrosnja : IPotrosnja
    {
        
        public int Sat { get; set; }

        public int Potrosnja { get; set; }

        public string Podrucje { get; set; }
        public string Date { get; set; }



        public string Time { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }







        public OstvarenaPotrosnja(string path,string fileName,int sat,int potrosnja,string podrucje)

        {
            this.Sat = sat;
            this.Potrosnja = potrosnja;
            this.Podrucje = podrucje;
            FileName = fileName;
            Path = path +"\\" +  FileName;
            string year = fileName.Split('_')[1];
            string month = fileName.Split('_')[2];
            string day = fileName.Split('_')[3].Split('.')[0];
            Date = year + "/" + month + "/" + day;
            Time = DateTime.Now.Hour.ToString() + ":" +
             DateTime.Now.Minute.ToString() + ":" +
            DateTime.Now.Second.ToString();

        }
    }
}
