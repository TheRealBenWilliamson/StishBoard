﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StishBoard
{
    class MiniMaxMind
    {

        private static MiniMaxMind instance;

        public static MiniMaxMind Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MiniMaxMind();
                }
                return instance;
            }
        }

        public int TraverseTree(StishMiniMaxNode CurrentNode, int DepthCount, int colour)
        {
            //Depth count is given as 0 when called at root

            if (CurrentNode == null || DepthCount == 0)
            {
                //the number 3 is a variable to be changed as sight increases
                return (colour * CurrentNode.FindValue(CurrentNode, CurrentNode.NodeBoardState, CurrentNode.Allegiance));
            }

            int Value = int.MinValue;

            for (int index = 0; index < CurrentNode.CountChildren(); index++)
            {
                Value = Math.Max(Value, -TraverseTree((StishMiniMaxNode)CurrentNode.GetChild(index), DepthCount - 1, -colour));
                CurrentNode.NegaMaxValue = Value;
            }
            return Value;
        }

        public void RecBuildMMTree(StishMiniMaxNode CurrentNode, int DepthCount)
        {
            //Depth count is given as 0 when called at root
            //the number quoted below  will not occur in the depth sequence

            if(CurrentNode == null || DepthCount == 0)
            {
                return;
            }

            ForeSight.Instance.GenerateChildren(CurrentNode);

            for (int index = 0; index < CurrentNode.CountChildren(); index++)
            {              
                RecBuildMMTree((StishMiniMaxNode)CurrentNode.GetChild(index), DepthCount - 1);
            }
        }

        public int BuildABTree(StishMiniMaxNode CurrentNode, int DepthCount, int Alpha, int Beta, int colour)
        {
            if (CurrentNode == null || DepthCount == 0)
            {
                //returns if this is the root node or is a leaf node
                return (colour * CurrentNode.FindValue(CurrentNode, CurrentNode.NodeBoardState, CurrentNode.Allegiance));
            }

            int Value = int.MinValue;
            ForeSight.Instance.GenerateChildren(CurrentNode);
            for (int index = 0; index < CurrentNode.CountChildren(); index++)
            {
                Value = Math.Max(Value, -1 * BuildABTree((StishMiniMaxNode)CurrentNode.GetChild(index), DepthCount - 1, -Beta, -Alpha, -colour));
                CurrentNode.NegaMaxValue = Value;
                Alpha = Math.Max(Alpha, Value);

                if(Alpha >= Beta)
                {
                    //this return statement "prunes" the tree and prevents further growth on the tree in those bad areas
                    return (colour * CurrentNode.FindValue(CurrentNode, CurrentNode.NodeBoardState, CurrentNode.Allegiance));
                }
            }

            //if the node does not need to be pruned
            return (colour * CurrentNode.FindValue(CurrentNode, CurrentNode.NodeBoardState, CurrentNode.Allegiance));
        }

        public void BuildMMTree(StishMiniMaxNode RootNode, int DepthLimit)
        {
            StishMiniMaxNode InvestNode;
            List<StishMiniMaxNode> GenQueue = new List<StishMiniMaxNode>();
            GenQueue.Add(RootNode);

            while(GenQueue.Count > 0)
            {
                InvestNode = GenQueue[0];
                ForeSight.Instance.GenerateChildren(InvestNode);
                //creates the depth layer but doesnt generate from them
                if (InvestNode.FindDepth() + 1 < DepthLimit)
                {                  
                    for (int index = 0; index < InvestNode.CountChildren(); index++)
                    {
                        GenQueue.Add((StishMiniMaxNode)InvestNode.GetChild(index));
                    }
                }

                GenQueue.Remove(InvestNode);
            }
        }
    }
}
