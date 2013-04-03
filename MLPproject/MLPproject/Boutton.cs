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
    class Boutton
    {

        Vector2 Position;
        Rectangle Container;
        List<string> Menu;

        public Boutton(List<string> menu, Vector2 position)
        {
            this.Position = position;
            this.Menu = menu;
        }

        public bool Is_selected()
        {
            return Container.Intersects(new Rectangle(Data.mouseState.X, Data.mouseState.Y, 1, 1))
                && (Data.mouseState.LeftButton == ButtonState.Released) && (Data.prevMouseState.LeftButton == ButtonState.Pressed);
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            int y = 0;
            
            foreach (string s in Menu)
            {
                spritebatch.DrawString(TexturePack.font, s, new Vector2(Position.X,Position.Y + y), Color.White);
                y += 30;
            }

            spritebatch.End();
        }

    }
}
