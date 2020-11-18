using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkSpell : Item
{
    private GameObject DescrText;
    void Start()
    {
        DescrText = gameObject.transform.GetChild(0).gameObject;
        DescrText.SetActive(false);
    }

    public DrinkSpell(string name, int id, Sprite sprite):base(name, id, sprite)
    {

    }

    public virtual void Effect()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            DescrText.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            DescrText.SetActive(false);
        }
    }
}
