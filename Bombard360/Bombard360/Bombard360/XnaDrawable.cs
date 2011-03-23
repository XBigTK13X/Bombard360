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
        protected static readonly int COOLDOWN_TIME = 2;
        protected bool m_isActive = true;
        protected bool m_isBlocking = false;

        protected List<XnaDrawable> m_containedGraphics = new List<XnaDrawable>();

        protected SpriteBatch m_renderTarget;
        protected ContentManager m_contentManager;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager assetHandler)
        {
            m_contentManager = assetHandler;
            m_graphic = assetHandler.Load<Texture2D>(m_assetPath);
            m_spriteInfo = SpriteSheetManager.GetSpriteInfo(m_assetName);
        }
        //Draw the sprite to the screen
        public void Draw(SpriteBatch target)
        {
            m_renderTarget = target;
            List<XnaDrawable> deadGraphics = new List<XnaDrawable>();
            foreach (XnaDrawable graphic in m_containedGraphics)
            {
                if (!graphic.IsActive())
                {
                    deadGraphics.Add(graphic);
                }
                else
                {
                    graphic.LoadContent(m_contentManager);
                    graphic.Draw(m_renderTarget);
                }
            }
            foreach (XnaDrawable deadGraphic in deadGraphics)
            {
                m_containedGraphics.Remove(deadGraphic);
            }
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
            UpdateContainedGraphics();
        }
        public void Move(int amountX, int amountY)
        {
            if (m_position.Y + amountY > -1 && m_position.Y + amountY < SpriteSheetManager.Rows)
            {
                m_position.Y += amountY;
            }
            if (m_position.X + amountX > -1 && m_position.X + amountX < SpriteSheetManager.Columns)
            {
                m_position.X += amountX;
            }
        }
        public bool IsActive()
        {
            return m_isActive;
        }
        public void UpdateContainedGraphics()
        {
            try
            {
                foreach (XnaDrawable containedGraphic in m_containedGraphics)
                {
                    containedGraphic.Update();
                }
            }
            catch (Exception ignored)
            {

            }
        }
        protected void UpdateBoardInformation()
        {
            BoardManager.AddIfUnblocked((int)m_position.X, (int)m_position.Y, this);
        }
        public bool IsBlocking()
        {
            return m_isBlocking;
        }
        public string GetAssetType()
        {
            return m_assetName;
        }
    }
}