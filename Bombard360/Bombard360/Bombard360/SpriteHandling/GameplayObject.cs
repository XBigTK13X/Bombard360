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
        private AnimatedTexture m_graphic = new AnimatedTexture(); 
        
        protected static readonly int COOLDOWN_TIME = 6;
        protected bool m_isActive = true;
        protected bool m_isBlocking = false;
        protected SpriteType m_assetName;
        protected Vector2 m_position = Vector2.Zero;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager assetHandler)
        {
            m_graphic.LoadContent(assetHandler,m_assetName);
        }
        //Draw the sprite to the screen
        public void Draw(SpriteBatch target)
        {
            m_graphic.Draw(target,m_position);
        }

        protected void Initialize(int gridColumn, int gridRow, SpriteType type)
        {
            m_assetName = type;
            m_position = new Vector2(gridColumn, gridRow);
        }
        public virtual void Update()
        {
        }
        public void Move(int amountX, int amountY)
        {
            if (BoardManager.IsCoordValid((int)m_position.X + amountX,(int)m_position.Y + amountY))
            {
                m_position.Y += amountY;
                m_position.X += amountX;
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
            return m_position;
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