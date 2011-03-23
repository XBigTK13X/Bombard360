using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard360
{
    class BoardTile
    {
        private List<XnaDrawable> m_drawableComponents = new List<XnaDrawable>();
        private List<XnaDrawable> m_containedElements = new List<XnaDrawable>();
        private Bomb m_bomb = null;
        private EnvironmentTile m_environmentTile = null;
        private Player m_player = null;
        private Explosion m_explosion = null;
        public void RegisterComponent(XnaDrawable component)
        {
            if(m_containedElements.Count==0)
            {
                m_containedElements.AddRange(new List<XnaDrawable>() { m_bomb, m_environmentTile, m_explosion, m_player });
            }
            switch (component.GetAssetType())
            {
                case "bomb":
                    m_bomb = (Bomb)component;
                    m_drawableComponents.Add(m_bomb);
                    break;
                case "character":
                    m_player = (Player)component;
                    m_drawableComponents.Add(m_player);
                    break;
                case "dirt_floor_tile":
                    m_environmentTile = (EnvironmentTile)component;
                    m_drawableComponents.Add(m_environmentTile);
                    break;
                case "explosion":
                    m_explosion = (Explosion)component;
                    m_drawableComponents.Add(m_explosion);
                    break;
                default:
                    throw new Exception("An unhandled type was detected in BoardTile.");
            }
        }
    }
}
