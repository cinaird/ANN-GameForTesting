using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePong
{
    public class Movable
    {
        public Double XSpeed { get; set; }
        public Double YSpeed { get; set; }

        public Double XPos { get; set; }
        public Double YPos { get; set; }

        public Double NextXPos { get { return XPos + XSpeed; } }
        public Double NextYPos { get { return YPos + YSpeed; } }

        public int XSizeFromCenter { get; set; }
        public int YSizeFromCenter { get; set; }

    }
}
