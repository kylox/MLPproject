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
    enum Phase_de_jeu
    {
        ravitaillement,
        deplacement,
        attaque
    };

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Joueur J1;
        Joueur J2;
        Phase_de_jeu nouvelle_phase;
        Phase_de_jeu old_phase;
        int joueur;
        int _joueur; //ne pas faire gaffe permet juste de dessiner le numero du joueur
        Unite unite_J1; // Temporairement je créer une unité à la main

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                //PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                //PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height

                // Petite modification pour que ça rentre dans mon écran ^^ (SDanTe)
                PreferredBackBufferWidth = 16 * 32 + 200 + 11 * 12,
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
            J2 = new Joueur(2, map);

            joueur = 0;

            unite_J1 = new Unite(2, new Vector2(32 * 5, 32 * 5), Type_unite.rapide, map);

            nouvelle_phase = Phase_de_jeu.ravitaillement;
            old_phase = Phase_de_jeu.ravitaillement;
        }
        protected override void UnloadContent()
        {

        }
        protected void jeux(Joueur J)//c'est la boucle du jeux suivant les phase de jeux J1 fait tout ses toutrs puis c'est au tour de J2
        {
            switch (nouvelle_phase)
            {
                case Phase_de_jeu.ravitaillement:

                    foreach (Ville ville in J.Villes)
                        J.Argent += 100;

                    if (Data.keyboardState.IsKeyDown(Keys.Space) && Data.prevKeyboardState.IsKeyUp(Keys.Space))
                        nouvelle_phase = Phase_de_jeu.deplacement;


                    break;

                case Phase_de_jeu.deplacement:

                    if (Data.keyboardState.IsKeyDown(Keys.Space) && Data.prevKeyboardState.IsKeyUp(Keys.Space))
                        nouvelle_phase = Phase_de_jeu.attaque;

                    break;

                case Phase_de_jeu.attaque:
                    if (Data.keyboardState.IsKeyDown(Keys.Space) && Data.prevKeyboardState.IsKeyUp(Keys.Space))
                    {
                        nouvelle_phase = Phase_de_jeu.attaque;
                        joueur = (joueur++) % 2;
                    }
                    break;
            }
        }
        protected override void Update(GameTime gameTime)
        {
            Data.Update();
            J1.Update();
            unite_J1.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            #region boucle de jeu
            if (joueur == 0)
                jeux(J1);
            else
                jeux(J2);

            #endregion

            _joueur = joueur++;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(TexturePack.fond, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(TexturePack.page, new Rectangle(80, 50, 16 * 32 + 150, 16 * 32 + 150), Color.FromNonPremultiplied(255, 204, 51, 255));
            spriteBatch.Draw(TexturePack.bordure, new Rectangle(map.Origine.X, map.Origine.Y, 16 * 32, 16 * 32), Color.FromNonPremultiplied(255, 204, 51, 50));
            map.Draw(spriteBatch);
            J1.Draw(spriteBatch);
            spriteBatch.DrawString(TexturePack.font, "c'est le tour de J" + _joueur + " phase de" + nouvelle_phase, new Vector2(100 + 5 * 11 + 30 + 25,0),Color.White);
            spriteBatch.DrawString(TexturePack.font, "press spacebar when you have finish your turn", new Vector2(100 + 5 * 11 + 30 + 25, 725), Color.White);
            

            spriteBatch.End();

            unite_J1.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
