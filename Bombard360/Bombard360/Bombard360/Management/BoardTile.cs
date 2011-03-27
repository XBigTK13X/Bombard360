using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Bombard360.Tiles;

namespace Bombard360
{
    class BoardTile
    {
        private List<GameplayObject> m_drawableComponents = new List<GameplayObject>();

        private Bomb m_bomb = null;
        private EnvironmentTile m_environmentTile = null;
        private Player m_player = null;
        private Explosion m_explosion = null;
        private Wall m_wall = null;
        private Vector2 m_position;
        private ContentManager m_assetHandler;
        private Crate m_crate;
        private Powerup m_powerup;

        public BoardTile(int gridColumn, int gridRow)
        {
            m_position = new Vector2(gridColumn, gridRow);
        }

        public bool IsTypeRegistered(SpriteType type)
        {
            foreach (GameplayObject element in m_drawableComponents)
            {
                if (type == element.GetAssetType())
                {
                    return true;
                }
            }
            return false;
        }
        private bool Contains(SpriteType assetType)
        {
            foreach (GameplayObject item in m_drawableComponents)
            {
                if (item.GetAssetType() == assetType)
                {
                    return true;
                }
            }            
            return false;
        }
        public bool Register(GameplayObject component)
        {
            if (!Contains(component.GetAssetType()))
            {
                switch (component.GetAssetType())
                {
                    case SpriteType.BOMB:
                        m_bomb = (Bomb)component;
                        m_drawableComponents.Add(m_bomb);
                        break;
                    case SpriteType.PLAYER_WALK:
                        m_player = (Player)component;
                        m_drawableComponents.Add(m_player);
                        break;
                    case SpriteType.PLAYER_STAND:
                        m_player = (Player)component;
                        m_drawableComponents.Add(m_player);
                        break;
                    case SpriteType.DIRT_FLOOR:
                        m_environmentTile = (EnvironmentTile)component;
                        m_drawableComponents.Add(m_environmentTile);
                        break;
                    case SpriteType.EXPLOSION:
                        m_explosion = (Explosion)component;
                        m_drawableComponents.Add(m_explosion);
                        break;
                    case SpriteType.WALL:
                        m_wall = (Wall)component;
                        m_drawableComponents.Add(m_wall);
                        break;
                    case SpriteType.CRATE:
                        m_crate = (Crate)component;
                        m_drawableComponents.Add(m_crate);
                        break;
                    case SpriteType.POWERUP:
                        m_powerup = (Powerup)component;
                        m_drawableComponents.Add(m_powerup);
                        break;
                    default:
                        throw new Exception("An unhandled type was detected in BoardTile.");
                }
                return true;
            }
            return false;
            //Console.Write(GetDebugLog());
        }

        public bool Unregister(GameplayObject component)
        {
            switch (component.GetAssetType())
            {
                case SpriteType.BOMB:
                    m_drawableComponents.Remove(m_bomb);
                    break;
                case SpriteType.PLAYER_WALK:
                    m_drawableComponents.Remove(m_player);
                    break;
                case SpriteType.PLAYER_STAND:
                    m_drawableComponents.Remove(m_player);
                    break;
                case SpriteType.DIRT_FLOOR:
                    m_drawableComponents.Remove(m_environmentTile);
                    break;
                case SpriteType.EXPLOSION:
                    m_drawableComponents.Remove(m_explosion);
                    break;
                case SpriteType.WALL:
                    m_drawableComponents.Remove(m_wall);
                    break;
                case SpriteType.CRATE:
                    m_drawableComponents.Remove(m_crate);
                    break;
                case SpriteType.POWERUP:
                    m_drawableComponents.Remove(m_powerup);
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
            var deadItems = new List<GameplayObject>();
            foreach (GameplayObject item in m_drawableComponents)
            {
                if (!item.IsActive())
                {
                    deadItems.Add(item);
                }
            }
            foreach (GameplayObject item in deadItems)
            {
                m_drawableComponents.Remove(item);
            }
        }

        public bool IsBlocked()
        {
            foreach (GameplayObject item in m_drawableComponents)
            {
                if (item.IsBlocking())
                {
                    return true;
                }
            }
            return false;
        }

        public GameplayObject GetTileOfType(SpriteType type)
        {
            foreach (GameplayObject item in m_drawableComponents)
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
                foreach (GameplayObject item in m_drawableComponents)
                {
                    if (item.GetPosition().X != m_position.X || item.GetPosition().Y != m_position.Y)
                    {
                        switch (item.GetAssetType())
                        {
                            case SpriteType.PLAYER_WALK:
                                BoardManager.Add((Player)item);
                                Unregister((Player)item);
                                return;
                            case SpriteType.PLAYER_STAND:
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
            foreach (GameplayObject component in m_drawableComponents)
            {
                component.LoadContent(m_assetHandler);
                component.Draw(target);
            }
        }
        public void LoadContent(ContentManager assetHandler)
        {
            m_assetHandler = assetHandler;
            foreach (GameplayObject component in m_drawableComponents)
            {
                if (!component.IsGraphicLoaded())
                {
                    component.LoadContent(assetHandler);
                }
            }
        }

        public Vector2 GetPosition()
        {
            return m_position;
        }

        public string GetDebugLog()
        {
            string result = "";
            foreach (GameplayObject item in m_drawableComponents)
            {
                if(item.GetAssetType()==SpriteType.EXPLOSION||item.GetAssetType()==SpriteType.BOMB)
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
