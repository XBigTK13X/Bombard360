using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class GameplayState:State
    {
        public GameplayState()
        {
            for (int ii = 0; ii < 10; ii++)
            {
                for (int jj = 0; jj < 10; jj++)
                {
                    m_windowComponents.Add(new EnvironmentTile(ii,jj,"dirt_floor"));
                }
            }
        }
    }
}
