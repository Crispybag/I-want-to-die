using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class LevelLoader : GameObject
{
    private Sound _mainLoop;
    public int levelCurrent = 0;
    Level level;
    StartScreen startScreen;
    //own hi-score = 107475
    public int hiScore = 0;
    public int scoreCurrent = 0;
    private int _lives = 3;

    public LevelLoader()
    {
        startScreen = new StartScreen(this);
        AddChild(startScreen);
        _mainLoop = new Sound("main_loop.wav", true, true);
        
    }

    public enum levelStates {Running, Next, Reset, Start}
    public static levelStates levelState = levelStates.Running;
      
    //========================================================================================
    //                                     > Reset Level Code <
    //========================================================================================
    private float _resetDelay = 40;
    List<GameObject> levelChildren;
    void resetLevel()
    {
        levelChildren = GetChildren();
        //reset delay for not an instant restart


        //Increase delay when reaching the end
        if (levelCurrent != 9) _resetDelay--;
        else _resetDelay -= 0.5f;

        if (_resetDelay <= 0)
        {
            while (levelChildren.Count > 0)
            {
                levelChildren[0].Destroy();
            }

            if (_lives > 1 && levelCurrent < 9)
            {
                _lives--;
                level = new Level(levelCurrent, this, _lives, scoreCurrent);
                AddChild(level);     
            }
            else
            {
                _lives = 3;
                levelCurrent = 0;
                scoreCurrent = 0;
                returnToStartScreen();
            }

            
            _resetDelay = 40;
            levelState = levelStates.Running;
        }

    }

    //========================================================================================
    //                                     > Next Level Code <
    //========================================================================================
    void nextLevel()
    {
        RemoveChild(level);
        level = new Level(levelCurrent, this, _lives);
        AddChild(level);
        levelState = levelStates.Running;
    }
    //========================================================================================
    //                                    > Start Level Code <
    //========================================================================================
    void startLevel()
    {
        RemoveChild(startScreen);
        level = new Level(levelCurrent, this, _lives, scoreCurrent);
        AddChild(level);
        _mainLoop.Play();
        levelState = levelStates.Running;
    }
    void returnToStartScreen()
    {
        RemoveChild(level);
        startScreen = new StartScreen(this);
        AddChild(startScreen);
        _mainLoop.Play(true);
        levelState = levelStates.Running;
    }
    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        if (levelState == levelStates.Reset) resetLevel();
        if (levelState == levelStates.Next) nextLevel();
        if (levelState == levelStates.Start) startLevel();
    }
}
