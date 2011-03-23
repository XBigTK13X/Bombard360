using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class Explosion:XnaDrawable
    {
        readonly int MAX_EXPLOSION_LIFE = 100;
        int m_explosionLife;
        int m_power;

        public Explosion(int gridColumn, int gridRow, int power)
        {
            Initialize(gridColumn, gridRow, "explosion");
            m_power = power;
            m_explosionLife = MAX_EXPLOSION_LIFE;
            UpdateBoardInformation();
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
