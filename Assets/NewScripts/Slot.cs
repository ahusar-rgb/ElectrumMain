using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item;

    public void Respawn()
    {
        if(item != null) 
        {
            gameObject.GetComponent<Image>().sprite = item.sprite;
            gameObject.GetComponent<Image>().color = new Color(255,255,255,255);
            gameObject.name = item.name;
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(255,255,255,0);
            gameObject.name = "none";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
	{
		InventoryManager.selectedItem = item;
        InventoryManager.UpdateDescr();
	}
}
