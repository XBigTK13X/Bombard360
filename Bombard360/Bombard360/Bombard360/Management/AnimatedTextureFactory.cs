using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Tiles;
using Microsoft.Xna.Framework.Content;

namespace Bombard360.Management
{
    class AnimatedTextureFactory
    {
        static ContentManager s_assetHandler;
        static public void SetContentManager(ContentManager assetHandler)
        {
            s_assetHandler = assetHandler;
        }
        static public AnimatedTexture Create(SpriteType type,int gridColumn, int gridRow)
        {
            var sprite = new AnimatedTexture();
            sprite.LoadContent(s_assetHandler,type);
            sprite.SetPosition(gridColumn, gridRow);
            return sprite;
        }
    }
}
