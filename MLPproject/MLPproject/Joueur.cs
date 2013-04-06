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
    class Joueur
    {
        public int Argent { get; set; }
        int _ID;

        int ID
        {
            get { return ID; }
            set { _ID = value; }
        }
        public List<Unite> Unites
        {
            get { return list_unite; }
            set { list_unite = value; }
        }
        public List<Ville> Villes
        {
            get { return list_ville; }
            set { list_ville = value; }
        }

        List<Unite> list_unite;
        List<Ville> list_ville;
        Map Map;

        public Joueur(int id, Map map)
        {
            Argent = 500;
            this._ID = id;
            this.Map = map;

            list_unite = new List<Unite>() { new Unite(2, new Vector2(32 * 5, 32 * 5), Type_unite.rapide, map) };


            if (id == 1)
                list_ville = new List<Ville>() { new Ville(1, new Vector2(0, 0), map) };

            else
            {
                list_ville = new List<Ville>() { new Ville(1, new Vector2(15, 15), map) };
               
            }

        }
        public void intersect()
        {


        }
        public void Update(GameTime gametime, Game1 game)
        {
           
                foreach (Unite unit in list_unite)
                    unit.Update(gametime, game);


        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (_ID == 1)
                spritebatch.DrawString(TexturePack.font, "argent : " + Argent, new Vector2(0, 150), Color.White);
            else
                spritebatch.DrawString(TexturePack.font, "argent : " + Argent, new Vector2(725, 150), Color.White);

            foreach (Ville ville in list_ville)
                ville.Draw(spritebatch);

            foreach (Unite unit in list_unite)
                unit.Draw(spritebatch);
        }
    }
}
