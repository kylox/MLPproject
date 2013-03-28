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
        Tile[,] plateau_tile = new Tile[32, 32];
        List<Unite> listUnits = new List<Unite>();
        List<Ville> listVilles = new List<Ville>();
        List<Texture2D> listTileTextures = new List<Texture2D>();
        public Map()
        {

        }


        public void Draw(SpriteBatch spritebatch)
        {
            // Affichage de toutes les tiles
            for (int i = 0, c = plateau_tile.GetLength(1); i < c; i++)
                for (int j = 0, d = plateau_tile.GetLength(0); j < d; j++)
                    plateau_tile[j, i].Draw(spritebatch);






        }

    }
}
