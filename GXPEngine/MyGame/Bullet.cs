using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Bullet : Sprite
{
    float xSpeed = 10f;
    Player player;

    public Bullet(Player playerVar) : base("colors.png")
    {
        SetOrigin(0.5f * width, 0.5f * height);
        player = playerVar;

        x = player.x;
        y = player.y;
        rotation = player.mouseAngle;
    }

    void moveBullet()
    {
        Move(0, -xSpeed);
    }

    int bounceCooldown;
    void OnCollision(GameObject other)
    {
        
        if (other is Wall && bounceCooldown < 1)
        {
            float slopeAngle = rotation - (other.rotation + 90);
            rotation = other.rotation + 90 - slopeAngle;
            bounceCooldown = 3;
        }
    }

    void optimise()
    {
        LateDestroy();
        Console.WriteLine("hello");
    }

    void Update()
    {
        moveBullet();
        if (y < -2000) optimise();
        bounceCooldown--;
    }

}
