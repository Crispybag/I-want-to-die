using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class MyGame : Game
{

    public MyGame() : base(1920, 1080, false)
    {
        LevelLoader levelLoader = new LevelLoader();
        AddChild(levelLoader);
    }

    static void Main()
    {
        new MyGame().Start();
    }
}