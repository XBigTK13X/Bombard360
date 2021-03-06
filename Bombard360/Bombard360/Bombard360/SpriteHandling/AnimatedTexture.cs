﻿using System;
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
        private readonly int m_ANIMATE_SPEED = 20;

        private SpriteBatch m_renderTarget;
        private ContentManager m_contentManager;
        private int m_currentFrame;
        private SpriteInfo m_spriteInfo;
        private Rectangle m_currentCell;
        private Texture2D m_graphic;
        private int m_animationTimer;

        protected Vector2 m_position = Vector2.Zero;

        public void LoadContent(ContentManager assetHandler,SpriteType assetName)
        {
            if (m_contentManager == null)
            {
                m_contentManager = assetHandler;
                m_graphic = assetHandler.Load<Texture2D>(m_assetPath);
                m_spriteInfo = SpriteSheetManager.GetSpriteInfo(assetName);
                m_currentFrame = 0;
                m_animationTimer = m_ANIMATE_SPEED;
            }
        }

        public void Draw(SpriteBatch target)
        {
            UpdateAnimation();
            m_renderTarget = target;
            target.Begin();
            m_currentCell = new Rectangle(m_currentFrame * m_spriteInfo.X, m_spriteInfo.SpriteIndex * m_spriteInfo.Y, m_spriteInfo.X, m_spriteInfo.Y);
            Vector2 tempPosition = new Vector2(m_position.Y * SpriteInfo.Width, m_position.X * SpriteInfo.Height);
            target.Draw(m_graphic, tempPosition, m_currentCell, Color.White);
            target.End();
        }

        private void UpdateAnimation()
        {
            if (m_spriteInfo.MaxFrame != 1)
            {
                m_animationTimer--;
                if (m_animationTimer <= 0)
                {
                    m_currentFrame = (m_currentFrame + 1) % m_spriteInfo.MaxFrame;
                    m_animationTimer = m_ANIMATE_SPEED;
                }
            }
        }

        public void SetSpriteInfo(SpriteInfo sprite)
        {
            if (m_spriteInfo != sprite)
            {
                m_spriteInfo = sprite;
                m_currentFrame = 0;
            }
        }

        public Vector2 GetPosition()
        {
            return m_position;
        }

        public void SetPosition(int x, int y)
        {
            m_position.X = x;
            m_position.Y = y;
        }
    }
}
