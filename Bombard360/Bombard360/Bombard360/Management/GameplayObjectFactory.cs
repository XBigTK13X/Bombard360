using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Tiles;

namespace Bombard360.Management
{
    class GameplayObjectFactory
    {
        static public GameplayObject Create(SpriteType type, int gridColumn, int gridRow)
        {
            switch (type)
            {
                case SpriteType.CRATE:
                    return new Crate(gridColumn, gridRow);
                case SpriteType.PLAYER_STAND:
                    return new HumanPlayer(gridColumn, gridRow, 0);
                case SpriteType.PLAYER_WALK:
                    return new ComputerPlayer(gridRow, gridColumn);
                case SpriteType.WALL:
                    return new Wall(gridColumn, gridRow);
                case SpriteType.DIRT_FLOOR:
                    return new EnvironmentTile(gridColumn, gridRow,SpriteType.DIRT_FLOOR);
                default:
                    throw new Exception("An undefined SpriteType case was passed into the GameplayObjectFactory.");
            }
        }
    }
}
