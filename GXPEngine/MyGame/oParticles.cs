using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class WindParticles : Sprite
{
    private Level _level;

    

    private float speed;
    private int _gameWidth = 920;
    private int _gameHeight = 540;
    Random rnd;

    public WindParticles(Level levelVar, Random rndVar) : base("windSprite.png")
    {
        _level = levelVar;
        alpha = 0.2f;
        rnd = rndVar;
        y = rnd.Next(0, _gameHeight);
        x = rnd.Next(0, _gameWidth);
        speed = rnd.Next(-25, -15);
    }

    //=======================================================================================
    //                                   >  Move Particles  <
    //=======================================================================================
   
    void move()
    {
        //Move at random speed when reset
        if (x >= _gameWidth) speed = rnd.Next(-25, -15);
        x += speed;

        //reset
        if (x < -width)
        {
            x = _gameWidth;
            y = rnd.Next(0, _gameHeight);
        }
    }
    //=======================================================================================
    //                                   >  Transition  <
    //=======================================================================================
    void transition()
    {
        //fade out and destroy
        alpha -= 0.05f;
        if (alpha <= 0) LateDestroy();
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        move();
        if (_level.levelCurrent >= _level.levelTransition) transition();
    }
}



//=======================================================================================
//                                   >  Dirt Particles  <
//=======================================================================================
class DirtParticles : Sprite
{
    Random rnd;
    Player player;
    Level level;
    int xSpeed;
    int ySpeed;
    public DirtParticles(Random rndVar, Player playerVar, Level levelVar) : base("dirt_particle.png")
    {
        rnd = rndVar;
        xSpeed = rnd.Next(-5, -1);
        ySpeed = rnd.Next(-5, -1);
        alpha = rnd.Next(6, 11) * 0.1f;
        scale  = rnd.Next(1,   3) / 2;
        
        player = playerVar;
        level  = levelVar;

        x = player.width / 2;
        y = player.height;

    }
    //=======================================================================================
    //                                   >  Move and Fade  <
    //=======================================================================================
    //moves and fades
    void moveAndFade()
    {
        x += xSpeed;
        y += ySpeed;
        alpha += -0.05f;
    }

    //=======================================================================================
    //                               >  Reset Location and alpha  <
    //=======================================================================================
    //relocate particle back to origin, randomize angle again
    void reset()
    {
        
        if (alpha <= 0.1)
        {
            x = player.width / 2;
            y = player.height;
            xSpeed = rnd.Next(-5, -1);
            ySpeed = rnd.Next(-5, -1);
            alpha = rnd.Next(6, 11) * 0.1f;
        }
    }

    //=======================================================================================
    //                               >  Stop drawing particles  <
    //=======================================================================================
    //stop drawing particles
    void stopDrawing()
    {
        if (level.gameSpeedX == 0) { alpha = 0f; }
    }


    //=======================================================================================
    //                                >  Darken Particle <
    //=======================================================================================
    //make particle black instead of white
    void darkenParticle()
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
    int groundLevel = 400;
    void Update()
    {
        
        darkenParticle();
        //no dirt particles when the player is on the ground
        if (player.y == groundLevel)reset();
        moveAndFade();
        stopDrawing();
    }
}



//=======================================================================================
//                                >  Star  Particles  <
//=======================================================================================

class StarParticles : AnimationSprite
{
    Random rnd;
    Level level;
    private int _gameWidth = 920;
    private int _gameHeight = 400;
    private float _fadeSpeed;                      //Speed at which alpha fades
    int pixelSize = 3;                    //Makes pixels snap to grid

    public StarParticles(Level levelVar) : base("stars.png", 3, 1)
    {
        level = levelVar;
        rnd = level.rnd;

        //lock to pixel grid, so the pixels aren't off
        y = (int)(rnd.Next(0, _gameHeight)/pixelSize) * pixelSize;
        x = (int)(rnd.Next(0, _gameWidth)/pixelSize)  * pixelSize;

        alpha = 0.1f;
        _fadeSpeed = 0.01f * rnd.Next(1, 4) ;
        SetFrame(rnd.Next(0, 3));
    }
    //=======================================================================================
    //                              >  fade stars in and out  <
    //=======================================================================================
    void fadeIn()
    {
        alpha += _fadeSpeed;
        if (alpha >= 0.9f || alpha <= 0.1f)
        {
            _fadeSpeed *= -1;
        }

        //relocate star
        if (alpha <= 0.1f)
        {
            y = (int)(rnd.Next(0, _gameHeight) / 3) * 3;
            x = (int)(rnd.Next(0, _gameWidth) / 3) * 3;
        }

    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================
    void Update()
    {
        fadeIn();
    }
}