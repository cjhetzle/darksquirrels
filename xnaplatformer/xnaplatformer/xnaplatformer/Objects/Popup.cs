using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace xnaplatformer
{
    class Popup : Object
    {
        private SpriteFont font;
        private float speed, fade_out_start, fade_out;
        private String message;
        private bool finished;

        public Popup(String message) : base()
        {
            this.message = message;
            speed = 10;
            fade_out = fade_out_start = 2;
        }

        public Popup(Vector2 pos, String message, SpriteFont font)
        {
            this.message = message;
            this.pos = pos;
            this.font = font;
            speed = 10;
            fade_out = fade_out_start = 2f;
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);
        }

        public override void Update(GameTime gameTime)
        {
            pos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            fade_out -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (fade_out - float.Epsilon <= 0)
                finished = true;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, message, pos, new Color(0, 0, 0, (int)(255*(fade_out / fade_out_start))));
        }

        public bool Finished
        {
            get { return finished; }
        }

    }
}
