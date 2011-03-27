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
            if (BoardManager.HasTileType(m_position, SpriteType.EXPLOSION))
            {
                m_bombLife = 0;
            }
            if (m_bombLife <= 0&&m_isActive)
            {
                Explode((int)m_position.X,(int)m_position.Y);
                m_isActive = false;
            }
        }
        private void Explode(int gridColumn, int gridRow)
        {
            m_owner.FreeBombCacheSlot();
            List<KeyValuePair<int, int>> explosionPositions = new List<KeyValuePair<int, int>>();
            for (int ii = 0; ii < m_range; ii++)
            {
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn + ii, gridRow));
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn, gridRow + ii));
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn - ii, gridRow));
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn, gridRow - ii));
            }
            Explosion explosionToAdd = null;
            foreach (KeyValuePair<int, int> pair in explosionPositions)
            {
                explosionToAdd = new Explosion(pair.Key, pair.Value, m_power);
                BoardManager.Add(pair.Key, pair.Value, explosionToAdd);
            }
        }
    }
}