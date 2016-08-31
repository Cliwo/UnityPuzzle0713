using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ModuleKind { UpAndDownArrow, CrossArrow };
    public class LevelInformation
    {
        public Coord startPoint;     
        public Coord[] destinations;        
        public Module.Color[] destinationColors;        
        //module Positions and moduleKind are not currently used.
        //because there is no fixed Module in every level.
        //If you have to make fixed Module, You can use these.
        public Coord[] modulePositions;       
        public ModuleKind[] moduleKind;
        public bool isLastName;
        /*
        public Coord StartPoint
        {
            get
            {
                return startPoint;
            }
            set
            {
                startPoint = value;
            }
        }
        public Coord[] Destinations
        {
            get
            {
                return destinations;
            }
            set
            {
                destinations = value;
            }
        }
        public Module.Color[] DestinationColors
        {
            get
            {
                return destinationColors;
            }
            set
            {
                destinationColors = value;
            }
        }
        //destinations and destinationColors should have same Length;
        public Coord[] ModulePositions
        {
            get
            {
                return modulePositions;
            }
            set
            {
                modulePositions = value;
            }
        }
        public ModuleKind[] ModuleKind
        {
            get
            {
                return moduleKind;
            }
            set
            {
                moduleKind = value;
            }
        }
        */
    }
}
