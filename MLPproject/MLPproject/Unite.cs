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
    enum Type_unite
    {
        legere = 0,
        rapide,
        lourde
    };

    class Unite
    {
        int Attaque;
        int Defense;
        int Joueur;
        int vitesse;
        Type_unite Type;
        Vector2 Position;
        Texture2D Sprite;

        public Unite(int joueur, Vector2 position, Type_unite type)
        {
            switch (Type)
            {
                case Type_unite.legere:
                    Attaque = 2;
                    Defense = 2;
                    vitesse = 2;
                    Sprite = TexturePack.TilesUnites[0]; // mettre la texure 
                    break;
                case Type_unite.rapide:
                    Attaque = 1;
                    Defense = 1;
                    vitesse = 4;
                    Sprite = TexturePack.TilesUnites[1];//mettre texutre
                    break;
                case Type_unite.lourde:
                    Attaque = 3;
                    Defense = 3;
                    vitesse = 1;
                    Sprite = TexturePack.TilesUnites[2]; // mettre texture
                    break;
            }
            this.Joueur = joueur;
            this.Position = position;
            this.Type = type;
        }

        public void Deplacement(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public bool Is_inbounds(Vector2 destination)//destination in bounds (a la souris)
        {
            switch (Type)
            {
                case Type_unite.legere:
                    if (Math.Abs(destination.X - Position.X) < 2 || Math.Abs(destination.X - Position.X) < 2)
                        return false;
                    break;

                case Type_unite.rapide:
                    if (Math.Abs(destination.X - Position.X) < 4 || Math.Abs(destination.X - Position.X) < 4)
                        return false;
                    break;
                case Type_unite.lourde:
                    if (Math.Abs(destination.X - Position.X) < 1 || Math.Abs(destination.X - Position.X) < 1)
                        return false;
                    break;

            }

            return true;
        }


        public void Update(GameTime gametime)
        {



        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(Sprite, Position, Color.White);
            spritebatch.End();

        }

    }
}
