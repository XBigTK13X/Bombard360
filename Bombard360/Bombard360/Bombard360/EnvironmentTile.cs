using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class EnvironmentTile:XnaDrawable
    {
        public EnvironmentTile(int gridColumn, int gridRow, string type)
        {
            Initialize(gridColumn, gridRow, type);
        }
    }
}
