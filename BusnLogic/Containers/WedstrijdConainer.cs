using BusnLogic.Entity;
using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic.Containers
{
    public class WedstrijdConainer
    {
        private readonly IWedstrijdContainer container;

        public WedstrijdConainer(IWedstrijdContainer container)
        {
            this.container = container;

        }

        public List<Wedstrijd> GetAllFromTeam(int TeamID)
        {
            List<WedstrijdDTO> dto = container.GetAllFromTeam(TeamID);
            List<Wedstrijd> wedstrijden = new List<Wedstrijd>();
            foreach (WedstrijdDTO temp in dto)
            {
                wedstrijden.Add(new Wedstrijd(temp));
            }
            return wedstrijden;
        }


        public Wedstrijd GetWedstrijdByID(int WedstrijdID)
        {
            Wedstrijd wedstrijd = new(container.GetWedstrijdByID(WedstrijdID));
            return wedstrijd;
        }
        public void AddSpelerToeWedstrijd(int SpelerID, int WedstrijdID)
        {
            container.AddSpelerToeWedstrijd(SpelerID, WedstrijdID);
        }

        public void UpdatePresentie(int SpelerID, int WedstrijdID, bool Presentie)
        {
            container.UpdatePresentie(SpelerID, WedstrijdID, Presentie);
        }

        public bool GetPresentie(int SpelerID, int WedstrijdID)
        {
            return container.GetPersentie(SpelerID, WedstrijdID);
        }

        public int CreateWedstrijd(Wedstrijd temp)
        {
            return container.CreateWedstrijd(temp.GetDTO());
        }
    }
}
