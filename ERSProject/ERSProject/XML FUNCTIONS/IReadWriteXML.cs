using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.Classes;

namespace ERSProject
{
    public interface IReadWriteXML
    {

        void ReadFromXML(string path, string vrstaDatoteke);

        void WriteToXML(string path, string vrstaDatoteke,IPotrosnja potrosnja);
    }
}
