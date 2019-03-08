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
    public class TitleScreen : GameScreen
    {
        KeyboardState keyState, oldkeyState;
        SpriteFont font;
        Vector3 playColor = new Vector3(10, 10, 10);
        Color continueClr, quitClr;
        MouseState mouse, oldmouse;
        Rectangle continueRect, quitRect;
        Texture2D backgroundImg, continueBtn, quitBtn;
        float screenX, screenY;

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            if (font == null)
                font = content.Load<SpriteFont>("Font1");
            backgroundImg = content.Load<Texture2D>("title");
            screenX = ScreenManager.Instance.Dimensions.X;
            screenY = ScreenManager.Instance.Dimensions.Y;
            continueBtn = content.Load<Texture2D>("start");
            quitBtn = content.Load<Texture2D>("quit");
            continueRect = new Rectangle((int)(screenX / 2), (int)(screenY / 2), continueBtn.Bounds.Width, continueBtn.Bounds.Height);
            quitRect = new Rectangle((int)(screenX / 2), (int)(screenY / 2) + 100, continueBtn.Bounds.Width, continueBtn.Bounds.Height);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            mouse = Mouse.GetState();
            
            if (continueRect.Contains(new Point(mouse.X, mouse.Y)) &&
                mouse.LeftButton == ButtonState.Released &&
                oldmouse.LeftButton == ButtonState.Pressed)
            {
                ScreenManager.Instance.AddScreen(new LevelScreen());
            }
            else if (continueRect.Contains(new Point(mouse.X, mouse.Y)) &&
                 mouse.LeftButton == ButtonState.Pressed)
            {
                continueClr = new Color(75, 75, 75);
            }
            else if (continueRect.Contains(new Point(mouse.X, mouse.Y)))
            {
                continueClr = new Color(125, 125, 125);
            }
            else if (continueRect.Contains(new Point(mouse.X, mouse.Y)) != true)
            {
                continueClr = Color.White;
            }

            if (quitRect.Contains(new Point(mouse.X, mouse.Y)) &&
                mouse.LeftButton == ButtonState.Released &&
                oldmouse.LeftButton == ButtonState.Pressed)
            {
                System.Environment.Exit(0);
            }
            else if (quitRect.Contains(new Point(mouse.X, mouse.Y)) &&
                 mouse.LeftButton == ButtonState.Pressed)
            {
                quitClr = new Color(75, 75, 75);
            }
            else if (quitRect.Contains(new Point(mouse.X, mouse.Y)))
            {
                quitClr = new Color(125, 125, 125);
            }
            else if (quitRect.Contains(new Point(mouse.X, mouse.Y)) != true)
            {
                quitClr = Color.White;
            }

            oldkeyState = keyState;
            oldmouse = mouse;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImg, new Rectangle(0, 0, (int)screenX, (int)screenY), Color.White);
            spriteBatch.Draw(continueBtn, continueRect, continueClr);
            spriteBatch.Draw(quitBtn, quitRect, quitClr);
        }
    }
}
