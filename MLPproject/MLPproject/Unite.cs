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
        int pv;

        bool IsSelected;

        Color selectionColor = Color.Gray;
        Color defaultColor = Color.White;

        Type_unite Type;
        Vector2 Position;
        Texture2D Sprite;
        Map Map;

        public Unite(int joueur, Vector2 position, Type_unite type, Map map)
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
                    Sprite = TexturePack.TilesUnites[1]; //mettre texutre
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
            this.Map = map;
            this.IsSelected = false;
        }

        public void Deplacement(Point p)
        {
            Position.X = p.X;
            Position.Y = p.Y;
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
            // On va permettre la selection de l'unité
            if ((Data.mouseState.LeftButton == ButtonState.Pressed) && (Data.prevMouseState.LeftButton != ButtonState.Pressed))
            {
                if (MouseOnTile())
                {
                    Map.GetTile((int)Position.X, (int)Position.Y).SetColor(selectionColor);
                    IsSelected = true;
                }
                else
                {
                    Map.GetTile((int)Position.X, (int)Position.Y).SetColor(defaultColor);
                    IsSelected = false;
                }
            }

            if ((Data.mouseState.RightButton == ButtonState.Pressed) && (Data.prevMouseState.RightButton != ButtonState.Pressed))
            {
                if (IsSelected)
                {
                    Map.GetTile((int)Position.X, (int)Position.Y).SetColor(defaultColor);
                    Deplacement(TilePos(new Point(Data.mouseState.X, Data.mouseState.Y)));
                    Map.GetTile((int)Position.X, (int)Position.Y).SetColor(selectionColor);
                }
            }
        }

        public bool MouseOnTile()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height).Contains(new Point(Data.mouseState.X, Data.mouseState.Y));
        }

        public Point TilePos(Point p)
        {
            int x = (p.X - (p.X % Sprite.Width));
            int y = (p.Y - (p.Y % Sprite.Height));

            return new Point(x, y);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(Sprite, Position, Color.White);
            spritebatch.End();
        }

    }
}
