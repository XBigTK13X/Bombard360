using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class SpriteSheetManager
    {
        private static Dictionary<string, SpriteInfo> m_manager = new Dictionary<string, SpriteInfo>()
        {
            {"dirt_floor",new SpriteInfo(0)}
        };
        public static SpriteInfo GetSpriteInfo(string spriteName)
        {
            try
            {
                return m_manager[spriteName];
            }
            catch (KeyNotFoundException e)
            {
                throw e;
            }
        }
    }
}
