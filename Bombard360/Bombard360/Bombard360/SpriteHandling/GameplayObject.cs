using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bombard360
{
    class GameplayObject
    {
        protected AnimatedTexture m_graphic = new AnimatedTexture(); 
        
        protected static int COOLDOWN_TIME = 6;
        protected bool m_isActive = true;
        protected bool m_isBlocking = false;
        protected SpriteType m_assetName;
        

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager assetHandler)
        {
            m_graphic.LoadContent(assetHandler,m_assetName);
        }
        //Draw the sprite to the screen
        public void Draw(SpriteBatch target)
        {
            m_graphic.Draw(target);
        }

        protected void Initialize(int gridColumn, int gridRow, SpriteType type)
        {
            m_assetName = type;
            m_graphic.SetPosition(gridColumn,gridRow);
        }
        public virtual void Update()
        {
        }
        public void Move(int amountX, int amountY)
        {
            if (BoardManager.IsCoordValid((int)m_graphic.GetPosition().X + amountX,(int)m_graphic.GetPosition().Y + amountY))
            {
                m_graphic.SetPosition((int)m_graphic.GetPosition().X + amountX, (int)m_graphic.GetPosition().Y + amountY);
            }
        }
        public bool IsActive()
        {
            return m_isActive;
        }
        public void SetInactive()
        {
            m_isActive = false;
        }
        public bool IsBlocking()
        {
            return m_isBlocking;
        }
        public SpriteType GetAssetType()
        {
            return m_assetName;
        }
        public Vector2 GetPosition()
        {
            return m_graphic.GetPosition();
        }
        public bool IsGraphicLoaded()
        {
            return (m_graphic != null);
        }
        protected void SetSpriteInfo(SpriteInfo sprite)
        {
            m_graphic.SetSpriteInfo(sprite);
        }
    }
}