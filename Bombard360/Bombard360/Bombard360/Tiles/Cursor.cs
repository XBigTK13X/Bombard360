using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Tiles
{
    class Cursor:GameplayObject
    {
        private int m_playerIndex = 0;
        private int m_blockIndex = 0;
        private int m_cooldown = 0;
        private List<SpriteType> m_allowedBlocks = new List<SpriteType>()
        {
            SpriteType.CURSOR,
            SpriteType.CRATE,
            SpriteType.PLAYER_STAND,
            SpriteType.PLAYER_WALK,
            SpriteType.WALL
        };
        
        public Cursor(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, SpriteType.CURSOR);
            LevelEditorManager.Initialize(m_playerIndex);
        } 
        public override void Update()
        {
            int yVel = ((InputManager.IsPressed(InputManager.Commands.MoveLeft, m_playerIndex)) ? -1 : 0) + ((InputManager.IsPressed(InputManager.Commands.MoveRight,m_playerIndex)) ? 1 : 0);
            int xVel = ((InputManager.IsPressed(InputManager.Commands.MoveDown, m_playerIndex)) ? 1 : 0) + ((InputManager.IsPressed(InputManager.Commands.MoveUp, m_playerIndex)) ? -1 : 0);
            Move(xVel, yVel);
            m_cooldown--;
            if (InputManager.IsPressed(InputManager.Commands.PlaceBomb,m_playerIndex)&&m_cooldown<=0)
            {
                //Cycle through the sprites available, one at a time
                m_blockIndex = (m_blockIndex + 1) % m_allowedBlocks.Count;
                SetSpriteInfo(SpriteSheetManager.GetSpriteInfo(m_allowedBlocks[m_blockIndex]));
                m_cooldown = COOLDOWN_TIME;
            }
            if (InputManager.IsPressed(InputManager.Commands.Confirm, m_playerIndex))
            {
                if (m_blockIndex != 0)
                {
                    LevelEditorManager.PlaceTile(m_allowedBlocks[m_blockIndex], (int)m_graphic.GetPosition().X, (int)m_graphic.GetPosition().Y);
                }
                else
                {
                    LevelEditorManager.RemoveTile((int)m_graphic.GetPosition().X, (int)m_graphic.GetPosition().Y);
                }
            }
        }
    }
}