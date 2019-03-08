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
    class Player : Person
    {
        private String[,] spriteDirectories;

        public Player(Rectangle rect, String[,] spriteDirectories) : base()
        {
            this.spriteDirectories = spriteDirectories;
            this.img = new Texture2D[spriteDirectories.GetLength(0), spriteDirectories.GetLength(1)];
            this.rect = rect;
            this.spriteRect = rect;      
            this.pos = new Vector2(rect.X, rect.Y);
            this.rect.X = (int)this.pos.X;
            this.rect.Y = (int)this.pos.Y;
            this.health = 3;
        }

        public new void Load(ContentManager Content)
        {
            int i, j;
            for (i = 0; i < img.GetLength(0); i++)
                for (j = 0; j < img.GetLength(1); j++)
                {
                    img[i, j] = Content.Load<Texture2D>(spriteDirectories[i, j]);
                }
        }


        public void Update(GameTime gameTime, KeyboardState keys, KeyboardState oldkeys, Grid grid)
        {
            //Gravity... simple enough
            Gravity(gameTime);
            //Updates the player's input and decides the velocity of the... player?
            //if (is_alive)
                Commands(keys, oldkeys, gameTime);
            //Updates the player's position
            //Position is used to tell whether or not the player is intersecting something before we add it too the player's rect

            //
            pos.X += vel.X;
            pos.Y -= vel.Y;


            //animation
            Animate(gameTime);

            //Move depreciate sub adds friction to the players movement when no keys are being pressed
            MoveDepreciate(vel, gameTime);


            //The biggest pain in the ars
            //The boundary class will keep the player on screen and out of the tiles... hopefully
            Boundary.Bound(ref vel, ref pos, oldPos, rect, ref is_falling, grid);

            // these are useless otherthan for drawing if it is bounded or not
            //is_bounded_sides_left = boundary.Is_bounded_sides_left();
            //is_bounded_sides_right = boundary.Is_bounded_sides_right();
            //is_bounded_tops = boundary.Is_bounded_tops();      

            //Updates the players location of 'drawness'
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            spriteRect.X = rect.X;
            spriteRect.Y = rect.Y;


            //This decides whether or not the player is moving
            if (vel == Vector2.Zero)
                is_moving = false;
            else
                is_moving = true;
            


            //Just like 'oldkeys' this tells us where the player last was
            //Usefull for deciding where the player came from before intersecting a tile
            oldPos = pos;
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void Commands(KeyboardState keys, KeyboardState oldkeys, GameTime gameTime)
        {
            if (keys.IsKeyDown(Keys.Right))
            {
                drawDirection = 1;
                oldDirrection = 1.0f;
                if (is_falling == false)
                {
                    if (vel.X + gameTime.ElapsedGameTime.TotalSeconds * speed_sprint <
                        (float)gameTime.ElapsedGameTime.TotalSeconds * speed)
                    {
                        vel.X += (float)gameTime.ElapsedGameTime.TotalSeconds * speed_sprint;
                    }
                }
                else
                {
                    if (vel.X < (float)gameTime.ElapsedGameTime.TotalSeconds * speed)
                    {
                        vel.X += (float)gameTime.ElapsedGameTime.TotalSeconds * speed_flight;
                    }
                }
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                drawDirection = 0;
                oldDirrection = -1.0f;
                if (is_falling == false)
                {
                    if (vel.X - gameTime.ElapsedGameTime.TotalSeconds * speed_sprint >
                        -(float)gameTime.ElapsedGameTime.TotalSeconds * speed)
                    {
                        vel.X += -(float)gameTime.ElapsedGameTime.TotalSeconds * speed_sprint;
                    }
                    else
                    {
                        vel.X = -(float)gameTime.ElapsedGameTime.TotalSeconds * speed;
                    }
                }
                else
                {
                    if (vel.X > -(float)gameTime.ElapsedGameTime.TotalSeconds * speed)
                    {
                        vel.X += -(float)gameTime.ElapsedGameTime.TotalSeconds * speed_flight;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space) ||
                keys.IsKeyDown(Keys.Up) && oldkeys.IsKeyUp(Keys.Up))
            {
                if (is_falling == false)
                {
                    is_falling = true;
                    vel.Y = (float)gameTime.ElapsedGameTime.TotalSeconds * jump_height;
                }
            }
        }
 
    }
}