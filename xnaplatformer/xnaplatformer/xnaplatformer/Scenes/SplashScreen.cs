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
    public class SplashScreen : GameScreen
    {
        KeyboardState keyState, oldkeyState;
        SpriteFont font;
        Vector3 promptColor = new Vector3(0, 0, 255);
        Texture2D backgroundImg;
        float screenX, screenY;
        //int count = 0, waittime;

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            if (font == null)
                font = content.Load<SpriteFont>("Font1");
            backgroundImg = content.Load<Texture2D>("title");
            screenX = ScreenManager.Instance.Dimensions.X;
            screenY = ScreenManager.Instance.Dimensions.Y;
            //waittime = 15; //the amount of time for the splash screen to disapear
           
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            //if (count < waittime)
            //    count += (float)gameTime.TotalGameTime.Seconds;
            //else
            //    ScreenManager.Instance.AddScreen(new TitleScreen());
            if (keyState.IsKeyDown(Keys.Space))
                promptColor = new Vector3(255, 0, 255);
            else if (oldkeyState.IsKeyDown(Keys.Space) && keyState.IsKeyUp(Keys.Space))
                ScreenManager.Instance.AddScreen(new TitleScreen());
            else
                promptColor = new Vector3(0, 0, 255);
            oldkeyState = keyState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImg, new Rectangle(0, 0, (int)screenX, (int)screenY), Color.White);
            Vector2 promptVect = new Vector2(100, 100);
            spriteBatch.DrawString(font, "CSC 281 Team: Best Girl",
                promptVect, Color.White);
            promptVect.Y += font.MeasureString("1").Y;
            promptVect.Y += font.MeasureString("1").Y;
            spriteBatch.DrawString(font, "Code: Cameron, Chris",
                promptVect, Color.White);
            promptVect.Y += font.MeasureString("1").Y;
            spriteBatch.DrawString(font, "Art: Anastasia",
                promptVect, Color.White);
            promptVect.Y += font.MeasureString("1").Y;
            spriteBatch.DrawString(font, "Levels: Kassidy",
                promptVect, Color.White);
            spriteBatch.DrawString(font, "Press 'Space' to continue",
                new Vector2(100, ScreenManager.Instance.Dimensions.Y - 100), new Color(promptColor.X, promptColor.Y, promptColor.Z));

        }
    }
}
                
