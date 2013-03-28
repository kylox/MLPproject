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
        legere,
        rapide,
        lourde
    };

    class Unite
    {
        int Attaque;
        int Defense;
        int Joueur;
       
        Type_unite Type;
        Vector2 Position;
        Texture2D Sprite;

        public Unite(int joueur, int attaque, int defense, Texture2D sprite, Vector2 position, Type_unite type)
        {
            this.Attaque = attaque;
            this.Defense = defense;
            this.Joueur = joueur;
            this.Position = position;
            this.Sprite = sprite;
            this.Type = type;
        }

        public void Deplacement(int x, int y)
        {

            Position.X = x;
            Position.Y = y;
        }

        public bool Is_inbounds()//destination in bounds (a la souris)
        {
            switch (Type)
            {
                case Type_unite.legere:
                    //a completer
                    break;

                case Type_unite.rapide:
                    //a completer
                    break;
                case Type_unite.lourde:
                    //a completer
                    break;



            }


            return false;
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
