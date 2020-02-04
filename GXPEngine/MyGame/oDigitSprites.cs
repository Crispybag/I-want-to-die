using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DigitSprites : AnimationSprite
{
    Level level;
    private string _scoreString;
    private int _zeroExcess;               //all the extra 0's infront of the score
    private int _digit;
    private int _hiScore;

    public DigitSprites(int digitVar, int xVar, int yVar, Level levelVar, int hiScoreVar = 99999999) : base("numbers.png", 10, 1)
    {
        _digit = digitVar;
        x = xVar;
        y = yVar;
        level = levelVar;
        _hiScore = hiScoreVar;
    }

    //=======================================================================================
    //                                  >  read digits  <
    //=======================================================================================
    //99999999 is an inachievable score, so it can instantly see when it needs to print the current score or high score
    void determineDigits()
    {
        //determines what int needs to be converted to string
        if (_hiScore == 99999999) _scoreString = level.score.ToString();
        else _scoreString = _hiScore.ToString();

        //determines ammount of 0s needed to be added
        _zeroExcess = 10 - _scoreString.Length;
    }

    //=======================================================================================
    //                                  >  print digits  <
    //=======================================================================================
    void frameSetter()
    {
        //sets each digit of the string back to an integer with ASCII compensation (-48)
        if (_digit >= _zeroExcess) SetFrame((int)(_scoreString[(_digit - _zeroExcess)] - 48));

        //all zeroExcess variables need to be a 0
        else SetFrame(0);
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        determineDigits();
        frameSetter();
    }
}

