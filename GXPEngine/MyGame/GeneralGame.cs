using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class MyGame : Game
{
   //hello
   

    public MyGame() : base(920, 540, false)
    {
        LevelLoader levelLoader = new LevelLoader();
        AddChild(levelLoader);
        //hi

    }

    static void Main()
    {
        new MyGame().Start();
    }
}