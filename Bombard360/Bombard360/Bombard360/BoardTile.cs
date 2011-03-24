using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
        private Vector2 m_position;
        private ContentManager m_assetHandler;

        public BoardTile(int gridColumn, int gridRow)
        {
            m_position = new Vector2(gridColumn, gridRow);
        }

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
        private bool Contains(string assetType)
        {
            var result = m_drawableComponents.Where(n => n.GetAssetType() == assetType);
            if (result.Count()==0)
            {
                return false;
            }
            return true;
        }
        public bool Register(XnaDrawable component)
        {
            if (!Contains(component.GetAssetType()))
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
                return true;
            }
            return false;
            //Console.Write(GetDebugLog());
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
            //Console.Write(GetDebugLog());
            return true;
        }

        public void RemoveGarbage()
        {
           //Console.Write(GetDebugLog());
            var garbage = m_drawableComponents.Where(i => !i.IsActive());
            if (garbage.Count() > 0)
            {
               m_drawableComponents.RemoveAll(item => !item.IsActive());
            }
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
                    if (item.GetPosition().X != m_position.X || item.GetPosition().Y != m_position.Y)
                    {
                        switch (item.GetAssetType())
                        {
                            case "character":
                                BoardManager.Add((Player)item);
                                Unregister((Player)item);
                                return;
                            default:
                                throw new Exception("Unhandled move scenario encountered!");
                        }
                    }
                    else
                    {
                        item.Update();
                    }
                }
            }
            catch (Exception ignore)
            {
                return;
            }
        }
        public void Draw(SpriteBatch target)
        {
            foreach (XnaDrawable component in m_drawableComponents)
            {
                if (!component.IsGraphicLoaded())
                {
                    component.LoadContent(m_assetHandler);
                }
                component.Draw(target);
            }
        }
        public void LoadContent(ContentManager assetHandler)
        {
            m_assetHandler = assetHandler;
            foreach (XnaDrawable componenet in m_drawableComponents)
            {
                componenet.LoadContent(assetHandler);
            }
        }
        public string GetDebugLog()
        {
            string result = "";
            foreach (XnaDrawable item in m_drawableComponents)
            {
                if(item.GetAssetType().Contains("explosion"))
                    result += item.GetAssetType() + ",";
            }
            if (result != "")
            {
                result = m_position.X + ":::" + m_position.Y + " -> " + result;
            }
            return result;
        }
    }
}
