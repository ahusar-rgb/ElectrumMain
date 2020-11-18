using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public int plushealth;   

    public Armor(string name, int id, Sprite sprite, int plushealth):base(name, id, sprite)
    {
        this.plushealth = plushealth;
    }
}
