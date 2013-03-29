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
    public enum Type_tile
    {
        eau = 0,
        roche = 1,
        herbe = 2
    }


    public class Tile
    {
        Vector2 Position;
        Color Color;
        int Joueur;
        Texture2D texture;
        public Type_tile Type {get; private set; }

        public Tile(Type_tile type)
        {
            Color = Color.White;
            Joueur = 0;
            this.Type = type;

            switch (Type)
            {
                case Type_tile.eau:
                    texture = null; //mettre le tile de l'eau
                    break;
                case Type_tile.herbe:
                    texture = null; //mettre le tile de l'herbe
                    break;
                case Type_tile.roche:
                    texture = null; //mettre le tile de la roche
                    break;
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(texture,Position,Color.White);
            spritebatch.End();
        }



    }
}
