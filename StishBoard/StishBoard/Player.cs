﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StishBoard
{
    public abstract class Player
    {
        // public or protected publicity?

        private static Player Player1Instance;
        private static Player Player2Instance;
        //dont know where to use these

        //by now a board should already have been created. StishBoard.Instance allows us to get a reference to the existing board.
        StishBoard board = StishBoard.Instance;

        //using enums because it is an easy way to show exclusive possible properties of something. eg an enum for the day of the week would be appropriate because there are seven set possibilities yet the day could only be one of them at once.
        //why didnt we use enums for deployemnt?
        public enum PlayerNumber { Player1, Player2};
        public enum PlayerType { Human, Computer};

        protected PlayerNumber playerNumber;
        protected uint balance;

        protected Base homeBase;

        protected Player(PlayerNumber PN)
        {
            playerNumber = PN;
            balance = 1;

            //homeBase = new Base();
            if (playerNumber == PlayerNumber.Player1)
            {
                new Base(this,board.getSquare(5, 9));
            }
            else
            {
                new Base(this, board.getSquare(5, 1));
            }
        }

        public string GetPlayerNum
        {
            get
            {
                return playerNumber.ToString();
            }
        }


        public ConsoleColor GetRenderColour()
        {
            ConsoleColor retval;

            switch (playerNumber)
            {
                case PlayerNumber.Player1:
                    retval = ConsoleColor.DarkRed;
                    break;
                case PlayerNumber.Player2:
                    retval = ConsoleColor.Blue;
                    break;
                default:
                    retval = ConsoleColor.White;
                    break;
            }

            return retval;
        }

        //a function that can be called upon to create a player of a given type. the player is aslo assigned a number to represent them so we can distinguish between to players of the same type.
        public static Player PlayerFactory(PlayerNumber PN, PlayerType PT)
        {
            Player creation = null;

            //creates either a human or computer object and tells it the which player number it has.
            if (PT == PlayerType.Human)
            {
                creation = new Human(PN);
            }
            if (PT == PlayerType.Computer)
            {
                creation = new Computer(PN);
            }

            return creation;
        }

        public uint Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }        

        public virtual void MakeMove()
        {

        }

        protected void TurnBalance()
        {
            for (uint y = 0; y < 11; y++)
            {
                for (uint x = 0; x < 11; x++)
                {
                    if((board.getSquare(x, y).Dep.DepType == "Barracks" || board.getSquare(x, y).Dep.DepType == "Base") && board.getSquare(x, y).Dep.OwnedBy == this)
                    {
                        balance ++ ;
                    }           
                }
            }
        }


        protected void PlayerCrane(uint FromX , uint FromY, uint ToX, uint ToY)
        {
            //this method will be used by any object derived from the player class. it will allow a player to munipulate deployment positions on the board hence letting them move move a unit or buy/place a deployment.

            //if a deployement is being bought then it has no original position. if the crane is given negative co-ordinates then it will not try to take it from any square on the board.

            Square From = board.getSquare(FromX, FromY);
            Square To = board.getSquare(ToX, ToY);

            From.Owner = this;
            To.Owner = this;
                
            To.Dep = From.Dep;
            From.Dep = new Empty();
                       
        }

    }
}
