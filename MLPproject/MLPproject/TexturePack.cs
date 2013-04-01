using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace MLPproject
{
    public static class TexturePack
    {
        public static List<Texture2D> TilesTexture { get; private set; }
        public static List<Texture2D> TilesUnites { get; private set; }

        public static Texture2D pixel,fond,bordure,page;
        public static SpriteFont font;
        public static void Load(ContentManager content)
        {
            pixel = content.Load<Texture2D>("pixel");
            fond = content.Load<Texture2D>("fond");
            bordure = content.Load<Texture2D>("bordure");
            page = content.Load<Texture2D>("page1");

            font = content.Load<SpriteFont>("spritefont");
            TilesTexture = new List<Texture2D>
            {
                content.Load<Texture2D>("EAU"),
                content.Load<Texture2D>("ROCHER"),
                content.Load<Texture2D>("HERBE"),
               
            };

            TilesUnites = new List<Texture2D>
            {
                content.Load<Texture2D>("leger"),
                content.Load<Texture2D>("soutien"),
                content.Load<Texture2D>("lourd")
            };
        }
    }
}
