using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class MyGame : Game
{
    public MyGame() : base(960, 540, false)
    {
    }

    void Update()
    {
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}