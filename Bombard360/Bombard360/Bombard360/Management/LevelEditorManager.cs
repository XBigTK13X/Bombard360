using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Bombard360.Tiles;
using Bombard360.Management;

namespace Bombard360
{
    class LevelEditorManager
    {
        private static LevelEditorBoard m_board = new LevelEditorBoard();
        private static Cursor m_cursor = new Cursor(0, 0);

        private static int m_playerIndex;
        static public void Initialize(int playerIndex)
        {
            m_playerIndex = playerIndex;
        }
        static public void Update()
        {
            m_cursor.Update();
            if (InputManager.IsPressed(InputManager.Commands.SaveFile, 0))
            {
                SaveManager.Append(m_board.GenerateSaveData());
            }
        }
        static public void Draw(SpriteBatch target)
        {
            m_board.Draw(target);
            m_cursor.Draw(target);
        }
        public static void LoadContent(ContentManager assetHandler)
        {
            m_board.LoadContent(assetHandler);
            m_cursor.LoadContent(assetHandler);
        }
        public static void PlaceTile(SpriteType tile,int gridColumn, int gridRow)
        {
            m_board.PlaceTile(tile,gridColumn,gridRow);
        }
        public static void RemoveTile(int gridColum, int gridRow)
        {
            m_board.RemoveTile(gridColum, gridRow);
        }
    }
}
