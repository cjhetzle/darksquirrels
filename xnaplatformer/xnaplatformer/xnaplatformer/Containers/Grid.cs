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
    class Grid
    {
        private bool bounded_left, bounded_right, auto_scroll;
        public Texture2D[] brickTile = new Texture2D[7];
        public Texture2D airTile;
        int[,] world;
        public List<Tile> tiles;
        public int width, height, auto_speed;
        public int maxX, minX;

        public Grid(int width, int height) //Outlines the properties of the map, max width, height, etc
        {
            Initialize(width, height);
        }

        public Grid(int width, int height, bool auto_scroll)
        {
            this.auto_scroll    = auto_scroll;
            this.auto_speed     = 1;
            Initialize(width, height);
        }

        public Grid(int width, int height, int[,] world, bool auto_scroll, int auto_speed) {
            this.world = world;
            this.auto_scroll    = auto_scroll;
            this.auto_speed     = auto_speed;
            Initialize(width, height);
        }

        private void Initialize(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new List<Tile>();
            maxX = 800;
            minX = 200;
        }

        public void Load()
        {
            for (int y = 0; y < height; y++) //Reads the map layout and creates a list of tiles in order of the map
            {
                for (int x = 0; x < width; x++)
                {
                    int value = world[y,x];
                    if (value == 0)
                    {
                        //air
                        tiles.Add(new Tile(new Point(x, y), "Air", true, airTile));
                    }
                    else if (value >= 1 && value <= 5)
                    {
                        //block
                        tiles.Add(new Tile(new Point(x, y), "Brick", false, brickTile[value - 1]));
                    }
                    else
                    {
                        tiles.Add(new Tile(new Point(x, y), "Grass", true, brickTile[value - 1]));
                    }
                }
            }
        }

        public Tile GetTile(Point position)
        {
            //return tile instance
            return tiles[position.Y * width + position.X];
        }


        public Vector2 Scroll(Vector2 playerVelocity, Vector2 playerPos, Rectangle playerRect, int ScreenWidth)
        {
            Bounded_Left();
            Bounded_Right(ScreenWidth);

            if (!auto_scroll)
            {
                #region For manual scrolling
                if (playerVelocity.X > 0)
                {
                    if (bounded_right == false)
                    {
                        // If the addition of the player velocity will not cause the camera to go off map
                        if (tiles[width - 1].tileRect.X + Tile.TileSize() + playerVelocity.X >= ScreenWidth)
                        {
                            if (playerPos.X + playerRect.Width >= maxX)
                            {
                                // shift all of the enemies and objects over to the left with the screen

                                ShiftTiles((int)playerVelocity.X);
                                playerPos.X = maxX - playerRect.Width;
                            }
                        }
                        else
                        {
                            int difference = tiles[width - 1].tileRect.X + Tile.TileSize() - ScreenWidth;

                            // same same, but by different amount
                            bounded_right = true;
                            ShiftTiles(difference);

                        }
                    }
                }
                else if (playerVelocity.X < 0)
                {
                    if (bounded_left == false)
                    {
                        if (tiles[0].tileRect.X - playerVelocity.X <= 0)
                        {
                            if (playerPos.X <= minX)
                            {
                                // to the left to the left

                                ShiftTiles((int)playerVelocity.X);
                                playerPos.X = minX;
                            }
                        }
                        else
                        {
                            int difference = tiles[0].tileRect.X;

                            // to the left by a different amount to the left by a different amount (cha cha)
                            bounded_left = true;
                            ShiftTiles(difference);
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region For auto scrolling
                if (tiles[width - 1].tileRect.X + Tile.TileSize() - auto_speed >= ScreenWidth)
                    playerPos = Scroll(playerPos);
                else
                {
                    // when it gets to the end of the level it will only
                    //move enough to show the end of the level and nothing off screen
                    int difference = tiles[width - 1].tileRect.X + Tile.TileSize() - ScreenWidth;
                    playerPos = Scroll(playerPos, difference);
                }
                #endregion
            }

            return playerPos;
        }

        private Vector2 Scroll(Vector2 pos)
        {
            pos.X -= auto_speed;
            ShiftTiles(auto_speed);
            return pos;
        }

        private Vector2 Scroll(Vector2 pos, int scroll_amount)
        {
            pos.X -= scroll_amount;
            ShiftTiles(scroll_amount);
            return pos;
        }

        public void Scroll(ref Vector2[] poss)
        {
            int i = 0, l = poss.Length;
            for (; i < l; ++i)
            {
                poss[i].X -= auto_speed;
            }
        }

        public void ScrollAcorns(ref Acorn acorn, int ScreenWidth)
        {
            if (tiles[width - 1].tileRect.X + Tile.TileSize() - auto_speed >= ScreenWidth)
                acorn.scroll(auto_speed);
        }

        public void ScrollWinBox(ref WinBox winBox)
        {
            winBox.Pos = new Vector2(winBox.Pos.X - auto_speed, winBox.Pos.Y);
        }

        private void ShiftTiles(int delta)
        {
            int i = 0, l = tiles.Count;
            for (; i < l; ++i)
            {
                tiles[i].tileRect.X -= delta;
            }
        }

        public Boolean Bounded_Left()
        {
            if (tiles[0].tileRect.X >= 0)
            {
                bounded_left = true;
            }
            else
                bounded_left = false;

            return bounded_left;
        }

        private Boolean Bounded_Right(int ScreenWidth)
        {
            if (tiles[width - 1].tileRect.X + tiles[width - 1].tileRect.Width <= ScreenWidth)
            {
                bounded_right = true;
            }
            else
            {
                bounded_right = false;
            }

            return bounded_right;
        }

        public Boolean Bounded_Right()
        {
            return bounded_right;
        }

        public Boolean Auto_Scroll()
        {
            return auto_scroll;
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile tile = GetTile(new Point(x, y));
                    spriteBatch.Draw(tile.tilePic, tile.tileRect, Color.White);
                }
            }
        }
    }
}
