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

        int ReadFromXML();

        void WriteToXML(string path,IPotrosnja potrosnja);
    }
}
