using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xnaplatformer
{
    class Object
    {
        protected Texture2D img;
        protected Vector2 pos;
        protected Rectangle rect;

        public Object()
        {

        }

        public Object(Texture2D img)
        {
            this.img = img;
            this.pos = new Vector2(0, 0);
        }

        public Object(Texture2D img, Vector2 pos)
        {
            this.img = img;
            this.pos = pos;
        }

        public virtual void Load(ContentManager Content)
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, pos, Color.White);
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Rectangle Rect
        {
            get { return rect; }
        }
    }
}
