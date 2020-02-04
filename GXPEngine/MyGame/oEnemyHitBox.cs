using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class EnemyHitBox : Sprite
{

    public Enemy.EnemyType enemyType;
    public EnemyHitBox(Enemy.EnemyType enemyTypeVar) : base("HitBox.png")
    {
        alpha = 0f;
        enemyType = enemyTypeVar;
    }
}