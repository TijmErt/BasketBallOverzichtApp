using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class ClubDTO
    {
        public int ID;
        public string Name;

        public ClubDTO(int iD, string name)
        {
            ID = iD;
            Name = name;
        }
    }
}
