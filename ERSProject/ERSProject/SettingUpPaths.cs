using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSProject
{
    class SettingUpPaths
    {

        public List<string> SettUpPathsRead() {


            List<string> Paths = new List<string>();
            Paths.Add("ostv_2020_05_07.xml");
            Paths.Add("prog_2020_05_07.xml");

            return Paths;
        }

        public List<string> SettUpPathsWrite()
        {


            List<string> Paths = new List<string>();
            Paths.Add("ostv_potrosnja.xml");
            Paths.Add("prog_potrosnja.xml");

            return Paths;
        }

    }
}
