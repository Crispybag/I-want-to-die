using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Bridge : AnimationSprite
{
    Level level;
    private int _bridgeAmmount = 3;

    public Bridge(int vX, Level vLevel) : base("Bridge.png", 2, 1)
    {
        x = vX;
        level = vLevel;
        y = 448;

        //load correct sprite
        if (level.levelCurrent >= level.levelTransition) SetFrame(1);
        if (level.levelCurrent >= level.levelTransition * 2) SetFrame(0);

    }
    //=======================================================================================
    //                                  >  move Bridge  <
    //=======================================================================================
    void moveBridge()
    {
        x += -level.gameSpeedX;
        if (x <= -width)
        {
            x += width * _bridgeAmmount;
        }

        y -= level.gameSpeedY;
    }

    //=======================================================================================
    //                                  >  changeSprite  <
    //=======================================================================================
    void changeSprite()
    {
        //if the level theme changes, so does the bridge, S m o o t h l y
        if (level.levelCurrent >= level.levelTransition && x <= -width + level.gameSpeedX) SetFrame(1);
        if (level.levelCurrent >= 2 * level.levelTransition && x <= -width + level.gameSpeedX) SetFrame(0);
    }

    //=======================================================================================
    //                                  >  darken Bridge  <
    //=======================================================================================
    void darkenBridge()
    {
        int brightness = 0;
        if (level.levelCurrent < 2 * level.levelTransition) SetColor(0, 0, 0);
        else
        {
            brightness += 10;
            SetColor(brightness, brightness, brightness);
        }
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        changeSprite();
        moveBridge();
        darkenBridge();
    }

}