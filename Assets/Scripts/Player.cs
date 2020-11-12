using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
 
    public static float speed = 1;
    public static Vector2 playerVector;

    protected int playerHealth = 5, playerMaxHealth =7;
    //[SerializeField] protected Healthbar healthbar;

    private JoystickFirst JoystickFirst;
    private WeaponTypes weaponType;
    private GameObject equipedweapon;

    void Start()
    {     
        //healthbar.Set(playerHealth/playerMaxHealth);
        JoystickFirst = GameObject.Find("Joystick1").GetComponent<JoystickFirst>();
        equipedweapon = GameObject.FindGameObjectWithTag("Start Weapon");
        if(equipedweapon != null) 
        {
            equipedweapon.transform.SetParent(gameObject.transform);
            equipedweapon.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
        }
    }

    void FixedUpdate()
    {
        if (!JoystickFirst.isFree)
        {
            float v = JoystickFirst.Vertical;
            float h = JoystickFirst.Horizontal;
            playerVector = (new Vector2(h, v) * Time.deltaTime) * speed;
            transform.Translate(playerVector);
            if(h<0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(h>0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            playerVector = Vector2.zero;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.inventoryPanel.SetActive(!InventoryManager.inventoryPanel.activeSelf);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name.StartsWith("Weapon") && Input.GetKeyDown(KeyCode.F))
        {
            EquipWeapon(col);
        }
    }

    void EquipWeapon(Collider2D col)
    {
        if(!Inventory.CheckWeapon(1))
        {
            Inventory.Equip(col.gameObject.GetComponent<Weapon>(), 1);
            SetWeapon(col.gameObject);
        }
        else if(!Inventory.CheckWeapon(2))
        {
            Inventory.Equip(col.gameObject.GetComponent<Weapon>(), 2);
            SetWeapon(col.gameObject);
        }
        else
        {
            Inventory.Equip(col.gameObject.GetComponent<Weapon>(), equipedweapon.GetComponent<Weapon>().number);
            DropEquipedWeapon(equipedweapon);
            SetWeapon(col.gameObject);
        }
        Inventory.PrintInfo();
    }

    public void Attack()
    {      
        switch (weaponType)
        {
            case WeaponTypes.Wand:
                equipedweapon.GetComponent<Wand>().Strike();
                break;
            case WeaponTypes.Staff:
                equipedweapon.GetComponent<Staff>().Strike();
                break;
            case WeaponTypes.Sword:
                equipedweapon.GetComponent<Sword>().Strike();
                break;
        }
    }

    public void SetWeapon(GameObject col)
    {
        if(equipedweapon != null) equipedweapon.SetActive(false);
        col.transform.SetParent(gameObject.transform);
        col.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
        col.GetComponent<Weapon>().isEquiped = true;
        equipedweapon = col;
        weaponType = (WeaponTypes)Enum.Parse(typeof(WeaponTypes), col.tag); 
    }

    private void DropEquipedWeapon(GameObject weaponObj)
    {
        weaponObj.transform.parent = null;
        weaponObj.GetComponent<Weapon>().isEquiped = false;
    }
}
