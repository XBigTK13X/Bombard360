using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Powerups
{
    class BombCachePowerup:IPowerupEffect
    {
        public override void Apply(Player player)
        {
            if (m_isActive)
            {
                player.ModifyBombCache(1);
                m_isActive = false;
            }
        }
        public override void Undo(Player player)
        {
            player.ModifyBombCache(-1);
        }
    }
}
