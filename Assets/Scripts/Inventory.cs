using System;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    private static List<Item> items = new List<Item>();
    public static Weapon[] weapons = new Weapon[2];
    private static Armor armor;
    private static Backpack backpack;
    public static int capacity;
    public static int coins;

    public static void AddToInventory(Item item)
    {
        if(items.Count < capacity)
        {
            items.Add(item);
        }
        else
        {
            //show message / do nothing 
        }
    }

    public static void Equip(Weapon weapon, int number)
    {
        weapon.number = number;
        weapons[number-1] = weapon;
    }

    public static void Equip(Armor armor)
    {
        Inventory.armor = armor;
    }

    public static void Equip(Backpack backpack)
    {
        Inventory.backpack = backpack;
        capacity = backpack.capacity;
    }

    public static void PrintInfo()
    {
        string str = "";
        foreach (Weapon weapon in weapons)
        {
            if(weapon != null) 
            {
                str += weapon.gameObject.ToString(); 
                str += "\n";
            }
            else
            {
                str += "null";
                str += "\n";
            }
        }
        print(str);
    }

    public static bool CheckWeapon(int number)
    {
        return weapons[number-1] != null;
    }
}
