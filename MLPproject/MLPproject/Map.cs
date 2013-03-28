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
    class Map
    {
        int[,] plateau_tile = new int[32,32];
        Type_unite[,] plateau_unité = new Type_unite[32, 32];

        public Map()
        {



        }


        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < plateau_tile.GetLength(1); i++)
			{
			 for (int j = 0; j < plateau_tile.GetLength(0); j++)
			{
                 switch(plateau_tile[i,j])
                 {
                     case 0: 

                         break;
                     case 1:
                         break;


                     case 2:

                         break;





                 }
			 
			}
			}






        }

    }
}
