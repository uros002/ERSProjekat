using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject.Classes
{
    public interface IPotrosnja
    {
        int Sat { get; set; }

        int Potrosnja { get; set; }

        string Podrucje { get; set; }

        DateTime Date { get; set; }

        int DateHour { get; set; }

        int DateMinute { get; set; }

        int DateSecond { get; set; }

        string Path { get; set; }

        string FileName { get; set; }
    }
}
