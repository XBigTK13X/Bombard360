using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bombard360
{
    class XnaDrawable
    {

        private readonly string m_assetPath = @"GameplaySheet";
        private int m_currentFrame;
        private SpriteInfo m_spriteInfo;
        private Rectangle m_currentCell;
        private Texture2D m_graphic;

        protected static readonly int COOLDOWN_TIME = 2;
        protected bool m_isActive = true;
        protected bool m_isBlocking = false;
        protected string m_assetName;
        protected Vector2 m_position = Vector2.Zero;
        protected SpriteBatch m_renderTarget;
        protected ContentManager m_contentManager;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager assetHandler)
        {
            m_contentManager = assetHandler;
            m_graphic = assetHandler.Load<Texture2D>(m_assetPath);
            m_spriteInfo = SpriteSheetManager.GetSpriteInfo(m_assetName);
            m_currentFrame = 0;
        }
        //Draw the sprite to the screen
        public void Draw(SpriteBatch target)
        {
            m_renderTarget = target;
            target.Begin();
            m_currentCell = new Rectangle(m_currentFrame * m_spriteInfo.X, m_spriteInfo.SpriteIndex * m_spriteInfo.Y, m_spriteInfo.X, m_spriteInfo.Y);
            Vector2 tempPosition = new Vector2(m_position.Y * SpriteInfo.Width, m_position.X * SpriteInfo.Height);
            target.Draw(m_graphic, tempPosition, m_currentCell, Color.White);
            target.End();
        }

        protected void Initialize(int gridColumn, int gridRow, string type)
        {
            m_assetName = type;
            m_position = new Vector2(gridColumn, gridRow);
        }
        public virtual void Update()
        {
            if (m_spriteInfo.MaxFrame != 1)
            {
                m_currentFrame = (m_currentFrame + 1) % m_spriteInfo.MaxFrame;
            }
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
        public string GetAssetType()
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
    }
}