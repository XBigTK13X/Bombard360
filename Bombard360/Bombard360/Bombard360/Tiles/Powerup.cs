using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Tiles
{
    class Powerup:GameplayObject
    {

        public Powerup(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, SpriteType.POWERUP);
        }
        public override void Update()
        {
            base.Update();

        }
    }
}
