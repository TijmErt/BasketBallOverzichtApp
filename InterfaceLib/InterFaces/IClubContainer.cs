﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public interface IClubContainer
    {
        public ClubDTO FindByID(long id);
        public List<ClubDTO> GetAll();
    }
}
