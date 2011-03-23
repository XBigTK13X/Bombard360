using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class Wall:XnaDrawable
    {
        public Wall(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, "wall");
            UpdateBoardInformation();
            m_isBlocking = true;
        }
    }
}
