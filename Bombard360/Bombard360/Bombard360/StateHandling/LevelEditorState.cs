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
    class LevelEditorState:State
    {
        public LevelEditorState()
        {
        }
        public override void Update()
        {            
          LevelEditorManager.Update();
        }
        public override void LoadContent(ContentManager assetHandler)
        {
           LevelEditorManager.LoadContent(assetHandler);
        }
        public override void Draw(SpriteBatch target)
        {
            LevelEditorManager.Draw(target);
        }
    }
}
