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
   public enum Phase_de_jeu
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
        public Phase_de_jeu Phase
        {
            get { return nouvelle_phase; }
            set { nouvelle_phase = value; }
        }
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

           

            nouvelle_phase = Phase_de_jeu.ravitaillement;
            old_phase = Phase_de_jeu.ravitaillement;
        }
        protected override void UnloadContent()
        {

        }
        void jeux(Joueur J)//c'est la boucle du jeux suivant les phase de jeux J1 fait tout ses toutrs puis c'est au tour de J2
        {
            switch (nouvelle_phase)
            {
                #region ravitaillement
                case Phase_de_jeu.ravitaillement:
                    
                    if (old_phase != nouvelle_phase)
                        foreach (Ville ville in J.Villes)
                            J.Argent += 100;

                    if (Data.keyboardState.IsKeyDown(Keys.Space) && Data.prevKeyboardState.IsKeyUp(Keys.Space))//change le tour !
                        nouvelle_phase = Phase_de_jeu.deplacement;

                    old_phase = nouvelle_phase;

                    break;
                #endregion
                #region deplacement
                case Phase_de_jeu.deplacement:

                    if (Data.keyboardState.IsKeyDown(Keys.Space) && Data.prevKeyboardState.IsKeyUp(Keys.Space))//change le tour
                    {
                        nouvelle_phase = Phase_de_jeu.attaque;
                        foreach (Unite unite in J1.Unites)
                        {
                            unite.isMoved = false;//les unite pourront etre rebouger au prochain tour
                            unite.isSelected = false;//les unite pourront etre rebouger au prochain tour
                        }
                    }

                    break;
                #endregion
                #region attaque
                case Phase_de_jeu.attaque:
                    if (Data.keyboardState.IsKeyDown(Keys.Space) && Data.prevKeyboardState.IsKeyUp(Keys.Space))//change le tour
                    {
                        nouvelle_phase = Phase_de_jeu.ravitaillement;
                        if (joueur == 0)
                            joueur = 1;
                        else
                            joueur = 0;
                        _joueur = joueur + 1;
                    }
                    break;
                #endregion
            }
        }
        protected override void Update(GameTime gameTime)
        {
            Data.Update();
            J1.Update(gameTime,this);
            J2.Update(gameTime, this);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            #region boucle de jeu
            if (joueur == 0)
                jeux(J1);
            else
                jeux(J2);

            #endregion


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
            J2.Draw(spriteBatch);
            spriteBatch.DrawString(TexturePack.font, "c'est le tour de J" + _joueur + " phase : " + nouvelle_phase, new Vector2(100 + 5 * 11 + 30 + 25, 0), Color.White);
            spriteBatch.DrawString(TexturePack.font, "press spacebar when you have finish your turn", new Vector2(100 + 5 * 11 + 30 + 25, 725), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
