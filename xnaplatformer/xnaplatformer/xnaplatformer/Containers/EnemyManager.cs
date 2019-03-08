using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace xnaplatformer
{
    class EnemyManager
    {
        private List<Fox> foxes;
        private Point size;

        public EnemyManager(Point size)
        {
            this.size = size;
            foxes = new List<Fox>();
        }

        public void Load(ContentManager Content)
        {
            Fox.img = Content.Load<Texture2D>("fox");
        }

        public void Update(GameTime gameTime)
        {
            foreach (Fox fox in foxes)
            {
                fox.Update(gameTime);
            }
        }

        public void Scroll(int delta)
        {
            foreach (Fox fox in foxes)
            {
                fox.Pos = new Vector2(fox.Pos.X - delta, fox.Pos.Y);
            }
        }

        public bool Intersects(Rectangle rect)
        {
            foreach (Fox fox in foxes)
            {
                if (fox.Rect.Intersects(rect)) return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Fox fox in foxes)
            {
                fox.Draw(spriteBatch);
            }
        }

        public void Add(Point pos)
        {
            foxes.Add(new Fox(new Point(pos.X * size.X, pos.Y * size.Y), size));
        }
    }
}
