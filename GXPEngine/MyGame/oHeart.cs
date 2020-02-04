using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Heart : Sprite
{
    private int _offsetX = 820;

    public Heart(int varX) : base("Heart.png")
    {
        x = varX + _offsetX;
        y = 20;
        
    }
}

