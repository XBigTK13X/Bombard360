using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class Wall:GameplayObject
    {
        public Wall(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, SpriteType.WALL);
            m_isBlocking = true;
        }
    }
}
