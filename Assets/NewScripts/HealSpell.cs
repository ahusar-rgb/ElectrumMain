using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : DrinkSpell
{
    private int healingRate = 2;
    public HealSpell(string name, int id, Sprite sprite):base(name, id, sprite)
    {

    }
    public override void Effect()
    {
        //Player.playerHealth += healingRate;
        print("Healed");
    }
}
