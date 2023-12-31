using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
    public interface IReadWriteXML
    {

        int ReadFromXML(string path);

        void WriteToXML(string path,int id,int load, string oblast);
    }
}
