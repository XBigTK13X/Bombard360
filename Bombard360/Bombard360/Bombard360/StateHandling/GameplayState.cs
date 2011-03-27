using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bombard360.Tiles;

namespace Bombard360
{
    class GameplayState:State
    {
        public GameplayState()
        {
            BoardManager.Initialize();
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
