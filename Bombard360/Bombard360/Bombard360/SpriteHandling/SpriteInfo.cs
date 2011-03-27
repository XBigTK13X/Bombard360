﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class SpriteInfo
    {
        public static readonly int Height = 33;
        public static readonly int Width = 33;
        public int X, Y, SpriteIndex, MaxFrame;

        public SpriteInfo(int spriteIndex,int maxFrame)
        {
            X = Width;
            Y = Height;
            SpriteIndex = spriteIndex;
            MaxFrame = maxFrame;
        }
    }
}
