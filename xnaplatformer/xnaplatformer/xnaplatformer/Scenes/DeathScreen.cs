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
    class DeathScreen : GameScreen
    {
        SpriteFont font;
        Texture2D continueBtnTexture,
            backgroundTexture;
        Vector2 countdownPos, messagePos;
        MouseState mouse, oldmouse;
        float continueCounter;

        public void Initialize()
        {
            continueCounter = 5.0f;
            messagePos = new Vector2(dimensions.X / 2 - 280,  dimensions.Y / 3);
            countdownPos = new Vector2(dimensions.X / 2 - 20, 2 * dimensions.Y / 3);
        }

        public override void LoadContent(ContentManager Content)
        {
            Initialize();

            font = Content.Load<SpriteFont>("Font1");
            continueBtnTexture = Content.Load<Texture2D>("continue_btn");
            backgroundTexture = Content.Load<Texture2D>("death_screen");

            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            continueCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (continueCounter <= 0)
            {
                ScreenManager.Instance.AddScreen(new TitleScreen());
            }

            oldmouse = mouse;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, dimensions.X, dimensions.Y), Color.White);
            spriteBatch.DrawString(font, (int)continueCounter + "", countdownPos, Color.White);
            spriteBatch.DrawString(font, "You didn't get enough nuts for winter.", messagePos, Color.White);
        }
    }
}
