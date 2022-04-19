using InterfaceLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib.InterFaces
{
    public interface IGebruikerContainer
    {
        public bool CheckGebruiker(string Email, string wachtwoord);
        public GebruikerDTO GetGebruiker(string Email);

        public void CreateGebruikerAccount();

        public List<GebruikerDTO> GetGebruikerFromTeam(long TeamID);
    }
}
