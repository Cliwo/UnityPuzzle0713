using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class CrossArrow : ActionArrow
    {
        public override Thunder.ThunderDirection[] generateThunder()
        {
            Thunder.ThunderDirection[] arr = { Thunder.ThunderDirection.UP, Thunder.ThunderDirection.DOWN , Thunder.ThunderDirection.RIGHT, Thunder.ThunderDirection.LEFT };
            return arr;
        }
    }
    
}
