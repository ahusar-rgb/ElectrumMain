using System;
using UnityEngine;
using System.Collections.Generic;


[Serializable]
public class Item : MonoBehaviour
{
    public string name;
    public int id;
    public Sprite sprite;
    public string description;

    public Item(string name, int id, Sprite sprite)
    {
        this.name = name;
        this.id = id;
        this.sprite = sprite;
    }
}
