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
        bool IsSelected;

        public Ville(int joueur, Vector2 position, Map map )
        {
            this.Position.X = position.X*32 + map.Origine.X;
            this.Position.Y = position.Y*32 + map.Origine.Y;
        }

        public void Update(Game1 game)
        {



        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(TexturePack.ville, Position, Color.White);
        }

    }
}
