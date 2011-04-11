using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Powerups;

namespace Bombard360.Tiles
{
    class Powerup:GameplayObject
    {

        public Powerup(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow, SpriteType.POWERUP);
        }
        public override void Update()
        {
            if (BoardManager.HasTileType(m_graphic.GetPosition(), SpriteType.PLAYER_STAND) || BoardManager.HasTileType(m_graphic.GetPosition(), SpriteType.PLAYER_WALK))
            {
                IPowerupEffect power = null;
                Random rand = new Random();
                int chance = rand.Next(0,100);
                if (chance < 25)
                {
                    power = new SpeedPowerup();
                }
                else if (chance >= 25 && chance < 50)
                {
                    power = new PlayerHealthPowerup();
                }
                else if (chance >= 50 && chance < 75)
                {
                    power = new BombCachePowerup();
                }
                else if (chance >= 75)
                {
                    power = new BombPowerPowerup();
                }
                Player temp = (Player)BoardManager.GetTileType(m_graphic.GetPosition(), SpriteType.PLAYER_STAND);
                Player temp2 = (Player)BoardManager.GetTileType(m_graphic.GetPosition(), SpriteType.PLAYER_WALK);
                if (null != temp)
                {
                    temp.AddPowerup(power);
                }
                if (null != temp2)
                {
                    temp2.AddPowerup(power);
                }
                m_isActive = false;
            }
        }
    }
}
