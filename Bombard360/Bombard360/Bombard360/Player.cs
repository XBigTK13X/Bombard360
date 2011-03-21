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

        public Player(int gridColumn, int gridRow, int playerId, bool isHuman)
        {
            Initialize(gridColumn, gridRow, "character");
            m_isHuman = isHuman;
            m_playerIndex = playerId;
        }
        public override void Update()
        {
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
        public void Move(int amountX, int amountY)
        {
            if (m_position.Y + amountY > 0 && m_position.Y + amountY < SpriteSheetManager.Rows)
            {
                m_position.Y += amountY;
            }
            if (m_position.X + amountX > 0 && m_position.X + amountX < SpriteSheetManager.Columns)
            {
                m_position.X += amountX;
            }
        }

        private void RunAsHuman()
        {
            int yVel = ((InputManager.IsMovingLeft(m_playerIndex)) ? -1 : 0) + ((InputManager.IsMovingRight(m_playerIndex)) ? 1 : 0);
            int xVel = ((InputManager.IsMovingDown(m_playerIndex)) ? 1 : 0) + ((InputManager.IsMovingUp(m_playerIndex)) ? -1 : 0);
            m_moveCooldown--;
            if (m_moveCooldown <= 0)
            {
                Move(xVel, yVel);
                m_moveCooldown = COOLDOWN_TIME;
            }
        }
        private void RunAsComputer()
        {
            Move(0, 1);
        }
    }
}
