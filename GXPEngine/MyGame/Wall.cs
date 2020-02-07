using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Wall : Sprite
{
    public Wall(float xVar, float yVar, int rotationVar) : base("square.png")
    {
        x = xVar;
        y = yVar;
        width = 1000;
        rotation = rotationVar;
    }
}

