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
    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");
        inventoryPanel.SetActive(false);
    }
    public Item GetRandomItem()
    {
        int randItem = (int)Random.Range(0f, AvailableItems.Length);
        Item item = new Item(AvailableItems[randItem].name, AvailableItems[randItem].id, AvailableItems[randItem].sprite);
        return item;
    }

    public void Update()
    {
        for(int i = 0; i < Inventory.weapons.Length; i++)
        {
            if(Inventory.weapons[i] != null)
            {
                weaponSlotsImages[i].sprite = Inventory.weapons[i].gameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }
       /* for(int i = 0; i < slots.Length; i++)
        {
            if(i + 1 < Player.inventory.items.Count)
            {
                slots[i].item = Player.inventory.items[i];
            }
            else
            {
                slots[i].item = null;
            }
        }*/
    }
}
