using System;
using UnityEngine;
using System.Collections.Generic;


[Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite sprite;

    public Item(string name, int id, Sprite sprite)
    {
        this.name = name;
        this.id = id;
        this.sprite = sprite;
    }
}
