using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class Bomb:XnaDrawable
    {
        public Bomb(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, "bomb");
        }
    }
}
