using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Bombard360
{
    class Bomb:XnaDrawable
    {
        readonly int MAX_BOMB_LIFE = 100;
        int m_bombLife;
        public Bomb(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, "bomb");
            m_bombLife = MAX_BOMB_LIFE;
        }
        public override void Update()
        {
            m_bombLife--;
            
            if (m_bombLife <= 0)
            {
                m_isActive = false;
            }
        }
    }
}