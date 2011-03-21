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
        protected Vector2 m_position = Vector2.Zero;
        private Texture2D m_graphic;
        private readonly string m_assetPath = @"GameplaySheet";
        protected string m_assetName;
        private int m_currentFrame;
        private SpriteInfo m_spriteInfo;
        private Rectangle m_currentCell;
        protected static readonly int COOLDOWN_TIME = 5;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager assetHandler)
        {
            m_graphic = assetHandler.Load<Texture2D>(m_assetPath);
            m_spriteInfo = SpriteSheetManager.GetSpriteInfo(m_assetName);
        }
        //Draw the sprite to the screen
        public void Draw(SpriteBatch target)
        {
            target.Begin();
            m_currentCell = new Rectangle(m_currentFrame*m_spriteInfo.X,m_spriteInfo.SpriteIndex*m_spriteInfo.Y, m_spriteInfo.X,m_spriteInfo.Y);
            Vector2 tempPosition = new Vector2(m_position.Y * SpriteInfo.Width, m_position.X * SpriteInfo.Height);
            target.Draw(m_graphic, tempPosition,m_currentCell, Color.White);
            target.End();
        }

        protected void Initialize(int gridColumn, int gridRow, string type)
        {
            m_assetName = type;
            m_position = new Vector2(gridColumn, gridRow);
        }
        public virtual void Update()
        {

        }
    }
}