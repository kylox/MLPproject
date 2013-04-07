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
        int vitesse;
        int pv;
        bool IsSelected, IsMoved;//la variable is moved permet de savoir si l'unite a etait deplace pendant le tour
        Color selectionColor = Color.Gray;
        Color defaultColor = Color.White;
        Type_unite Type;
        Vector2 Position;
        Texture2D Sprite;
        Map Map;
        Joueur Joueur;
        Game1 Game;
        public bool isSelected
        {
            get { return IsSelected; }
            set { IsSelected = value; }
        }
        public bool isMoved
        {
            get { return IsMoved; }
            set { IsMoved = value; }
        }

        public Unite(Joueur joueur, Vector2 position, Type_unite type, Map map, Game1 game)
        {
            this.Game = game;
            this.Joueur = joueur;
            this.Type = type;
            IsMoved = false;
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
            this.Position.X = map.Origine.X + position.X;
            this.Position.Y = map.Origine.Y + position.Y;
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
            return Map.Origine.X <= destination.X
                                    && destination.X <= (Map.Origine.X + 16 * 32)
                                    && Map.Origine.Y <= destination.Y && destination.Y <= Map.Origine.Y + 16 * 32; ;
        }
        public void Update(GameTime gametime, Game1 game)
        {
            // On va permettre la selection de l'unité
            #region selection + deplacement
            if (game.Phase == Phase_de_jeu.deplacement)
            {
                if ((Data.mouseState.LeftButton == ButtonState.Pressed) && (Data.prevMouseState.LeftButton != ButtonState.Pressed))
                {
                    if (MouseOnTile())
                    {
                        Map.GetTile((int)Position.X - Map.Origine.X, (int)Position.Y - Map.Origine.Y).SetColor(selectionColor);
                        IsSelected = true;
                    }
                    else
                    {
                        Map.GetTile((int)Position.X - Map.Origine.X, (int)Position.Y - Map.Origine.Y).SetColor(defaultColor);
                        IsSelected = false;
                    }
                }
                if ((Data.mouseState.RightButton == ButtonState.Pressed) && (Data.prevMouseState.RightButton != ButtonState.Pressed))
                {
                    if (IsSelected /*&& !IsMoved*/)
                    {
                        bool tupeux = false;
                        int ixe = (int)Math.Abs(Data.mouseState.X - (Data.mouseState.X % 32) - 10 - Position.X);
                        int ygrec = (int)Math.Abs(Data.mouseState.Y - (Data.mouseState.Y % 32) - 8 - Position.Y);
                        switch (Type)
                        {
                            case Type_unite.legere:
                                tupeux = (ixe / 32 <= 2) && (ygrec / 32 <= 2)
                                    && Is_inbounds(new Vector2(Data.mouseState.X, Data.mouseState.Y));
                                break;
                            case Type_unite.rapide:
                                tupeux = (ixe / 32 <= 4) && (ygrec / 32 <= 4)
                                    && Is_inbounds(new Vector2(Data.mouseState.X, Data.mouseState.Y));
                                break;
                            case Type_unite.lourde:
                                tupeux = (ixe / 32 <= 1) && (ygrec / 32 <= 1)
                                   && Is_inbounds(new Vector2(Data.mouseState.X, Data.mouseState.Y));
                                break;
                        }
                        if (tupeux)
                        {
                            Map.GetTile((int)Position.X - Map.Origine.X, (int)Position.Y - Map.Origine.Y).SetColor(defaultColor);
                            Deplacement(TilePos(new Point(Data.mouseState.X, Data.mouseState.Y)));
                            Map.GetTile((int)Position.X - Map.Origine.X, (int)Position.Y - Map.Origine.Y).SetColor(selectionColor);
                            IsMoved = true;
                        }
                    }
                }
            }
            #endregion
        }
        public bool MouseOnTile()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height).Contains(new Point(Data.mouseState.X, Data.mouseState.Y));
        }
        public Point TilePos(Point p)
        {
            int x = (p.X - (p.X % 32)) - 10;
            int y = (p.Y - (p.Y % 32)) - 8;
            return new Point(x, y);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (Joueur.ID == 1)
                spritebatch.Draw(Sprite, Position, Color.Blue);
            else
                spritebatch.Draw(Sprite, Position, Color.Red);

            if (this.IsSelected == true)
            {
                #region draw caracteristique de l'unité selectionné
                if (Joueur.ID == 1)
                    spritebatch.DrawString(TexturePack.font, "attaque : " + this.Attaque + "\ndefense : " + this.Defense + "\nvitesse : " + this.vitesse, new Vector2(10, 500), Color.White);
                if (Joueur.ID == 2)
                    spritebatch.DrawString(TexturePack.font, "attaque : " + this.Attaque + "\ndefense : " + this.Defense + "\nvitesse : " + this.vitesse, new Vector2(725, 500), Color.White);
                #endregion
                #region draw de la surface de mouvement
                if (Game.Phase == Phase_de_jeu.deplacement)
                    switch (Type)
                    {
                        case Type_unite.legere:
                            spritebatch.Draw(TexturePack.pixel, new Rectangle((int)Position.X - 32 * 2, (int)Position.Y - 32 * 2, 160, 160), Color.FromNonPremultiplied(204, 255, 255, 100));
                            break;

                        case Type_unite.rapide:
                            spritebatch.Draw(TexturePack.pixel, new Rectangle((int)Position.X - 32 * 4, (int)Position.Y - 32 * 4, 288, 288), Color.FromNonPremultiplied(204, 255, 255, 100));
                            break;

                        case Type_unite.lourde:
                            spritebatch.Draw(TexturePack.pixel, new Rectangle((int)Position.X - 32, (int)Position.Y - 32, 96, 96), Color.FromNonPremultiplied(204, 255, 255, 100));
                            break;
                    }
                #endregion
            }
        }
    }
}
