using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class HUD : GameObject
{
    public HUD(int lives)
    {
        LevelClear levelClear = new LevelClear();
        AddChild(levelClear);

        HighScore highScore = new HighScore();
        AddChild(highScore);

        Lives livesText = new Lives();
        AddChild(livesText);

        for (int i = 0; i < lives; i++)
        {
            Heart hearts = new Heart(i * 20);
            AddChild(hearts);
        }
    }
}
