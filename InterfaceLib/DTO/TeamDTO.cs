using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class TeamDTO
    {
        public int? ID;
        public string Name;
        public int LeeftijdsCategorieID;

        public TeamDTO( string name, int leeftijdsCategorieID, int? id = null)
        {
            ID = id;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
        }
    }
}
