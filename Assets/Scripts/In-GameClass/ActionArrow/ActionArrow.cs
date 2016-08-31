using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class ActionArrow
    {
        //You can make your own actionArrow using this class (extends)
        //Module -> ActionIndicator -> ActionArrow(CrossArrow or UpAndDownArrow)
        //ActionIndicator generates thunder based on ActionArrow Information
        //ActionArrow defines the way of generating thunder.
        public abstract Thunder.ThunderDirection[] generateThunder();   
    }
    
}
