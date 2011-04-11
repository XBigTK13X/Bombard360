using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Management;
using Microsoft.Xna.Framework.Content;

namespace Bombard360
{
    class MenuButton:GameplayObject
    {
        State m_state;
        public MenuButton(State state,SpriteType type,int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, type);
            m_state = state;
        }
        public void Activate()
        {
            StateManager.LoadState(m_state);
        }
    }
}
