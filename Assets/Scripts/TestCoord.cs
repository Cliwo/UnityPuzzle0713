using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class TestCoord
    {
        int xPos;
        int yPos;

        [JsonConstructor]
        public TestCoord()
        {
            XPos = 2;
            YPos = 3;
        }

        public int XPos
        {
            get
            {
                return xPos;
            }

            set
            {
                xPos = value;
            }
        }

        public int YPos
        {
            get
            {
                return yPos;
            }

            set
            {
                yPos = value;
            }
        }
    }
}
