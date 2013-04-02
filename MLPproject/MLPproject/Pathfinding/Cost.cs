using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MLPproject
{
    static class Cost
    {
        static public int getCost(Tile tile)
        {
            if (tile.Type == Type_tile.herbe)
                return 1;
            else if (tile.Type == Type_tile.eau)
                return 2;
            
            // Si on arrive là c'est qu'on peut pas passer à travers
            return -1;
        }
    }
}
