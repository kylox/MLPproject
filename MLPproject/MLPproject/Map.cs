using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MLPproject
{
    class Map
    {
        public readonly int mapWidth = 32, //tu triches pierre :)
                            mapHeight = 32,
                            tileWidth = TexturePack.TilesTexture[0].Width,
                            tileHeight = TexturePack.TilesTexture[0].Height;
        Point origine = new Point(0, 0);
        Tile[,] plateau_tile;
        public List<Unite> listUnits = new List<Unite>();
        public List<Ville> listVilles = new List<Ville>();
        List<Texture2D> listTileTextures = new List<Texture2D>();
       
        public Map()
        {
            plateau_tile = new Tile[mapWidth, mapHeight];
            // Generation des tiles avec des textures aleatoires
            Random r = new Random();
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    plateau_tile[i, j] = new Tile((Type_tile)r.Next(3), origine.X + i * tileWidth, origine.Y + j * tileHeight);

        }       
        public void Draw(SpriteBatch spritebatch)
        {
            // Affichage de toutes les tiles
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                    plateau_tile[j, i].Draw(spritebatch);




            

        }
        public Point MouseToMap()
        {
            return ScreenToMap(new Point(Data.mouseState.X, Data.mouseState.Y));
        }
        public Point ScreenToMap(Point p)
        {
            p.X = (p.X - origine.X) / tileWidth;
            p.Y -= (p.Y - origine.Y) / tileHeight;
            return p;
        }
        public Point MapToScreen(Point p)
        {
            p.X = origine.X + p.X * tileWidth;
            p.Y = origine.Y + p.Y * tileHeight;
            return p;
        }
    }
}
