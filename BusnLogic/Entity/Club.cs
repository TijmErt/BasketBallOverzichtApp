using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic
{
    public class Club
    {
        public long ID;
        public string Name;

        public Club(long iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public Club(ClubDTO TempDTO)
        {
            this.ID = TempDTO.ID;
            this.Name = TempDTO.Name;
        }
        public override string? ToString()
        {
            return $"Club met naam: {Name} en ID: {ID} \n ";
        }
    }
}
