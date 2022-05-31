using InterfaceLib.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic.Containers
{
    public class WedstrijdenConainer
    {
        private readonly IWedstrijdContainer container;

        public WedstrijdenConainer(IWedstrijdContainer container)
        {
            this.container = container;

        }
    }
}
