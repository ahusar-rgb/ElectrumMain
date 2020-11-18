using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static GameObject inventoryPanel;
    public Slot[] slots = new Slot[6];
    [SerializeField]
    private Item[] AvailableItems;
    public Image[] weaponSlotsImages = new Image[2];
    public static Text descrText;
    public static Item selectedItem;

    void Start()
    {
        inventoryPanel = GameObject.Find("Inventory");
        inventoryPanel.SetActive(false);
        GameObject descr = inventoryPanel.transform.GetChild(1).gameObject;
        descrText = descr.GetComponent<Text>();
    }
    public Item GetRandomItem()
    {
        int randItem = (int)Random.Range(0f, AvailableItems.Length);
        Item item = new Item(AvailableItems[randItem].name, AvailableItems[randItem].id, AvailableItems[randItem].sprite);
        return item;
    }

    public void UpdateInventory()
    {
       for(int i = 0; i < Inventory.items.Count; i++)
        {
            slots[i].item = Inventory.items[i];
            slots[i].Respawn();
        }
        if(Inventory.items.Count == 0)
        {
            foreach (Slot slot in slots)
            {
                slot.item = null;
                selectedItem = null;
                slot.Respawn();
            }
        }
    }

    public static void UpdateDescr()
    {
        descrText.text = selectedItem != null ? selectedItem.description : "None item selcted";
    }

    public void Drop()
    {
        Player.DropItem(selectedItem.gameObject);
    }
    public void Use()
    {
        Player.UseItem(selectedItem.gameObject);
    }
}
