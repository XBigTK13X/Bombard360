using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Management;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bombard360
{
    class LevelEditorBoard
    {
        private SpriteType m_backgroundTile = SpriteType.DIRT_FLOOR;
        private SpriteType[,] m_tileMap = new SpriteType[SpriteSheetManager.Columns, SpriteSheetManager.Rows];
        private AnimatedTexture[,] m_displayMap = new AnimatedTexture[SpriteSheetManager.Columns, SpriteSheetManager.Rows];

        public LevelEditorBoard()
        {
            for (int ii = 0; ii < SpriteSheetManager.Columns; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Rows; jj++)
                {
                    m_tileMap[ii, jj] = SpriteType.EMPTY;
                }
            }
        }
        public void PlaceTile(SpriteType tile, int gridCol, int gridRow)
        {
            m_tileMap[gridCol, gridRow] = tile;
        }
        public void RemoveTile(int gridCol, int gridRow)
        {
            m_tileMap[gridCol, gridRow] = SpriteType.EMPTY;
        }
        private AnimatedTexture[,] GenerateDisplayData()
        {
            var data = new AnimatedTexture[SpriteSheetManager.Columns, SpriteSheetManager.Rows];
            for (int ii = 0; ii < SpriteSheetManager.Columns; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Rows; jj++)
                {
                    data[ii, jj] = AnimatedTextureFactory.Create(m_tileMap[ii,jj], ii, jj);

                }
            }
            return data;
        }
        public void Draw(SpriteBatch target)
        {
            m_displayMap = GenerateDisplayData();
            foreach (AnimatedTexture sprite in GenerateDisplayData())
            {
                sprite.Draw(target);
            }
        }
        public void LoadContent(ContentManager assetHandler)
        {
            AnimatedTextureFactory.SetContentManager(assetHandler);
        }
        public BoardTile[,] GenerateLevelData(int humanPlayers)
        {
            BoardTile[,] data = new BoardTile[SpriteSheetManager.Columns, SpriteSheetManager.Rows];
            for (int ii = 0; ii < SpriteSheetManager.Columns; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Rows; jj++)
                {
                    data[ii, jj] = new BoardTile(ii, jj);
                    data[ii,jj].Register(GameplayObjectFactory.Create(m_tileMap[ii,jj],ii,jj));
                    data[ii, jj].Register(GameplayObjectFactory.Create(m_backgroundTile,ii,jj));
                }
            }
            return data;
        }
        public void SetBackground(SpriteType type)
        {
            m_backgroundTile = type;
        }
    }
}
