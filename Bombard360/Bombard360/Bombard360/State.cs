using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace Bombard360
{
    class State
    {
       protected List<XnaDrawable> m_windowComponents = new List<XnaDrawable>();

        public void LoadContent(ContentManager assetHandler)
        {
            foreach (XnaDrawable component in m_windowComponents)
            {
                component.LoadContent(assetHandler);
            }
        }
        
        public void Draw(SpriteBatch target)
        {
            foreach (XnaDrawable component in m_windowComponents)
            {
                component.Draw(target);
            }
        }
    }
}
