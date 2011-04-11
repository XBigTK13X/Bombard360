using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Tiles
{
    class HumanPlayer:Player
    {
        private int m_playerIndex;
        public HumanPlayer(int gridColumn, int gridRow,int playerId):base(gridColumn,gridRow)
        {
            m_playerIndex = playerId;
        }
        public override void Run()
        {
            int yVel = ((InputManager.IsMovingLeft(m_playerIndex)) ? -1 : 0) + ((InputManager.IsMovingRight(m_playerIndex)) ? 1 : 0);
            int xVel = ((InputManager.IsMovingDown(m_playerIndex)) ? 1 : 0) + ((InputManager.IsMovingUp(m_playerIndex)) ? -1 : 0);
            MoveIfPossible(xVel, yVel);
            if (InputManager.IsPlacingBomb(m_playerIndex))
            {
                PlaceBomb();
            }
        }
    }
}
