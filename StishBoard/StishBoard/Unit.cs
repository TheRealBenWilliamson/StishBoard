﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StishBoard
{
    public class Unit : Deployment
    {
        //constructor: gives the new object it's variable values to represent that it contains a unit
        public Unit()
        {
            //add health
            depType = "Unit";
            Icon = "U";
            ownedBy = null;
        }

        public Unit(Player player, Square square)
        {
            //add health
            depType = "Unit";
            Icon = "U";
            ownedBy = player;
            square.Dep = this;
            square.Owner = player;

        }
    }
}
