using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class SpriteInfo
    {
        public static readonly int Height = 32;
        public static readonly int Width = 32;
        public int X, Y, SpriteIndex;

        public SpriteInfo(int spriteIndex)
        {
            X = Width;
            Y = Height;
            SpriteIndex = spriteIndex;
        }
    }
}
