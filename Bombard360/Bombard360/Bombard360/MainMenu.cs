using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bombard360
{
    class MainMenu
    {
        List<MenuButton> m_buttons = new List<MenuButton>();
        public MainMenu()
        {
            
            m_buttons.Add(new MenuButton(new LevelEditorState(),SpriteType.EDIT_BUTTON,12,10));
            m_buttons.Add(new MenuButton(new GameplayState(), SpriteType.PLAY_BUTTON,8,10));
        }
        public void Draw(SpriteBatch target)
        {
            foreach (MenuButton button in m_buttons)
            {
                button.Draw(target);
            }
        }
        public void LoadContent(ContentManager assetHandler)
        {
            foreach (MenuButton button in m_buttons)
            {
                button.LoadContent(assetHandler);
            }
        }
        public void Update()
        {
            if (InputManager.IsPressed(InputManager.Commands.MoveUp, 0))
            {
                m_buttons[1].Activate();
            }
            if (InputManager.IsPressed(InputManager.Commands.MoveDown,0))
            {
                m_buttons[0].Activate();
            }

        }
    }
}
