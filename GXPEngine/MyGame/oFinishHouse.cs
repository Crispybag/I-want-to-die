using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class FinishHouse : Sprite
{
    private Level _level;
    
    public FinishHouse(Level levelVar) : base("finishHouse.png")
    {
        _level = levelVar;
        x = 940 + width;
        y = 448 - height; 
    }

    //========================================================================================
    //                                     > move the house <
    //========================================================================================
    void move()
    {
        x += -_level.gameSpeedX;
    }

    //========================================================================================
    //                                     > Congratulations <
    //========================================================================================
    //Makes the game stop, and resets the level
    void congratulations()
    {
        _level.finalStop = true;
        LevelLoader.levelState = LevelLoader.levelStates.Reset;

    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        move();
        if (x <= 940 - 2 * width && !_level.finalStop) congratulations();
    }
}