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
    }
}
