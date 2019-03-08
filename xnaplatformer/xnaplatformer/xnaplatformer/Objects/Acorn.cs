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
    class Acorn : Object
    {
        public Acorn(Rectangle rect)
            : base()
        {
            this.rect = rect;
            this.pos = new Vector2(rect.X, rect.Y);
            this.rect.X = (int)this.pos.X;
            this.rect.Y = (int)this.pos.Y;
        }

        public new void Load(ContentManager Content)
        {
             img = Content.Load<Texture2D>("Acorn");
        }


        public void Update(GameTime gameTime, KeyboardState keys, KeyboardState oldkeys, Grid grid)
        {
            base.Update(gameTime);
        }

        public void scroll(int autospeed)
        {
            rect.X -= autospeed;
            pos.X -= autospeed;
        }

        public static Acorn[] spawn(int[,] arr)
        {
            Acorn[] acorns = new Acorn[arr.GetLength(0)];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                acorns[i] = new Acorn(new Rectangle(50 * arr[i, 0], 50 * arr[i, 1], 50, 50));
            }
            return acorns;
        }
    }
}