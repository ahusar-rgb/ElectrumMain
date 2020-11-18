using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSpell : DrinkSpell
{
    float duration;
    float SpeedingUpRate = 1.2f;
    public SpeedSpell(string name, int id, Sprite sprite):base(name, id, sprite)
    {

    }

    public override void Effect()
    {
        /*float time = Time.time;
        Player.speed *= SpeedingUpRate;
        if(Time.time - time >= duration)
        {
            Player.speed /= SpeedingUpRate;
        }
        //не дописал*/
        print("speeded");
    }
}
