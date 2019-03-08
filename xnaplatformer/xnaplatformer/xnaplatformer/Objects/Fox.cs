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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    class Fox : Object
    {

        new public static Texture2D img;

        public Fox(Point pos, Point dimensions)
        {
            // TODO: Construct any child components here
            this.pos = new Vector2((int)pos.X, (int)pos.Y);
            this.rect = new Rectangle(pos.X, pos.Y, dimensions.X, dimensions.Y);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, rect, Color.White);
        }
    }
}
