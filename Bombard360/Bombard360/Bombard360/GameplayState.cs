using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class GameplayState:State
    {
        private readonly Dictionary<int, int> m_wallPositions = new Dictionary<int, int>()
        {
            {1,1},
            {2,2}
        };
        public GameplayState()
        {
            for (int ii = 0; ii < SpriteSheetManager.Rows; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Columns; jj++)
                {
                    m_windowComponents.Add(new EnvironmentTile(ii,jj,"dirt_floor"));
                }
            }
            foreach (KeyValuePair<int, int> pair in m_wallPositions)
            {
                m_windowComponents.Add(new Wall(pair.Key, pair.Value));
            }
            m_windowComponents.Add(new Player(0, 0, 0, true));
        }
        public void Update()
        {
            foreach (XnaDrawable component in m_windowComponents)
            {
                component.Update();
            }
            BoardManager.CollectGarbage();
        }
    }
}
