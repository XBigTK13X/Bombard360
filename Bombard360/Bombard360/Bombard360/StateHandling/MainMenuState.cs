using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bombard360.Tiles;

namespace Bombard360
{
    class MainMenuState:State
    {
        private MainMenu m_menu;
        public MainMenuState()
        {
            m_menu = new MainMenu();
        }
        public override void Update()
        {            
            m_menu.Update();
        }
        public override void LoadContent(ContentManager assetHandler)
        {
            m_menu.LoadContent(assetHandler);
        }
        public override void Draw(SpriteBatch target)
        {
            m_menu.Draw(target);
        }
    }
}
