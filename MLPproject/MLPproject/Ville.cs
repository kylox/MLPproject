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
        bool IsSelected, Isplayable;
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
            this.Map = map;
            this.Joueur = joueur;
        }
        public bool MouseOnTile()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 32, 32).Contains(new Point(Data.mouseState.X, Data.mouseState.Y));
        }
        public void Update(Game1 game)
        {
            int y = 250;
            if (game.Phase == Phase_de_jeu.ravitaillement)
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


                if (IsSelected && Isplayable)
                {
                    foreach (string S in type_unite)
                    {
                        if ((Data.mouseState.LeftButton == ButtonState.Pressed) && (Data.prevMouseState.LeftButton != ButtonState.Pressed) &&
                            new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1).Intersects(new Rectangle(0, y + 10, 100, 5)))
                        {
                            switch (y)
                            {
                                case 250:
                                    Joueur.Unites.Add(new Unite(Joueur, new Vector2(this.Position.X + 32, this.Position.Y), Type_unite.legere, Map, game));
                                    break;
                                case 300:
                                    Joueur.Unites.Add(new Unite(Joueur, new Vector2(this.Position.X + 32, this.Position.Y), Type_unite.rapide, Map, game));
                                    break;
                                case 350:
                                    Joueur.Unites.Add(new Unite(Joueur, new Vector2(this.Position.X + 32, this.Position.Y), Type_unite.lourde, Map, game));
                                    break;
                            }
                        }
                        y += 50;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            int y = 250;
            if (Joueur.ID == 1)
                spritebatch.Draw(TexturePack.ville, Position, Color.Blue);
            else
                spritebatch.Draw(TexturePack.ville, Position, Color.Red);
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

        }
    }
}
