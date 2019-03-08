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
    class LevelScreen : GameScreen
    {
        KeyboardState keys, oldkeys;
        SpriteFont font;
        Grid grid;
        bool intersecting = false;
        //Vector3 promptColor = new Vector3(0, 0, 255),
        //    playColor = new Vector3(0, 255, 0);
        MouseState mouse, oldmouse;
        Player player;
        WinterWall wall;
        WinBox winBox;
        Popups popups;
        EnemyManager foxes;        
        Acorn[] acorns;

        private void Initialize()
        {
            int[,] level1 = new int[,]
            {
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,4,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,5,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,0,0,0,3},
                {3,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,6,0,7,0,0,0,1,2,4,5,0,0,0,0,0,0,0,0,0,6,0,0,0,6,0,0,0,0,0,0,1,2,4,5,0,0,7,0,0,0,0,0,0,0,0,0,0,0,3},
                {3,2,4,4,2,4,4,3,3,3,3,2,4,4,2,2,2,2,2,4,4,4,2,2,2,2,4,4,2,4,4,3,3,3,3,2,4,4,2,2,2,2,2,4,4,4,2,2,2,3},
                {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
            };

            grid = new Grid(level1.GetLength(1), level1.GetLength(0), level1, true, 3);

            String[,] playerImages = new String[,] {
                {"Left/walk1", "Right/walk1"},
                {"Left/walk2", "Right/walk2"},
                {"Left/walk3", "Right/walk3"}
            };
            player  = new Player(new Rectangle(Dimensions.X / 2, -100, 50, 92), playerImages);
            acorns = Acorn.spawn(new int[,] { { 10, 8 }, { 20, 8 }, { 32, 3 }, { 42, 8 } });
            foxes = new EnemyManager(new Point(Tile.TileSize(),Tile.TileSize()));
            foxes.Add(new Point( 9, 8));
            foxes.Add(new Point(19, 9));
            foxes.Add(new Point(31, 8));
            foxes.Add(new Point(41, 8));
            wall    = new WinterWall(50, Dimensions.Y);
            winBox = new WinBox(new Point((int)((grid.width-2)*Tile.TileSize()),0), new Point(50, dimensions.Y));
            popups = new Popups();
        }

        public override void LoadContent(ContentManager Content)
        {
            Initialize();
            base.LoadContent(Content);
            if (font == null)
                font = content.Load<SpriteFont>("Font1");



            player  .Load(Content);
            wall    .Load(Content);
            popups  .Load(font);
            foxes   .Load(Content);
            foreach (Acorn acorn in acorns)
            {
                if (acorn != null)
                {
                    acorn.Load(Content);
                }
            }

            

            grid.airTile        = Content.Load<Texture2D>("transparent");
            grid.brickTile[0]   = Content.Load<Texture2D>("Tiles/Tile1");
            grid.brickTile[1]   = Content.Load<Texture2D>("Tiles/Tile2");
            grid.brickTile[2]   = Content.Load<Texture2D>("Tiles/Tile3");
            grid.brickTile[3]   = Content.Load<Texture2D>("Tiles/Tile4");
            grid.brickTile[4]   = Content.Load<Texture2D>("Tiles/Tile5");
            grid.brickTile[5]   = Content.Load<Texture2D>("Tiles/grass");
            grid.brickTile[6]   = Content.Load<Texture2D>("Tiles/tall grass");

            grid.Load();
        }

        public override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState(); mouse = Mouse.GetState();

            if (player.Is_Alive)
            {
                player.Pos = grid.Scroll(player.Vel, player.Pos, player.Rect, dimensions.X);
                            wall.Update(gameTime);
                            grid.ScrollWinBox(ref winBox);
                            if (!grid.Bounded_Right())
                            {
                                winBox.Update(gameTime);
                                foxes.Scroll(grid.auto_speed);
                            }
            }
            
            player.Update(gameTime, keys, oldkeys, grid);
            if (grid.Bounded_Right())
                wall.IncreasingWidth = true;

            
            popups.Update(gameTime);
            foxes.Update(gameTime);

            if (wall.Rect.Intersects(player.Rect))
            {
                // player got hit
                player.DecrementHealth(100);
                popups.Insert(player.Pos, "WINTER HAS COME");
                // oof
                player.Vel = new Vector2(10, player.Vel.Y);
            }
            else if (winBox.Rect.Intersects(player.Rect))
            {
                int needed = acorns.Length;
                foreach (Acorn acorn in acorns)
                {
                    if (acorn == null)
                    {
                        needed--;
                    }
                }
                if (needed <= 0)
                {
                    ScreenManager.Instance.AddScreen(new WinScreen(2));
                }
                else
                {
                    if (popups.getNumber() == 0)
                    {
                        popups.Insert(player.Pos, "Need " + needed + " more acorns");
                    }
                }
            }
            else if (foxes.Intersects(player.Rect))
            {
                if (!intersecting)
                {
                    player.DecrementHealth(1);
                    popups.Insert(player.Pos, "Health -1");
                    player.Vel = new Vector2(-player.Vel.X * 3, -player.Vel.Y * 3);
                }
                intersecting = true;
            } else {
                intersecting = false;
            }

            for (int i = 0; i < acorns.Length; i++)
            {
                Acorn acorn = acorns[i];
                if (acorn != null)
                {
                    grid.ScrollAcorns(ref acorn, dimensions.X);
                    if (acorn.Rect.Intersects(player.Rect))
                    {
                        popups.Insert(player.Pos, "Gimme dat");
                        acorns[i] = null;
                    }
                }
            }

            if (!player.Is_Alive)
                ScreenManager.Instance.AddScreen(new DeathScreen());

            oldkeys = keys; oldmouse = mouse; base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);

            player.Draw(spriteBatch);

            popups.Draw(spriteBatch);

            foreach (Acorn acorn in acorns)
            {
                if (acorn != null)
                {
                    acorn.Draw(spriteBatch);
                }
            }

            foxes.Draw(spriteBatch);

            wall.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

    }
}
