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
    class Ville
    {
        Vector2 Position;
        bool IsSelected, Isplayable, creation;
        public bool isSelected
        {
            get { return IsSelected; }
            set { IsSelected = value; }
        }
        public bool isPlayable
        {
            get { return Isplayable; }
            set { Isplayable = value; }
        }
        public bool Creation
        {
            get { return creation; }
            set { creation = value; }
        }
        Map Map;
        Color selectionColor = Color.Gray;
        Color defaultColor = Color.White;
        Color texte_selec = Color.Red;
        List<string> type_unite;
        Joueur Joueur;
        int y;
        public Ville(Joueur joueur, Vector2 position, Map map)
        {
            type_unite = new List<string>() { "unite legere", "unite rapide", "unite lourde" };
            this.Position.X = position.X * 32 + map.Origine.X;
            this.Position.Y = position.Y * 32 + map.Origine.Y;
            IsSelected = false;
            Isplayable = true;
            creation = false;
            this.Map = map;
            this.Joueur = joueur;
            y = 250;
        }
        public bool MouseOnTile()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 32, 32).Contains(new Point(Data.mouseState.X, Data.mouseState.Y));
        }
        private Vector2 position_unite(Vector2 P)
        {
            P = Map.GetTile((int)P.X - Map.Origine.X, (int)P.Y - Map.Origine.Y).Position;
            P.X -= Map.Origine.X;
            P.Y -= Map.Origine.Y;
            return P;
        }
        public void Update(Game1 game)
        {
            
            if (game.Phase == Phase_de_jeu.ravitaillement)
            {
                #region selection de la ville
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
                #endregion
                #region creation nouvelle unité
                if (IsSelected && Isplayable) //permet de passer en mode posage d'unité 
                {
                    foreach (string S in type_unite)
                    {
                        if ((Data.mouseState.RightButton == ButtonState.Pressed) && (Data.prevMouseState.RightButton != ButtonState.Pressed)
                            && new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, 250 + 10, 100, 5)))
                        {
                            creation = true;
                            y = 250;
                        }
                        if ((Data.mouseState.RightButton == ButtonState.Pressed) && (Data.prevMouseState.RightButton != ButtonState.Pressed)
                            && new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, 300 + 10, 100, 5)))
                        {
                            creation = true;
                            y = 300;

                        }
                        if ((Data.mouseState.RightButton == ButtonState.Pressed) && (Data.prevMouseState.RightButton != ButtonState.Pressed)
                            && new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, 350 + 10, 100, 5)))
                        {
                            creation = true;
                            y = 350;
                        }
                    }
                }

                if (creation && (Data.mouseState.LeftButton == ButtonState.Pressed) && (Data.prevMouseState.LeftButton != ButtonState.Pressed) && Isplayable)//pose l'unité ! 
                {
                    switch (y)
                    {
                        case 250:

                            if (Math.Abs(Data.mouseState.X - Position.X) <= 32 * 2 && (Math.Abs(Data.mouseState.Y - Position.Y) <= 32 * 2))
                            {
                                Joueur.Unites.Add(new Unite(Joueur, position_unite(new Vector2(Data.mouseState.X, Data.mouseState.Y)), Type_unite.legere, Map, game));
                                Joueur.Argent -= 100;
                                break;
                            }
                            break;
                        case 300:
                            if (Math.Abs(Data.mouseState.X - Position.X) <= 32 * 2 && (Math.Abs(Data.mouseState.Y - Position.Y) <= 32 * 2))
                            {
                                Joueur.Unites.Add(new Unite(Joueur, position_unite(new Vector2(Data.mouseState.X, Data.mouseState.Y)), Type_unite.rapide, Map, game));
                                Joueur.Argent -= 200;
                                break;
                            }
                            break;
                        case 350:
                            if (Math.Abs(Data.mouseState.X - Position.X) <= 32 * 2 && (Math.Abs(Data.mouseState.Y - Position.Y) <= 32 * 2))
                            {
                                Joueur.Unites.Add(new Unite(Joueur, position_unite(new Vector2(Data.mouseState.X, Data.mouseState.Y)), Type_unite.lourde, Map, game));
                                Joueur.Argent -= 300;
                                break;
                            }
                            break;
                    }
                    Isplayable = false;
                }

                #endregion
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            int y_1 = 250;

            #region dessine la ville de la bonne couleur
            if (Joueur.ID == 1)
                spritebatch.Draw(TexturePack.ville, Position, Color.Blue);
            else
                spritebatch.Draw(TexturePack.ville, Position, Color.Red);
            #endregion
            #region dessine des boutons de creation d'unité
            if (IsSelected && Isplayable)
            {
                foreach (string S in type_unite)
                {
                    if (new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, y_1 + 10, 100, 5)))
                        spritebatch.DrawString(TexturePack.font, S, new Vector2(0, y_1), texte_selec);
                    else
                        spritebatch.DrawString(TexturePack.font, S, new Vector2(0, y_1), defaultColor);
                    y_1 += 50;
                }
            }
            #endregion

        }
    }
}
