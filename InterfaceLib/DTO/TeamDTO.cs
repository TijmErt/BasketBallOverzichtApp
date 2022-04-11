using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class TeamDTO
    {
        public long ID;
        public string Name;

        public TeamDTO(long id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
