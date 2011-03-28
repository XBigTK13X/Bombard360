using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class SpriteSheetManager
    {
        public static readonly int Columns = 15;
        public static readonly int Rows = 15;
        private static Dictionary<SpriteType, SpriteInfo> m_manager = new Dictionary<SpriteType, SpriteInfo>()
        {
            {SpriteType.DIRT_FLOOR,new SpriteInfo(0,1)},
            {SpriteType.PLAYER_WALK,new SpriteInfo(1,2)},
            {SpriteType.BOMB,new SpriteInfo(2,1)},
            {SpriteType.WALL,new SpriteInfo(3,1)},
            {SpriteType.EXPLOSION,new SpriteInfo(4,1)},
            {SpriteType.PLAYER_STAND,new SpriteInfo(5,4)},
            {SpriteType.CRATE,new SpriteInfo(6,1)},
            {SpriteType.POWERUP,new SpriteInfo(7,1)}
        };
        public static SpriteInfo GetSpriteInfo(SpriteType spriteName)
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
