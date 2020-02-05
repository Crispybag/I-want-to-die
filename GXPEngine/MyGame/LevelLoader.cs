using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class LevelLoader : GameObject
{
    public LevelLoader()
    {
        startLevel();
    }


    void startLevel()
    {
        Level level = new Level();
        AddChild(level);
    }
}