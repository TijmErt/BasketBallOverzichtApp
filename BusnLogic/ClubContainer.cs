using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic
{
    public class ClubContainer
    {
        private IClubContainer container;
        public ClubContainer(IClubContainer containerTemp)
        {
            this.container = containerTemp;
        }

        public Club FindByID(long id)
        {
            ClubDTO dto = container.FindByID(id);
            return new Club(dto);
        }

        public List<Club> GetAll()
        {
            List<Club> list = new List<Club>();
            foreach (ClubDTO item in container.GetAll())
            {
                list.Add(new Club(item));
            }
            return list;
        }
    }
}
