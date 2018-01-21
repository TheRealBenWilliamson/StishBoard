﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StishBoard
{
    public class Player
    {
        // public or protected publicity?

        private static Player Player1Instance;
        private static Player Player2Instance;
        //dont know where to use these

        //using enums because it is an easy way to show exclusive possible properties of something. eg an enum for the day of the week would be appropriate because there are seven set possibilities yet the day could only be one of them at once.
        //why didnt we use enums for deployemnt?
        public enum PlayerNumber { Player1, Player2};
        public enum PlayerType { Human, Computer};       

        //a function that can be called upon to create a player of a given type. the player is aslo assigned a number to represent them so we can distinguish between to players of the same type.
        public static Player PlayerFactory(PlayerNumber PN, PlayerType PT)
        {
            //creates either a human or computer object and tells it the which player number it has.
            if (PT == PlayerType.Human)
            {
                new Human(PN);
            }
            if (PT == PlayerType.Computer)
            {
                new Computer(PN);
            }

            return new Player();
        }
    }
}
