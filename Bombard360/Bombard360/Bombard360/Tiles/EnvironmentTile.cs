using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class EnvironmentTile:GameplayObject
    {
        public EnvironmentTile(int gridColumn, int gridRow, SpriteType type)
        {
            Initialize(gridColumn, gridRow, type);
        }
    }
}
