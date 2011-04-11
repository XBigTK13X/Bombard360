using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Bombard360.Tiles
{
    class Bomb:GameplayObject
    {
        readonly int MAX_BOMB_LIFE = 100;
        int m_bombLife;
        Player m_owner;
        int m_power;
        int m_range;

        public Bomb(int gridColumn, int gridRow,Player owner,int power,int range)
        {
            Initialize(gridColumn, gridRow, SpriteType.BOMB);
            m_bombLife = MAX_BOMB_LIFE;
            m_owner = owner;
            m_power = power;
            m_range = range;
            m_isBlocking = true;
        }
        public override void Update()
        {
            m_bombLife--;
            if (BoardManager.HasTileType(m_graphic.GetPosition(), SpriteType.EXPLOSION))
            {
                m_bombLife = 0;
            }
            if (m_bombLife <= 0&&m_isActive)
            {
                Explode((int)m_graphic.GetPosition().X,(int)m_graphic.GetPosition().Y);
                m_isActive = false;
            }
        }
        private void Explode(int gridColumn, int gridRow)
        {
            m_owner.FreeBombCacheSlot();
            BoardManager.AddExplosion(gridColumn, gridRow, m_power, 0, 0, 0);
            BoardManager.AddExplosion(gridColumn+1, gridRow,m_power,m_range,1,0);
            BoardManager.AddExplosion(gridColumn, gridRow+1, m_power, m_range, 0,  1);
            BoardManager.AddExplosion(gridColumn - 1, gridRow, m_power, m_range, -1, 0);
            BoardManager.AddExplosion(gridColumn, gridRow-1, m_power, m_range, 0, -1);
        }
    }
}