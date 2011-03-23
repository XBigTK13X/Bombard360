using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class SpriteSheetManager
    {
        public static readonly int Columns = 20;
        public static readonly int Rows = 20;
        private static Dictionary<string, SpriteInfo> m_manager = new Dictionary<string, SpriteInfo>()
        {
            {"dirt_floor_tile",new SpriteInfo(0)},
            {"character",new SpriteInfo(1)},
            {"bomb",new SpriteInfo(2)},
            {"wall",new SpriteInfo(3)},
            {"explosion",new SpriteInfo(4)}
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
