using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    //class for Json (to Save Object to Json or vice versa)
    public class Coord
    {
        
        public int x;
        public int y;
        
        public Coord(int x, int y)
        {
            this.x = x; this.y = y;
        }
        /*
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        */
    }
}
