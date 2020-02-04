using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class FinishFlag : AnimationSprite
{
    Level level;
    public FinishFlag(int xVar, Level levelVar) : base("colors.png", 2, 1)
    {
        x = xVar;
        level = levelVar;
        alpha = 0;
        
    }

    //=======================================================================================
    //                                 >  Destroy Flag  <
    //=======================================================================================
    void removeFlag()
    {
        if (x <= 0)
        {
            level.nextLevel = true;
            LateDestroy();
        }
        
    }

    //=======================================================================================
    //                                 >  Move Flag  <
    //=======================================================================================
    void moveFlag()
    {
        x -= level.gameSpeedX;
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        removeFlag();
        moveFlag();
    }
}

