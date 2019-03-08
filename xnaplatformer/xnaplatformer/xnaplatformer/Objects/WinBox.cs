using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace xnaplatformer
{
    class WinBox : Object
    {
        
        public WinBox(Vector2 pos, Point dimensions)
        {
            this.pos = pos;
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, dimensions.X, dimensions.Y);
        }

        public WinBox(Point pos, Point dimensions)
        {
            this.pos = new Vector2(pos.X, pos.Y);
            this.rect = new Rectangle(pos.X, pos.Y, dimensions.X, dimensions.Y);
        }

        public override void Load(ContentManager Content)
        {

            base.Load(Content);
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
