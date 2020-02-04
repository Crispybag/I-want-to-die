using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Background : AnimationSprite
{
    Level level;
    private int _speed;
    private int _frame;
    private int _bgLoopAmmount = 3;
    public Background(Level levelVar, int frameVar = 0, int speedVar = 0, int xVar = 0) : base("Game_Background.png", 3, 2)
    {
        _frame = frameVar;
        level = levelVar;
        SetFrame(_frame);
        _speed = speedVar;
        x = xVar;
        
    }

    //=======================================================================================
    //                                >  Move Background  <
    //=======================================================================================
    void moveBg()
    {
        x += _speed;
        if (x <= -width)
        {
            //make it loop seamlessly
            x += width * _bgLoopAmmount;
        }

       
    }

    //=======================================================================================
    //                      >  Remove Background after transition  <
    //=======================================================================================
    void bgSuicide()
    {
        //start fading out first background at transition level
        if (_frame == 0 && level.levelCurrent >= level.levelTransition && alpha > 0)
        {
            alpha -= 0.05f;
        }

        if (_frame <= 4 && level.levelCurrent >= 2 * level.levelTransition && alpha > 0)
        {
            alpha -= 0.05f;
        }
    }
    //=======================================================================================
    //                                  >  Color Correction  <
    //=======================================================================================
    void colorCorrection()
    {
        if (_frame == 5) SetColor(0.5f, 0.5f, 0.5f);
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        //stop moving background when player faceplanted
        if (level.gameSpeedX > 0) moveBg();
        y -= level.gameSpeedY;

        bgSuicide();
        colorCorrection();
    }
   
}
