using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class IPowerupEffect
    {
        protected bool m_isTemporary = false;
        protected bool m_isActive = true;
        int m_lifeTime = 100;
        public virtual void Apply(Player player) { }
        public virtual void Undo(Player player) { }
        public void Update(Player player)
        {
            if (m_isTemporary)
            {
                m_lifeTime--;
                if (m_lifeTime <= 0)
                {
                    m_isActive = false;
                    Undo(player);
                }
            }
        }
    }
}
