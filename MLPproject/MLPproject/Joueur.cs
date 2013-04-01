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

        int compte_en_banque { get; set; }
        int _ID;
        Vector2 Position;

        Map Map;

        int ID
        {
            get { return ID; }
            set { _ID = value; }
        }

        public Joueur(int id, Map map)
        {
            compte_en_banque = 500;
            this._ID = id;
            this.Map = map;
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
