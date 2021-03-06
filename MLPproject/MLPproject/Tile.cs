﻿using System;
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
    public enum Type_tile
    {
        // On initialise l'enum à 0 pour pouvoir l'utiliser dans les tables
        eau = 0,
        roche,
        herbe
    }
    public class Tile
    {
        public Vector2 Position;
        Color Color;
        Texture2D texture;
        public Type_tile Type {get; private set; }
        public Tile(Type_tile type, int x, int y)
        {
            Color = Color.White;
            this.Type = type;
            // On charge la texture adaptée à la tile en fonction de son type
            texture = TexturePack.TilesTexture[(int)type];
            Position = new Vector2(x, y);

        }
        public void SetColor(Color color)
        {
            this.Color = color;
        } // Permet de modifier la couleur de fond de la tile 
        public void Draw(SpriteBatch spritebatch)
        {
           spritebatch.Draw(texture,new Vector2(Position.X,Position.Y), this.Color);
        }
    }
}
