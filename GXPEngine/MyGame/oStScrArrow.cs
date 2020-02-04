using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class StScrArrow : Sprite
{
    LevelLoader levelLoader;
    private bool _keyAtTop = true;

    public StScrArrow(LevelLoader levelLoaderVar) : base("stScrArrow.png")
    {
        levelLoader = levelLoaderVar;
        x = 300;
        y = 420;
    }

    //========================================================================================
    //                                     > pressKey <
    //========================================================================================
    void pressKey()
    {
        //arrow moves between fixed location
        if (Input.GetKey(Key.DOWN))
        {
            y = 466;
            _keyAtTop = false;
        }

        else if (Input.GetKey(Key.UP))
        {
            y = 420;
            _keyAtTop = true;
        }
    }

    //========================================================================================
    //                                     > Start Game <
    //========================================================================================
    void startGame()
    {
        if (Input.GetKey(Key.SPACE))
        {
            //start game when the option is "start game"
            if (_keyAtTop) LevelLoader.levelState = LevelLoader.levelStates.Start;

            //quit game when option is "quit"
            else Environment.Exit(1);
            
        }
      
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        pressKey();
        startGame();
    }
}
