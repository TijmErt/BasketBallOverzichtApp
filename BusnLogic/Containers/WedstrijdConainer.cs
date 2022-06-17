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
        /// <summary>
        /// Haalt alle wedstrijden op die het meegeven team heeft
        /// </summary>
        /// <param name="TeamID">Het id van de team waarvan je de wedstrijden wil ophalen</param>
        /// <returns>Geeft een lijst van wedstrijden terug</returns>
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

        /// <summary>
        /// Hier haal je wedstrijd informatie mee op 
        /// </summary>
        /// <param name="WedstrijdID">Het Id van de wedstrijd waar je de informatie van wilt ophalen </param>
        /// <returns>Geeft een Wedstrijd terug </returns>
        public Wedstrijd GetWedstrijdByID(int WedstrijdID)
        {
            Wedstrijd wedstrijd = new(container.GetWedstrijdByID(WedstrijdID));
            return wedstrijd;
        }

        /// <summary>
        /// Hiermee word er een speler gekoppeld aan de wedstrijd
        /// </summary>
        /// <param name="SpelerID">Het ID van de speler die word gekoppeld</param>
        /// <param name="WedstrijdID">Het ID van de Wedstrijd waar aan word gekoppeld </param>
        public void AddSpelerToeWedstrijd(int SpelerID, int WedstrijdID)
        {
            container.AddSpelerToeWedstrijd(SpelerID, WedstrijdID);
        }

        /// <summary>
        /// Hiermee word de presentie van een speler bij een wedstrijd aangepast
        /// </summary>
        /// <param name="SpelerID">Het ID van de speler waarvan de presentie word aangepast</param>
        /// <param name="WedstrijdID">Het ID van de wedstrijd waarvoor de presentie van de speler word aangepast</param>
        /// <param name="Presentie">de Presentie waarde die de oude presentie waarde vervangt.</param>
        public void UpdatePresentie(int SpelerID, int WedstrijdID, bool Presentie)
        {
            container.UpdatePresentie(SpelerID, WedstrijdID, Presentie);
        }

        /// <summary>
        /// Hiermee word de Presentie opgehaald van een speler bij een wedstrijd
        /// </summary>
        /// <param name="SpelerID">De speler ID van wie de presentie word opgehaald</param>
        /// <param name="WedstrijdID">De WedstrijdID van de wedstrijd waar de presentie word opghaald</param>
        /// <returns>geeft de waarde van de presentie terug als bool</returns>
        public bool GetPresentie(int SpelerID, int WedstrijdID)
        {
            return container.GetPresentie(SpelerID, WedstrijdID);
        }

        /// <summary>
        /// Je Creeërt hiermee een wedstrijd
        /// </summary>
        /// <param name="dto">Hier geef jee wedstrijdDTO in met de gegevens van een wedstrijd (Behalve de ID)</param>
        /// <returns>Geeft de ID terug als int van de gemaakte wedstrijd</returns>
        public int CreateWedstrijd(Wedstrijd temp)
        {
            return container.CreateWedstrijd(temp.GetDTO());
        }
    }
}
