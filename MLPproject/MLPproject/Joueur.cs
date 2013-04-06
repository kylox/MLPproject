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
        Vector2 Position;
        Map Map;

        public Joueur(int id, Map map)
        {
            Argent = 500;
            this._ID = id;
            this.Map = map;
            list_unite = new List<Unite>();
            list_ville = new List<Ville>();
        }
        public void intersect()
        {


        }
        public void Update()
        {



        }
        public void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
