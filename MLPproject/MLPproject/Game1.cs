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

        Boutton selection_unité_J1;
        Boutton selection_ville_J1;
        Boutton selection_rien_J1;

        Boutton selection_unité_J2;
        Boutton selection_ville_J2;
        Boutton selection_rien_J2;

        List<Unite> unité_J1;
        List<Unite> unité_J2;

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
                PreferredBackBufferHeight = 16 * 32 + 50
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
            selection_ville_J1 = new Boutton(new List<string> { "normal pony", "speedy pony", "heavy pony" }, new Vector2(0, 50));
            selection_rien_J1 = new Boutton(new List<string> { "construire ville" }, new Vector2(0, 50));

            selection_ville_J2 = new Boutton(new List<string> { "normal pony", "speedy pony", "heavy pony" }, new Vector2(612+5*11, 50));
            selection_rien_J2 = new Boutton(new List<string> { "construire ville" }, new Vector2(612+5*11, 50));


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
            map.Draw(spriteBatch);
            J1.Draw(spriteBatch);
            spriteBatch.DrawString(TexturePack.font, "press spacebar when you have finish your turn", new Vector2(100+5*11, 515), Color.White);
            spriteBatch.End();
            selection_rien_J1.draw(spriteBatch);
            selection_rien_J2.draw(spriteBatch);
            unite_J1.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
