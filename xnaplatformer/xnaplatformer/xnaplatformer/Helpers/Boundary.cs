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
    class Boundary
    {
        static Boolean is_bounded_sides_left;
        static Boolean is_bounded_sides_right;
        static Boolean is_bounded_tops;
        static int screenWidth, screenHeight;
        public static Vector2 Bound(ref Vector2 Vel, ref Vector2 Pos, Vector2 oldPos, Rectangle Rect, ref bool is_falling, Grid grid)
        {
            Boolean shouldbe_falling = true;
            is_bounded_sides_left = false;
            is_bounded_sides_right = false;
            is_bounded_tops = false;

            if (Pos.Y + Rect.Height > screenHeight)
            {
                Vel.Y = 0;
                shouldbe_falling = false;
                Pos.Y = screenHeight - Rect.Height;
            }
            else if (Pos.Y + Rect.Height == screenHeight)
            {
                shouldbe_falling = false;
            }

            if (Pos.X + Rect.Width > screenWidth)
            {
                Vel.X = 0;
                Pos.X = screenWidth - Rect.Width;
            }
            else if (Pos.X < 0)
            {
                Vel.X = 0;
                Pos.X = 0;
            }




            for (int y = 0; y < grid.height; y++)
            {
                for (int x = 0; x < grid.width; x++)
                {
                    Tile tile = grid.GetTile(new Point(x, y));
                    float distance = Vector2.Distance(new Vector2(tile.tileRect.X, tile.tileRect.Y), Pos);
                    if (distance < 150)
                    {
                        if (tile.canWalk == false)
                        {



                            if (Pos.X >= tile.tileRect.X && Pos.X < tile.tileRect.X + tile.tileRect.Width ||
                               Pos.X + Rect.Width > tile.tileRect.X && Pos.X + Rect.Width <= tile.tileRect.X + tile.tileRect.Width ||
                               Pos.X < tile.tileRect.X && Pos.X + Rect.Width > tile.tileRect.X + tile.tileRect.Width)
                            {
                                //if Rect is in the same column
                                if (Pos.Y + Rect.Height >= tile.tileRect.Y && oldPos.Y + Rect.Height <= tile.tileRect.Y)
                                {
                                    //if Rect WAS above and now inside of
                                    Vel.Y = 0;
                                    is_falling = false;
                                    Pos.Y = tile.tileRect.Y - Rect.Height;
                                    is_bounded_tops = true;
                                }
                                if (Pos.Y <= tile.tileRect.Y + tile.tileRect.Height && oldPos.Y >= tile.tileRect.Y + tile.tileRect.Height)
                                {
                                    //if Rect WAS below and now inside of
                                    Vel.Y = 0;
                                    Pos.Y = tile.tileRect.Y + tile.tileRect.Height;
                                    is_bounded_tops = true;
                                }
                                if (Pos.Y + Rect.Height == tile.tileRect.Y)
                                    shouldbe_falling = false;
                            }



                            if (Pos.Y >= tile.tileRect.Y && Pos.Y < tile.tileRect.Y + tile.tileRect.Height ||
                                    Pos.Y + Rect.Height > tile.tileRect.Y && Pos.Y + Rect.Height <= tile.tileRect.Y + tile.tileRect.Height ||
                                    Pos.Y < tile.tileRect.Y && Pos.Y + Rect.Height > tile.tileRect.Y + tile.tileRect.Height)
                            {
                                if (oldPos.Y + Rect.Height >= tile.tileRect.Y)
                                {
                                    if (Pos.X + Rect.Width >= tile.tileRect.X && Pos.X < tile.tileRect.X)
                                    {
                                        //if Rect WAS to the left of
                                        Vel.X = 0;
                                        Pos.X = tile.tileRect.X - Rect.Width;
                                        is_bounded_sides_right = true;
                                    }
                                    if (Pos.X <= tile.tileRect.X + tile.tileRect.Width && Pos.X + Rect.Width > tile.tileRect.X + tile.tileRect.Width)// && oldPos.X >= tile.tileRect.X + tile.tileRect.Width)
                                    {
                                        //if Rect WAS to the right of
                                        Vel.X = 0;
                                        Pos.X = tile.tileRect.X + tile.tileRect.Width;
                                        is_bounded_sides_left = true;
                                    }
                                }
                            }
                        }
                        //end of boundary

                    }
                }
            }
            is_falling = shouldbe_falling;
            return Pos;
        }

        public static bool Is_bounded_tops()
        {
            return is_bounded_tops;
        }
        public static bool Is_bounded_sides_left()
        {
            return is_bounded_sides_left;
        }
        public static bool Is_bounded_sides_right()
        {
            return is_bounded_sides_right;
        }
        public static void SetWidth(int width)
        {
            screenWidth = width;
        }
        public static void SetHeight(int height)
        {
            screenHeight = height;
        }
    }

}
