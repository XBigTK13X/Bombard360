using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bombard360
{
    class BoardTile
    {
        private List<XnaDrawable> m_drawableComponents = new List<XnaDrawable>();

        private Bomb m_bomb = null;
        private EnvironmentTile m_environmentTile = null;
        private Player m_player = null;
        private Explosion m_explosion = null;
        private Wall m_wall = null;


        public bool IsTypeRegistered(string type)
        {
            foreach (XnaDrawable element in m_drawableComponents)
            {
                if (type == element.GetAssetType())
                {
                    return true;
                }
            }
            return false;
        }
        public void Register(XnaDrawable component)
        {
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
                case "wall":
                    m_wall = (Wall)component;
                    m_drawableComponents.Add(m_wall);
                    break;
                default:
                    throw new Exception("An unhandled type was detected in BoardTile.");
            }
        }

        public bool Unregister(XnaDrawable component)
        {
            switch (component.GetAssetType())
            {
                case "bomb":
                    m_drawableComponents.Remove(m_bomb);
                    break;
                case "character":
                    m_drawableComponents.Remove(m_player);
                    break;
                case "dirt_floor_tile":
                    m_drawableComponents.Remove(m_environmentTile);
                    break;
                case "explosion":
                    m_drawableComponents.Remove(m_explosion);
                    break;
                case "wall":
                    m_drawableComponents.Remove(m_wall);
                    break;
                default:
                    throw new Exception("An unhandled type was detected in BoardTile.");
            }
            return true;
        }

        public bool IsBlocked()
        {
            foreach (XnaDrawable item in m_drawableComponents)
            {
                if (item.IsBlocking())
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveGarbage()
        {
            m_drawableComponents.RemoveAll(item => !item.IsActive());
        }

        public List<XnaDrawable> GetDrawableComponents()
        {
            return m_drawableComponents;
        }

        public XnaDrawable GetTileOfType(string type)
        {
            foreach (XnaDrawable item in m_drawableComponents)
            {
                if (item.GetAssetType() == type)
                {
                    return item;
                }
            }
            return null;
        }
        public void Update()
        {
            try
            {
                foreach (XnaDrawable item in m_drawableComponents)
                {
                    item.Update();
                }
            }
            catch(Exception ignore)
            {
                Console.WriteLine("IGNORED: {0}", ignore.Message);
            }
        }
        public void Draw(SpriteBatch target)
        {
            foreach (XnaDrawable componenet in m_drawableComponents)
            {
                componenet.Draw(target);
            }
        }
        public void LoadContent(ContentManager assetHandler)
        {
            foreach (XnaDrawable componenet in m_drawableComponents)
            {
                componenet.LoadContent(assetHandler);
            }
        }
    }
}
