using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Enemy : AnimationSprite
{
    //determines what enemy you need to dodge
    public enum EnemyType
    {
        headbonkBoi = 1,
        trippyBoi   = 2,
        fallyBoi    = 3,
        rollyBoi    = 4,
        turtleBoi   = 5
    }
    private int _extraFrameAdd = 1;                       //to circumvent empty enemy sprite
    public EnemyType enemyType = EnemyType.headbonkBoi;
    Level level;
    public Enemy(int vX, Level vLevel, EnemyType vEnemyType) : base("EnemyObstacles.png", 4, 2)
    {
        x = vX;
        level = vLevel;
        enemyType = vEnemyType;
        EnemyHitBox enemyHitBox = new EnemyHitBox(enemyType);

        AddChild(enemyHitBox);
        //setting y coordinate
        if (enemyType != EnemyType.fallyBoi) y = 400;
        else y = 0;


        //load correct sprite
        if (level.levelCurrent >= (level.levelTransition)) _extraFrameAdd = 5;
        if (level.levelCurrent >= (level.levelTransition * 2)) _extraFrameAdd = 1;

        if (enemyType == EnemyType.trippyBoi)   SetFrame(0 + _extraFrameAdd);
        if (enemyType == EnemyType.headbonkBoi) SetFrame(1 + _extraFrameAdd);
        if (enemyType == EnemyType.fallyBoi)    SetFrame(2 + _extraFrameAdd);
        if (enemyType == EnemyType.rollyBoi)    SetFrame(2 + _extraFrameAdd);
        if (enemyType == EnemyType.turtleBoi)   SetFrame(1 + _extraFrameAdd);
        
    }


    //=======================================================================================
    //                                >  Enemy Movement  <
    //=======================================================================================
    void moveEnemy()
    {
        if (enemyType != EnemyType.rollyBoi || x > 920) x -= level.gameSpeedX;
        else x -= (int)(1.5 * level.gameSpeedX);

        y -= level.gameSpeedY;
        if (enemyType == EnemyType.fallyBoi && y < 400 && x < 920) y += 5;
    }

    //=======================================================================================
    //                                >  destroy  <
    //=======================================================================================
    void destroyEnemy()
    {
        if (x < 0) LateDestroy();
    }

    //=======================================================================================
    //                                  >  darken Enemy  <
    //=======================================================================================
    void darkenEnemy()
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
        moveEnemy();
        destroyEnemy();
        darkenEnemy();
    }
}
