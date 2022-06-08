using InterfaceLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib.InterFaces
{
    public interface IWedstrijdContainer
    {

        public List<WedstrijdDTO> GetAllFromTeam(int TeamID);

        public void CreateWedstrijd(WedstrijdDTO dto);

    }
}
