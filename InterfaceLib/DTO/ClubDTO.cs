using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class ClubDTO
    {
        public long ID;
        public string Name;

        public ClubDTO(long iD, string name)
        {
            ID = iD;
            Name = name;
        }
    }
}
