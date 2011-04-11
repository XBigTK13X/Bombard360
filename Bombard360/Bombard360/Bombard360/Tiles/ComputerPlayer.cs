using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Tiles
{
    class ComputerPlayer:Player
    {
        private int m_AI_SPEED = COOLDOWN_TIME * 2;
        private int m_aiCooldown;
        public ComputerPlayer(int gridRow, int gridColumn):base(gridColumn,gridRow)
        {
            m_aiCooldown = m_AI_SPEED;
        }
        public override void Run()
        {
            m_aiCooldown--;
            if (m_aiCooldown <= 0)
            {
                int xVel = ((AIManager.IsClosestPlayerWest(this)) ? -1 : 0) + ((AIManager.IsClosestPlayerEast(this)) ? 1 : 0);
                int yVel = ((AIManager.IsClosestPlayerSouth(this)) ? 1 : 0) + ((AIManager.IsClosestPlayerNorth(this)) ? -1 : 0);
                MoveIfPossible(xVel, yVel);
                m_aiCooldown = m_AI_SPEED;
                PlaceBomb();
            }
        }
    }
}
