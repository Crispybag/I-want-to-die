using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


class StartScreen : GameObject
{
    StScrBackground stScrBackground;
    StScrArrow stScrArrow;
    LevelLoader levelLoader;

    public StartScreen(LevelLoader levelLoaderVar)
    {
        levelLoader = levelLoaderVar;
        stScrBackground = new StScrBackground();
        stScrArrow = new StScrArrow(levelLoader);
        AddChild(stScrBackground);
        AddChild(stScrArrow);
    }
}