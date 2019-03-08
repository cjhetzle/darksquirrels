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
    class WinScreen : GameScreen
    {

        SpriteFont font;
        Texture2D continueBtnTexture;
        Vector2 countdownPos, messagePos;
        MouseState mouse, oldmouse;
        float continueCounter;
        int nextLevel;

        public WinScreen(int nextLevel)
        {
            continueCounter = 5.0f;
            messagePos = new Vector2(dimensions.X / 2 - 400, dimensions.Y / 3);
            countdownPos = new Vector2(dimensions.X / 2 - 20, 2 * dimensions.Y / 3);
            this.nextLevel = nextLevel;
        }

        public void Initialize()
        {
            continueCounter = 5.0f;
            messagePos = new Vector2(dimensions.X / 2 - 400, dimensions.Y / 3);
            countdownPos = new Vector2(dimensions.X / 2 - 20, 2 * dimensions.Y / 3);
        }

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Font1");
            continueBtnTexture = Content.Load<Texture2D>("continue_btn");

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
                if (nextLevel == 2)
                {
                    ScreenManager.Instance.AddScreen(new Level2Screen());
                }
                else
                {
                    ScreenManager.Instance.AddScreen(new TitleScreen());
                }
            }

            oldmouse = mouse;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, (int)continueCounter + "", countdownPos, Color.Black);
            spriteBatch.DrawString(font, "You managed to get enough food to survive winter!", messagePos, Color.Black);
        }
    }
}
