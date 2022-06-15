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

        public WedstrijdDTO GetWedstrijdByID(int WedstrijdID);
        public void AddSpelerToeWedstrijd(int SpelerID, int WedstrijdID);

        public void UpdatePresentie(int SpelerID, int WedstrijdID, bool Presentie);

        public bool GetPersentie(int SpelerID, int WedstrijdID);

        public void CreateWedstrijd(WedstrijdDTO dto);

    }
}
