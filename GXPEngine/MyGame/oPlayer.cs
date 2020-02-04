using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Player : AnimationSprite
{
    Level level;
    private Sound _jumpSound;
    private Sound _duckSound;
    private Sound _facePlantSound;

    public int lives;

    public Player(Level levelVar) : base("SpriteSheetCharacter.png", 5, 1)
    {
        level = levelVar;
        x = 30;
        y = 400;
        createParticles();

        _jumpSound = new Sound("jumpSound.wav", false, false);
        _duckSound = new Sound("duckSound.wav", false, false);
        _facePlantSound = new Sound("facePlantSound.wav", false, false);
    }

    //=======================================================================================
    //                                   >  Animation  <
    //=======================================================================================
    
    //Variables to keep track of when the frames should switch
    private int _idleFrame;
    private int _idleFrameSwitch = 20;

    void IdleAnimation()
    {
        _idleFrame++;
        if (_idleFrame++ > _idleFrameSwitch / 2)
        {
            SetFrame(1);
        }
        if (_idleFrame++ >= _idleFrameSwitch)
        {
            SetFrame(0);
            _idleFrame = 0;
        }
    }
    
    //=======================================================================================
    //                                   >  Movement  <
    //=======================================================================================
    private int _speed = 3;

    private void movementPlayer()
    {
        //move right
        if (Input.GetKey(Key.RIGHT) && x + _speed < game.width - width)
        {
            Move(_speed, 0);
        }

        //move left
        else if (Input.GetKey(Key.LEFT) && x + _speed > 0)
        {
            Move(-_speed, 0);
        }
    }
    //=======================================================================================
    //                                >  Player State  <
    //=======================================================================================

    //enum that utilises the state of the player
    enum Playerstate { idle, jumping, ducking, faceplant};
    Playerstate playerstate = Playerstate.idle;

    //variables to keep track of how long player dodges
    private int _dodgeTick = 0;
    private int _dodgeTickMax = 20;

    private bool _keyHeld = false;

    private void changePlayerState()
    {
        _dodgeTick--;

        //you can only tap to activate
        if (!Input.GetKey(Key.DOWN) && !Input.GetKey(Key.UP)) _keyHeld = false;

        //state where player is able to jump/duck
        if (playerstate == Playerstate.idle && !_keyHeld)
        {
            //jump :D
            if (Input.GetKey(Key.UP))
            {
                _dodgeTick = _dodgeTickMax;
                _keyHeld = true;

                playerstate = Playerstate.jumping;
                y -= 5;
                SetFrame(3);
                _jumpSound.Play();
            }

            //duck >:]
            else if (Input.GetKey(Key.DOWN))
            {
                _dodgeTick = _dodgeTickMax;
                _keyHeld = true;

                playerstate = Playerstate.ducking;
                SetFrame(2);
                _duckSound.Play();
            }
        }

        //idle :>
        if (_dodgeTick < 0)
        {
            if (playerstate == Playerstate.jumping) y += 5;
            playerstate = Playerstate.idle;

        }

        
    }
    //=======================================================================================
    //                                   >  Hit Check  <
    //=======================================================================================
    private void OnCollision(GameObject other)
    {
        //check enemy type and playerstate, check if dead or not
        if (other is EnemyHitBox)         
        {
            EnemyHitBox enemyHitBox = other as EnemyHitBox;

            //collision for head bonkies
            if (enemyHitBox.enemyType == Enemy.EnemyType.headbonkBoi)
            {
                if (playerstate != Playerstate.ducking)
                {
                    die();
                }
            }

            //collision for rollybois
            if (enemyHitBox.enemyType == Enemy.EnemyType.trippyBoi ||enemyHitBox.enemyType == Enemy.EnemyType.rollyBoi)
            {
                if (playerstate != Playerstate.jumping)
                {
                    die();
                }
            }

            //falling enemies
            if (enemyHitBox.enemyType == Enemy.EnemyType.fallyBoi)
                if (playerstate != Playerstate.jumping || (enemyHitBox.y < 395 && enemyHitBox.y > 375))
                {
                    die();
                }
        }
    }
    //=======================================================================================
    //                                     >  Die  <
    //=======================================================================================
    //keep player on ground, slide forward a bit

    private int _slideDist = 32;
    private void die()
    {
        if (playerstate != Playerstate.faceplant)
        {
            x += _slideDist;
            _facePlantSound.Play();
        }
        playerstate = Playerstate.faceplant;
        y = 400;
        LevelLoader.levelState = LevelLoader.levelStates.Reset;
        SetFrame(4);
        

    }
    //=======================================================================================
    //                              >  Create Particles  <
    //=======================================================================================
    DirtParticles dirtParticles;
    private int _particleAmmount = 20;
    void createParticles()
    {
        for (int i = 0; i < _particleAmmount; i++)
        {
            dirtParticles = new DirtParticles(level.rnd, this, level);
            AddChild(dirtParticles);
        }
    }
    //=======================================================================================
    //                                >  Darken Player  <
    //=======================================================================================
    void darkenPlayer()
    {
        int brightness = 0;
        if (level.levelCurrent < 2 * level.levelTransition) SetColor(0, 0, 0);
        else
        {
            brightness += 10;
            SetColor(brightness, brightness, brightness);
        }
    }

   
    //=======================================================================================
    //                                >  Update Player  <
    //=======================================================================================
    void Update()
    {
        if (playerstate != Playerstate.faceplant)
        {
            changePlayerState();
            if (playerstate == Playerstate.idle) IdleAnimation();
            movementPlayer();

        }

        darkenPlayer();
        y -= level.gameSpeedY;
    }
}