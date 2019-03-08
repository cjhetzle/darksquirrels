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
    class Tile
    {
        private static int TILE_SIZE = 50;
        private Point pos;
        private string type = "";
        public bool canWalk;
        public Texture2D tilePic;
        private Point gridPosition;
        public Rectangle tileRect;

        public Tile(Point GridPos, string Type, bool CanWalk, Texture2D TilePic)
        {
            pos = GridPos;
            type = Type;
            canWalk = CanWalk;
            tilePic = TilePic;
            gridPosition = GridPos;
            tileRect = new Rectangle(GridPos.X * TILE_SIZE,
                GridPos.Y * TILE_SIZE,
                TILE_SIZE,
                TILE_SIZE);
        }

        public static int TileSize()
        {
            return TILE_SIZE;
        }

        public int tileSize
        {
            get { return TILE_SIZE; }
            set
            {
                TILE_SIZE = value;
                this.tileRect = new Rectangle(pos.X * TILE_SIZE,
                    pos.Y * TILE_SIZE,
                    TILE_SIZE,
                    TILE_SIZE);

            }
        }
    }
}
