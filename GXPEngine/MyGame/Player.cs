using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Player : Sprite
{
    
    public Player(float newX, float newY) : base("arrow.png")
    {
        x = newX;
        y = newY;

        SetOrigin(width / 2, height / 2);
    }


    float mouseX;
    float mouseY;

    void getMousePos()
    {
        mouseX = Input.mouseX;
        mouseY = Input.mouseY;
    }


    float mouseAngle;

    void getMouseAngle()
    {
        if (mouseX - x >= 0)
        {
            mouseAngle =  (180 / Mathf.PI) * Mathf.Acos((-mouseY + y) / Mathf.Sqrt(Mathf.Pow(mouseX - x, 2) + Mathf.Pow(mouseY - y, 2)));
        }

        else
        {
            mouseAngle = -(180 / Mathf.PI) * Mathf.Acos((-mouseY + y) / Mathf.Sqrt(Mathf.Pow(mouseX - x, 2) + Mathf.Pow(mouseY - y, 2)));
        }
    }

    void rotatePlayer()
    {
        getMouseAngle();
        rotation = mouseAngle;
    }
   
    void Update()
    {
        getMousePos();
        rotatePlayer();
        Console.WriteLine(mouseAngle);
    }
}