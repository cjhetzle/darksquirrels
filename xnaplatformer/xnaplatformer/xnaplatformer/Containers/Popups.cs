using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xnaplatformer
{
    class Popups
    {
        private List<Popup> allPopups;
        private SpriteFont font;

        public Popups()
        {
            allPopups = new List<Popup>();
        }

        public void Load(SpriteFont font)
        {
            this.font = font;
        }

        public void Update(GameTime gameTime)
        {
            short i = 0, l = (short)allPopups.Count;
            for (; i < l; ++i)
            {
                if (allPopups[i].Finished)
                {
                    allPopups.RemoveAt(i);
                    --l;
                    --i;
                }
                else
                {
                    allPopups[i].Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Popup pu in allPopups)
                pu.Draw(spriteBatch);
        }

        public void Insert(Vector2 position, String message)
        {
            allPopups.Add(new Popup(position, message, font));
        }

        public int getNumber()
        {
            return allPopups.Count;
        }
    }
}
