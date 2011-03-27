using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Tiles
{
    class Crate:GameplayObject
    {
        private int m_health = 100;
        private Random m_random = new Random((int)DateTime.Now.Ticks);
        private readonly int m_dropChancePercent = 100;

        public Crate(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, SpriteType.CRATE);
            m_isBlocking = true;
        }
        public override void Update()
        {
            if (m_isActive)
            {
                if (BoardManager.HasTileType(m_position, SpriteType.EXPLOSION))
                {
                    m_health -= BoardManager.GetExplosionInstance(m_position).GetPower();
                    if (m_health <= 0)
                    {
                        RandomlyDropPowerup();
                        m_isActive = false;
                    }
                }
            }
        }
        private void RandomlyDropPowerup()
        {
            if (m_random.Next(0, 100)>(100-m_dropChancePercent))
            {
                BoardManager.Add(new Powerup((int)m_position.X, (int)m_position.Y));
            }
            m_isActive = false;
        }
    }
}
