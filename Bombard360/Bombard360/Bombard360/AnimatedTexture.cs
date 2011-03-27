using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class AnimatedTexture
    {
        private readonly string m_assetPath = @"GameplaySheet";
        private readonly int m_animateFramesPerSecond = 1;

        private SpriteBatch m_renderTarget;
        private ContentManager m_contentManager;
        private int m_currentFrame;
        private SpriteInfo m_spriteInfo;
        private Rectangle m_currentCell;
        private Texture2D m_graphic;

        public void LoadContent(ContentManager assetHandler,string assetName)
        {
            m_contentManager = assetHandler;
            m_graphic = assetHandler.Load<Texture2D>(m_assetPath);
            m_spriteInfo = SpriteSheetManager.GetSpriteInfo(assetName);
            m_currentFrame = 0;
        }

        public void Draw(SpriteBatch target,Vector2 position)
        {
            m_renderTarget = target;
            target.Begin();
            m_currentCell = new Rectangle(m_currentFrame * m_spriteInfo.X, m_spriteInfo.SpriteIndex * m_spriteInfo.Y, m_spriteInfo.X, m_spriteInfo.Y);
            Vector2 tempPosition = new Vector2(position.Y * SpriteInfo.Width, position.X * SpriteInfo.Height);
            target.Draw(m_graphic, tempPosition, m_currentCell, Color.White);
            target.End();
        }

        public void Animate()
        {
            if (m_spriteInfo.MaxFrame != 1)
            {
                m_currentFrame = (m_currentFrame + 1) % m_spriteInfo.MaxFrame;
            }
        }
    }
}
