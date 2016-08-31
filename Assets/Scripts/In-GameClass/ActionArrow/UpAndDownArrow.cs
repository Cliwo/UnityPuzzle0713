using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class UpAndDownArrow : ActionArrow
    {
        public override Thunder.ThunderDirection[] generateThunder()
        {
            Thunder.ThunderDirection[] arr = { Thunder.ThunderDirection.UP, Thunder.ThunderDirection.DOWN };
            return arr;
        }
    }
}
