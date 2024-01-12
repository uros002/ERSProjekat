using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERSProject.Classes;

namespace ERSProject.XML_FUNCTIONS
{
    public interface IReadWriteGeografskaPodrucja
    {
        void WriteGeografskaPodrucja(GeografskaPodrucja geoPod, string path);

        int ReadGeografskaPodrucja(GeografskaPodrucja geoPod, string path);
    }
}
