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
            int yVel = ((InputManager.IsPressed(InputManager.Commands.MoveLeft, m_playerIndex)) ? -1 : 0) + ((InputManager.IsPressed(InputManager.Commands.MoveRight, m_playerIndex)) ? 1 : 0);
            int xVel = ((InputManager.IsPressed(InputManager.Commands.MoveDown, m_playerIndex)) ? 1 : 0) + ((InputManager.IsPressed(InputManager.Commands.MoveUp, m_playerIndex)) ? -1 : 0);
            MoveIfPossible(xVel, yVel);
            if (InputManager.IsPressed(InputManager.Commands.PlaceBomb,m_playerIndex))
            {
                PlaceBomb();
            }
        }
    }
}
