using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class LevelClear : Sprite
{
    private int _timer = 60;
    public LevelClear() : base("level_clear.png")
    {
        x = 460 - width/2;
        y = 100;
    }

    //========================================================================================
    //                                     > selfDestruct <
    //========================================================================================
    void selfDestruct()
    {
        _timer--;
        if (_timer <= 0) LateDestroy();
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        selfDestruct();
    }
}