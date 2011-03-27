using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bombard360
{
    class GameplayState:State
    {
        private readonly Dictionary<int, int> m_wallPositions = new Dictionary<int, int>()
        {
            {1,1},
            {2,2}
        };
        public GameplayState()
        {
            BoardManager.Initialize();
            for (int ii = 0; ii < SpriteSheetManager.Rows; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Columns; jj++)
                {
                    BoardManager.Add(new EnvironmentTile(ii,jj,SpriteType.DIRT_FLOOR));
                }
            }
            foreach (KeyValuePair<int, int> pair in m_wallPositions)
            {
                BoardManager.Add(new Wall(pair.Key, pair.Value));
            }
            BoardManager.Add(new Player(0, 0, 0, true));
        }
        public void Update()
        {            
            BoardManager.Update();
        }
        public override void LoadContent(ContentManager assetHandler)
        {
            BoardManager.LoadContent(assetHandler);
        }
        public override void Draw(SpriteBatch target)
        {
            BoardManager.Draw(target);
        }
    }
}
