using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

class Level : GameObject
{
    public Random rnd = new Random();

    public List<Enemy> enemies;        //list to create and remove enemies
    private bool _startLevel = true;   //for game state
    public bool    nextLevel = false;  //also for gamestate

    public int levelCurrent = 0;
    public int levelTransition = 2;   //level at which a transition takes place, 2nd transition is levelTransition * 2
    public int levelFinish = 9;      //Level where you finish, because the game needs an end apparently

    public int score;            
    //public int lives;

    public int gameSpeedX;
    public int gameSpeedY;

    LevelLoader levelLoader;
    Player player;
    public Level(int vCurrentLevel, LevelLoader levelLoaderVar, int livesVar, int scoreVar = 0)

    {
        levelCurrent = vCurrentLevel;
        levelLoader = levelLoaderVar;

        player = new Player(this);
        player.lives = livesVar;
        score = scoreVar;
    }

    //=======================================================================================
    //                         >  Load Objects when starting a level  <
    //=======================================================================================
    void loadStartObjects()
    {


        int bridgeAmmount =   3;     //3 for infinite loop
        int bridgeOffset  = 480;     //width of the bridge
        //============================Background===========================
        if (levelCurrent < levelTransition)
        {
            Background background = new Background(this);
            AddChild(background);
        }
        
        //=============================Ground==============================
        for (int i = 0; i < bridgeAmmount; i++)
        {
            Bridge bridge = new Bridge(bridgeOffset * i, this);
            AddChild(bridge);
        }
        //============================HUD================================
        HUD hud = new HUD(player.lives);
        AddChild(hud);
        

        
        AddChild(player);
    }

    //=======================================================================================
    //                    >  Load Objects when going to the next level  <
    //=======================================================================================
    void loadLevelObjects(int[] enemyType, int enemyCountVar = 100, int enemyIntervalVar = 48, int enemyOffsetVar = 980)
    {
        int enemyCount = enemyCountVar;
        int enemyInterval = enemyIntervalVar;
        int enemyOffset = enemyOffsetVar;
        //==============================Enemy==============================
        enemies = new List<Enemy>();
        for (int i = 0; i < enemyCount; i++)
        {
            enemies.Add(new Enemy(enemyOffset + i * enemyInterval, this, (Enemy.EnemyType)enemyType[i]));
            AddChild(enemies[i]);
        }
        //========================Finish Flag==============================
        FinishFlag finishFlag = new FinishFlag((enemyCount + 1) * enemyInterval + enemyOffset, this);
        AddChild(finishFlag);
    }


    //=======================================================================================
    //                    >  when you game over, check if high score  <
    //=======================================================================================
    void hiScoreCheck()
    {
        //checks score and lives when face planted, determines what to do with it

        if (player.lives == 1 || levelCurrent == levelFinish)
        {
            if (score > levelLoader.hiScore)
            {
                levelLoader.hiScore = score;
            }
        }

        else levelLoader.scoreCurrent = score;
    }

    //=======================================================================================
    //                               >  Determine Game Speed <
    //=======================================================================================
    private int _yShakeMax = 16;
    public bool finalStop = false;
    void speedDecider()
    {
        //speed of the game when died
        if (LevelLoader.levelState == LevelLoader.levelStates.Reset && levelCurrent != levelFinish)
        {
            gameSpeedX = 0;

            //shake effect
            _yShakeMax *= - 1;
            gameSpeedY = _yShakeMax;
            if (_yShakeMax > 0) _yShakeMax -= 2;

            
        }
        else
        {
            //speed dependent on level
            if (levelCurrent < levelFinish)
            {
                gameSpeedX = (int)(levelCurrent + 14) / 2;
                gameSpeedY = 0;
            }
            else
            {
                if (!finalStop) gameSpeedX = 2;
                else gameSpeedX = 0;
            }
        }
        
    }

    //=======================================================================================
    //                               >  Enemy Spawn Patterns <
    //=======================================================================================
    Map levelData = MapParser.ReadMap("gameprogramming.tmx");

    int[] levelEnemiesTiled(int levelCurrent, Map levelData)
    {

        //setup Layers, layer loaded dependent on level
        Layer mainLayer = levelData.Layers[levelCurrent];

        //take data from tiled layer, transform into array
        int[] array = mainLayer.Data.innerXML.Split(',').Select(int.Parse).ToArray();

        return array;
    }
    
    
    //=======================================================================================
    //                               >  Add Score <
    //=======================================================================================
    private int scoreAdd(int scoreVar)
    {
        //score dependent on game speed
        scoreVar += gameSpeedX;
        return scoreVar;
    }
    //=======================================================================================
    //                               >  Print Score <
    //=======================================================================================
    void printScore(int scoreVar, int yVar = 50, bool hiScore = false)
    {
        //for the high score digits
        if (hiScore)
        {
            for (int i = 0; i < 10; i++)
            {
                DigitSprites scoreDigits = new DigitSprites(i, 20 * i + 200, 20, this, levelLoader.hiScore);
                AddChild(scoreDigits);
            }
        }

        //All the digits that aren't high score digits
        else
        {
            for (int i = 0; i < 10; i++)
            {
                DigitSprites scoreDigits = new DigitSprites(i, 20 * i + 200, yVar, this);
                AddChild(scoreDigits);
            }
        }
    }

    //=======================================================================================
    //                          >  parallax background scrolling <
    //=======================================================================================
    int bgAmmount = 3;                            //Connect 3 for infinite loop
    int bgOffset = 960;                           //Space between 2 background objects
    void loadBackground(int frameType)
    {
        for (int i = 0; i < bgAmmount; i++)
        {
            Background background = new Background(this, frameType, gameSpeedX - frameType, bgOffset * i);
            AddChild(background);
        }     
    }
    //=======================================================================================
    //                          >  Create Wind Particles <
    //=======================================================================================
    private int _windParticleMax = 20;          //ammount of wind particles
              

    void createWindParticles()
    {
        for (int i = 0; i < _windParticleMax; i++)
        {
            WindParticles windParticle = new WindParticles(this, rnd);
            AddChild(windParticle);
        }
    }
    //=======================================================================================
    //                          >  Create Star Particles <
    //=======================================================================================
    private int _starParticleMax = 30;

    void createStarParticles()
    {
        for (int i = 0; i < _starParticleMax; i++)
        {
            StarParticles starParticle = new StarParticles(this);
            AddChild(starParticle);
        }
    }

    //=======================================================================================
    //                          >  Create Level Clear <
    //=======================================================================================
    void createLevelClear()
    {
        LevelClear levelClear = new LevelClear();
        AddChild(levelClear);
    }

    //========================================================================================
    //                                     > Update Code <
    //========================================================================================

    void Update()
    {
        //functions that happens when the game starts or resets
        if (_startLevel)
        {
            //loading parallax background 
            if (levelCurrent < 2 * levelTransition)
            {
                for (int i = 0; i < 4; i++) loadBackground(i + 1);
            }

            //loading general objects
            loadStartObjects();
            loadLevelObjects(levelEnemiesTiled(levelCurrent, levelData));

            //Load Particles
            if (levelCurrent < levelTransition) createWindParticles();
            if (levelCurrent >= 2* levelTransition) createStarParticles();

            //Load Scores
            printScore(score);
            printScore(levelLoader.hiScore, 20, true);


            _startLevel = false;
        }

        //functions that happen when you go to the next level
        if (nextLevel)
        {
            
            levelCurrent++;
            
            levelLoader.levelCurrent = levelCurrent;
            createLevelClear();

            //Load enemies and finish flag
            if (levelCurrent < levelFinish)
            {
                if (levelCurrent == 2 * levelTransition) createStarParticles();
                loadLevelObjects(levelEnemiesTiled(levelCurrent, levelData));
                
            }

            //Load Finish house
            else
            {
                FinishHouse finishHouse = new FinishHouse(this);
                AddChild(finishHouse);
                
            }

            nextLevel = false;

        }

        //determines speed of the game and triggers shake effect
        speedDecider();

        //score stuff
        score += gameSpeedX;
        if (LevelLoader.levelState == LevelLoader.levelStates.Reset) hiScoreCheck();
        
    }
}

