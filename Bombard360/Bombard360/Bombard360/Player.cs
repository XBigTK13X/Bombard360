using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class Player:XnaDrawable
    {
        private bool m_isHuman;
        private float m_health = 100;
        private bool m_isAlive;
        private int m_playerIndex;
        private int m_moveCooldown = COOLDOWN_TIME;
        private readonly int MAX_BOMB_CACHE_SIZE = 10;
        private int m_currentBombCacheSize = 0;

        public Player(int gridColumn, int gridRow, int playerId, bool isHuman)
        {
            Initialize(gridColumn, gridRow, "character");
            m_isHuman = isHuman;
            m_playerIndex = playerId;
        }
        public override void Update()
        {
            base.Update();
            if (m_isHuman)
            {
                RunAsHuman();
            }
            else
            {
                RunAsComputer();
            }
        }

        public bool TakeDamage(int damageDealt)
        {
            m_health -= damageDealt;
            if (m_health <= 0)
            {
                m_isAlive = false;
            }
            return m_isAlive;
        }
        private void PlaceBomb()
        {
            if (MAX_BOMB_CACHE_SIZE > m_currentBombCacheSize)
            {
                m_containedGraphics.Add(new Bomb((int)m_position.X, (int)m_position.Y, this));
                m_currentBombCacheSize++;
            }
        }
        public void ExplodeBomb(int gridColumn, int gridRow,int power)
        {
            if (m_currentBombCacheSize > 0)
            {
                m_currentBombCacheSize--;
            }
            List<KeyValuePair<int,int>> explosionPositions = new List<KeyValuePair<int,int>>();
            for (int ii = 0; ii < power; ii++)
            {
                explosionPositions.Add(new KeyValuePair<int,int>(gridColumn + ii, gridRow));
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn, gridRow+ii));
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn - ii, gridRow));
                explosionPositions.Add(new KeyValuePair<int, int>(gridColumn, gridRow - ii));
            }
            foreach (KeyValuePair<int, int> pair in explosionPositions)
            {
                m_containedGraphics.Add(new Explosion(pair.Key, pair.Value, power));
            }
        }
        private void RunAsHuman()
        {
            int yVel = ((InputManager.IsMovingLeft(m_playerIndex)) ? -1 : 0) + ((InputManager.IsMovingRight(m_playerIndex)) ? 1 : 0);
            int xVel = ((InputManager.IsMovingDown(m_playerIndex)) ? 1 : 0) + ((InputManager.IsMovingUp(m_playerIndex)) ? -1 : 0);
            m_moveCooldown--;
            if (m_moveCooldown <= 0)
            {
                if(BoardManager.IsCellEmpty((int)m_position.X+xVel,(int)m_position.Y+yVel))
                {
                    Move(xVel, yVel);
                    m_moveCooldown = COOLDOWN_TIME;
                }
            }
            if (InputManager.IsPlacingBomb(m_playerIndex))
            {
                PlaceBomb();
            }
        }
        private void RunAsComputer()
        {
            Move(0, 1);
        }
    }
}
