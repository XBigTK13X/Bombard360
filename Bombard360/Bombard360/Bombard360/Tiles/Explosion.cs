using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Tiles
{
    class Explosion:GameplayObject
    {
        readonly int MAX_EXPLOSION_LIFE = 100;
        int m_explosionLife;
        int m_power;

        public Explosion(int gridColumn, int gridRow, int power,int range,int spreadXVel,int spreadYVel)
        {
            if (range-- > 2)
            {
                BoardManager.AddExplosion(gridColumn + spreadXVel, gridRow + spreadYVel, power, range, spreadXVel, spreadYVel);
            }
            Initialize(gridColumn, gridRow, SpriteType.EXPLOSION);
            m_power = power;
            m_explosionLife = MAX_EXPLOSION_LIFE;
        }
        public override void Update()
        {
            m_explosionLife--;

            if (m_explosionLife <= 0)
            {
                m_isActive = false;
            }
        }
        public int GetPower()
        {
            return m_power;
        }
    }
}
