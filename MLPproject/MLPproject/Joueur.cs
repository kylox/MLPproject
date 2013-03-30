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

        int compte_en_banque {get ; set;}
        int _ID;
        Vector2 Position;

        Map Map;

        int ID
        {
            get{return ID;}
            set{_ID = value;}
        }

        public Joueur(int id,Map map)
        {
            compte_en_banque = 500;
            this._ID = id;
            this.Map = map;
            if (id == 1)
                Position = Vector2.Zero;
        }

        public void Update()
        {
            if (_ID == 1)
            {
                if (Data.keyboardState.IsKeyDown(Keys.Q) && Data.keyboardState != Data.prevKeyboardState && Position.X > 0)
                    Position.X--;
                if (Data.keyboardState.IsKeyDown(Keys.D) && Data.keyboardState != Data.prevKeyboardState && Position.X < Map.mapWidth)
                    Position.X++;
                if (Data.keyboardState.IsKeyDown(Keys.Z) && Data.keyboardState != Data.prevKeyboardState && Position.Y > 0)
                    Position.Y--;
                if (Data.keyboardState.IsKeyDown(Keys.S) && Data.keyboardState != Data.prevKeyboardState && Position.Y < Map.mapHeight)
                    Position.Y++;
            }
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(TexturePack.TilesUnites[0], new Vector2((int)Position.X * 32,(int) Position.Y * 32) , Color.Red);
        }
    }
}
