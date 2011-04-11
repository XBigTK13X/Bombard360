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
        protected List<GameplayObject> m_windowComponents = new List<GameplayObject>();
        public virtual void LoadContent(ContentManager assetHandler)
        {
            foreach (GameplayObject component in m_windowComponents)
            {
                component.LoadContent(assetHandler);
            }
        }
        
        public virtual void Draw(SpriteBatch target)
        {
            foreach (GameplayObject component in m_windowComponents)
            {
                component.Draw(target);
            }
        }
        public virtual void Update()
        {
            throw new Exception("State.Update() was not defined for the current state being used.");
        }
    }
}
