using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
    public interface IReadWriteXML
    {

        void ReadFromXML(string path);

        void WriteToXML();
    }
}
