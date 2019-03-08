using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace xnaplatformer
{
    class WinterWall : Object
    {
        private int speed;
        private bool increase;

        public WinterWall(int width, int height)
        {
            rect.Width = width;
            rect.Height = height;
            pos = new Vector2(0, 0);
            speed = 2;
            increase = false;
        }

        public override void Load(ContentManager Content)
        {
            img = Content.Load<Texture2D>("snowflake");
            base.Load(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (increase)
                Increase(gameTime);
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            base.Update(gameTime);
        }

        private void Increase(GameTime gameTime)
        {
            rect.Width += speed;
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Width
        {
            get { return rect.Width; }
            set { rect.Width = value; }
        }

        public bool IncreasingWidth
        {
            get { return increase; }
            set { increase = value; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, rect, new Color(10, 50, 200, 100));
        }
    }
}
