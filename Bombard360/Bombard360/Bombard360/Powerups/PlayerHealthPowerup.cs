using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360.Powerups
{
    class PlayerHealthPowerup:IPowerupEffect
    {
        public override void Apply(Player player)
        {
            if (m_isActive)
            {
                player.ModifyHealth(1);
                m_isActive = false;
            }
        }
        public override void Undo(Player player)
        {
            player.ModifyHealth(-1);
        }
    }
}
