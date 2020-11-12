using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGeneral : EnemyScript
{   

    [SerializeField] protected float retreatSpeed; // скорость отступления
    [SerializeField] protected float atackDistance; 
    [SerializeField] protected float minDistance = 1f;  // минимальная дистанция от плеера, при нарушении которой враг начинает отступать



    private int health;

    public int Health
    {
        get
        {
            return health;
        }

        set 
        {
            if(value < 0)               // здоровье не может быть отрицательным
            {
                health = 0;
            }
            else 
            {
                health = value;
            }
        }
    }




    private int damage;

    public int Damage
    {
        get
        {
            return damage;
        }

        set 
        {
            if(value <= 0)              // урон не может быть нулевым или отрицательным
            {
                damage = 1;
            }
            else{
                damage = value;
            }
        }
    }
}
