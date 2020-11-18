using System;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    public static List<Item> items = new List<Item>();
    public static Weapon[] weapons = new Weapon[2];
    public static Armor armor;
    public static Backpack backpack;
    public static int capacity = 6;
    public static int coins;
    public static InventoryManager invManager;

    void Start()
    {
        GameObject invObj = GameObject.FindWithTag("InvObj");
        invManager = invObj.GetComponent<InventoryManager>();
        items.Clear();
    }
    public static void AddToInventory(Item item)
    {
        if(items.Count < capacity)
        {
            items.Add(item);
            invManager.UpdateInventory();
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

    public static bool CheckWeapon(int number)
    {
        return weapons[number-1] != null;
    }
}
