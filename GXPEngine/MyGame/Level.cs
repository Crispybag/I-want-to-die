using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Level : GameObject
{
    Random rnd = new Random();

    public Level()
    {
        for (int i = 0; i < 50; i++) createPlayer(rnd.Next(0,1920), rnd.Next(0,1080));
    }

    void createPlayer(float newX, float newY)
    {
        Player player = new Player(newX, newY);
        AddChild(player);
    }
}
