using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLPproject
{
    class Joueur
    {

        int compte_en_banque;
        int _ID;

        int ID
        {
            get{return ID;}
            set{_ID = value;}
        }

        public Joueur(int id)
        {
            compte_en_banque = 500;
            this._ID = id;
        }

    }
}
