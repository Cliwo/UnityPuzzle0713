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
        public Vector2 startPoint;
        public Vector2[] destinations;
        public Vector2[] modulePositions;
        public ModuleKind[] moduleKind;

        
    }
}
