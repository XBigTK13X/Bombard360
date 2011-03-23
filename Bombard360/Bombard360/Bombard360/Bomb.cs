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
        Player m_owner;
        int m_power;
        int m_range;

        public Bomb(int gridColumn, int gridRow,Player owner,int power,int range)
        {
            Initialize(gridColumn, gridRow, "bomb");
            m_bombLife = MAX_BOMB_LIFE;
            m_owner = owner;
            m_power = power;
            m_range = range;
            m_isBlocking = true;
        }
        public override void Update()
        {
            UpdateBoardInformation();
            m_bombLife--;
            if (BoardManager.HasTileType(m_position, "explosion"))
            {
                m_bombLife = 0;
            }
            if (m_bombLife <= 0&&m_isActive)
            {
                m_owner.ExplodeBomb((int)m_position.X,(int)m_position.Y, m_range,m_power);
                m_isActive = false;
            }
        }
    }
}