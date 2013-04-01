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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Joueur J1;

        // Temporairement je créer une unité à la main
        Unite unite_J1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                //PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                //PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height

                // Petite modification pour que ça rentre dans mon écran ^^ (SDanTe)
                PreferredBackBufferWidth = 16 * 32 + 200 +11*12,
                PreferredBackBufferHeight = 16 * 32 + 300
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }


        protected override void LoadContent()
        {
           

            TexturePack.Load(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new Map();
            J1 = new Joueur(1, map);

            unite_J1 = new Unite(1, new Vector2(32 * 5, 32 * 5), Type_unite.legere, map);
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            Data.Update();
            J1.Update();
            unite_J1.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(TexturePack.fond, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(TexturePack.page, new Rectangle(80, 50, 16 * 32 + 150, 16 * 32 + 150), Color.FromNonPremultiplied(255, 204, 51, 255));
            map.Draw(spriteBatch);
            J1.Draw(spriteBatch);
            spriteBatch.DrawString(TexturePack.font, "press spacebar when you have finish your turn", new Vector2(100+5*11+30, 535), Color.White);
            spriteBatch.Draw(TexturePack.bordure, new Rectangle(100 + 5 * 11, 20, 16 * 32, 16 * 32), Color.FromNonPremultiplied(255, 204, 51, 50));
            
            spriteBatch.End();
        
            unite_J1.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
