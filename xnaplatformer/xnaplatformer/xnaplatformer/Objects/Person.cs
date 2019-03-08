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
    class Person : Object
    {
        protected new Texture2D[,] img;
        public bool is_falling = true,
            is_moving = true,
            is_shooting = false,
            can_move = true,
            is_alive = true;        
        protected float speed = 400.0f,
        speed_flight = 20.0f,
        speed_sprint = 70.0f,
        depspeed_ground = 35.0f,
        depspeed_falling = 1.0f,
        jump_height = 1000.0f,
        gravity = 40.0f,
        oldDirrection = 1.0f,
        animationDelay = 0.1f,
        time, delay = 0.08f;
        public int health;
        protected Vector2 vel, oldPos;
        protected Rectangle spriteRect;

        public Person() : base()
        {
        }
        
        protected int currentSprite = 0, drawDirection = 1;

        protected void MoveDepreciate(Vector2 vel, GameTime gameTime)
        {
            if (is_falling)
            {
                if (vel.X > 0)
                {
                    if (vel.X - (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_falling > 0)
                        this.vel.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_falling;
                    else
                        this.vel.X = 0;
                }
                else if (vel.X < 0)
                    if (vel.X + (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_falling < 0)
                        this.vel.X += (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_falling;
                    else
                        this.vel.X = 0;
            }
            else
            {
                if (vel.X > 0)
                {
                    if (vel.X - (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_ground > 0)
                        this.vel.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_ground;
                    else
                        this.vel.X = 0;
                }
                else if (vel.X < 0)
                    if (vel.X + (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_ground < 0)
                        this.vel.X += (float)gameTime.ElapsedGameTime.TotalSeconds * depspeed_ground;
                    else
                        this.vel.X = 0;
            }
        }

        protected void Gravity(GameTime gameTime)
        {
            if (is_falling)
                this.vel.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * gravity;
        }

        public void Recoil(GameTime gameTime, Vector2 pos1, Vector2 pos2, Rectangle rect)
        {
            Vector2 direction = new Vector2(pos.X - (pos2.X + (rect.Width / 2)), pos.Y - (pos2.Y + (rect.Height / 2)));
            direction.Normalize();
            vel.X = direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds * jump_height;
            vel.Y = direction.Y * -(float)gameTime.ElapsedGameTime.TotalSeconds * jump_height;
            is_falling = true;
        }

        protected void Animate(GameTime gameTime)
        {
            double mod = (speed / (vel.X / gameTime.ElapsedGameTime.TotalSeconds));
            if (mod < 0)
                mod = mod * -1;
            if (is_moving == true && time >= delay * mod && is_falling == false)
            {
                if (currentSprite < img.GetLength(0)-1)
                {
                    currentSprite++;
                }
                else
                {
                    currentSprite = 0;
                }

                time = 0;
            }
            else if (is_moving == false)
            {
                currentSprite = 0;
            }
            else if (is_falling)
            {
                currentSprite = 2;
            }
        }

        public bool DecrementHealth()
        {
            health--;
            if (health <= 0)
                is_alive = false;
            return is_alive;
        }

        public bool DecrementHealth(int delta)
        {
            health -= delta;
            if (health <= 0)
                is_alive = false;
            return is_alive;
        }

        public bool Is_Alive
        {
            get { return is_alive; }
        }

        public void Damage(int damage)
        {
            health -= damage;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRect.Width = img[currentSprite, drawDirection].Width;
            spriteBatch.Draw(img[currentSprite, drawDirection], spriteRect, Color.White);
        }

        public Vector2 Vel
        {
            get { return vel; }
            set { vel = value; }
        }
    }
}