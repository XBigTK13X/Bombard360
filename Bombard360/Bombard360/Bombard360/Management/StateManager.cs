using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bombard360.Management
{
    class StateManager
    {
        static private State m_state;
        static private SpriteBatch m_target;
        static private ContentManager m_assetHandler;
        static public void LoadState(State state)
        {
            m_state = state;
            if (m_assetHandler != null)
            {
                m_state.LoadContent(m_assetHandler);
                m_state.Draw(m_target);
            }
        }
        static public void Draw(SpriteBatch target)
        {
            m_target = target;
            m_state.Draw(target);
        }
        static public void LoadContent(ContentManager assetHandler)
        {
            m_assetHandler = assetHandler;
            m_state.LoadContent(assetHandler);
        }
        static public void Update()
        {
            m_state.Update();
            if (InputManager.IsGoingToMainMenu(0))
            {
                LoadState(new MainMenuState());
            }
        }
    }
}
