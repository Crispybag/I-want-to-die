using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Level : GameObject
{
    Random rnd = new Random();

    public Level()
    {
        createPlayer(500, 500);
        createWall();
    }

    Player player;
    void createPlayer(float newX, float newY)
    {
        player = new Player(newX, newY);
        AddChild(player);
    }

    void shootBullet()
    {
        Bullet playerBullet = new Bullet(player);
        AddChild(playerBullet);
    }

    void createWall()
    {
        Wall wall = new Wall(50, 700, 0);
        AddChild(wall);
        wall = new Wall(50, 50, 90);
        AddChild(wall);
        wall = new Wall(50, 50, 23);
        AddChild(wall);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) shootBullet();
    }
}
