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
        Map Map;
        Color selectionColor = Color.Gray;
        Color defaultColor = Color.White;
        Color texte_selec = Color.Red;
        List<string> type_unite;
        Joueur Joueur;

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
        }
        public bool MouseOnTile()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 32, 32).Contains(new Point(Data.mouseState.X, Data.mouseState.Y));
        }

        private Vector2 position_unité(Vector2 P)
        {


            P = Map.GetTile((int)P.X - Map.Origine.X, (int)P.Y - Map.Origine.Y).Position;
            P.X -= Map.Origine.X;
            P.Y -= Map.Origine.Y;
            return P;
        }
        public void Update(Game1 game)
        {
            int y = 250;
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
                            && new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, y + 10, 100, 5)))
                        {
                            creation = true;
                            break;
                        }
                        y += 50;
                    }
                }

                if (creation && (Data.mouseState.LeftButton == ButtonState.Pressed) && (Data.prevMouseState.LeftButton != ButtonState.Pressed) && Isplayable)//pose l'unité ! 
                {
                    switch (y)
                    {
                        case 250:
                            if (Math.Abs(Data.mouseState.X - Position.X) <= 32*2 && (Math.Abs(Data.mouseState.Y - Position.Y) <= 32*2))
                            {
                                Joueur.Unites.Add(new Unite(Joueur, position_unité(new Vector2(Data.mouseState.X, Data.mouseState.Y)), Type_unite.legere, Map, game));
                                Joueur.Argent -= 100;
                                break;
                            }
                            break;
                        case 300:
                            Joueur.Unites.Add(new Unite(Joueur, position_unité(new Vector2(Data.mouseState.X, Data.mouseState.Y)), Type_unite.rapide, Map, game));
                            Joueur.Argent -= 200;
                            break;
                        case 350:
                            Joueur.Unites.Add(new Unite(Joueur, position_unité(new Vector2(Data.mouseState.X, Data.mouseState.Y)), Type_unite.lourde, Map, game));
                            Joueur.Argent -= 300;
                            break;
                    }
                    Isplayable = false;
                }
                #endregion
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            int y = 250;

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
                    if (new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, y + 10, 100, 5)))
                        spritebatch.DrawString(TexturePack.font, S, new Vector2(0, y), texte_selec);
                    else
                        spritebatch.DrawString(TexturePack.font, S, new Vector2(0, y), defaultColor);
                    y += 50;
                }
            }
            #endregion

        }
    }
}
