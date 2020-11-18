using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
 
    public static float speed = 1;
    public static Vector2 playerVector;

    public static int playerHealth = 5, playerMaxHealth =7;
    //[SerializeField] protected Healthbar healthbar;

    private JoystickFirst JoystickFirst;
    private WeaponTypes weaponType;
    private GameObject equipedweapon;
    public static InventoryManager inventoryManager;
    private Animator anim;
    private Transform hand;
    private float PlayerOrigScale;

    void Start()
    {     
        PlayerOrigScale = gameObject.transform.localScale.x;
        hand = gameObject.transform.GetChild(0);
        anim = GetComponent<Animator>();
        inventoryManager = GameObject.Find("GameController").GetComponent<InventoryManager>();
        JoystickFirst = GameObject.Find("Joystick1").GetComponent<JoystickFirst>();
        equipedweapon = GameObject.FindGameObjectWithTag("StartWeapon") ?? null;
        if(equipedweapon != null) 
        {
            equipedweapon.transform.SetParent(gameObject.transform);
            equipedweapon.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.inventoryPanel.SetActive(!InventoryManager.inventoryPanel.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapon();
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
                gameObject.transform.localScale = new Vector3(-PlayerOrigScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else if(h>0)
            {
                gameObject.transform.localScale = new Vector3(PlayerOrigScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        else
        {
            playerVector = Vector2.zero;
            anim.SetBool("isRunning", false);
        }
        if(playerVector != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (col.gameObject.name.StartsWith("Weapon"))
            {  
                EquipWeapon(col);
            }
            else if(col.gameObject.tag == "Item")
            {
                Inventory.AddToInventory(col.gameObject.GetComponent<Item>());
                col.gameObject.transform.position = transform.position;
                col.gameObject.transform.parent = transform;
                col.gameObject.SetActive(false);
            }
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
            equipedweapon.SetActive(true);
            Player.DropItem(equipedweapon);  
            equipedweapon = null;
            SetWeapon(col.gameObject); 
        }
        for(int i = 0; i < Inventory.weapons.Length; i++)
        {
            if(Inventory.weapons[i] != null)
            {
                inventoryManager.weaponSlotsImages[i].sprite = Inventory.weapons[i].gameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }
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
        if(equipedweapon != null) 
        {
            equipedweapon.SetActive(false);
            equipedweapon.GetComponent<Weapon>().isEquiped = false;
        }
        equipedweapon = col;
        col.transform.SetParent(gameObject.transform);
        col.transform.position = new Vector3(hand.position.x, hand.position.y, -1f);
        col.GetComponent<Weapon>().isEquiped = true;
        equipedweapon.SetActive(true);
        weaponType = (WeaponTypes)Enum.Parse(typeof(WeaponTypes), col.tag); 
    }

    public static void DropItem(GameObject item)
    {
        if(item.GetComponent<Item>() != null)
        {
            int index = Inventory.items.FindIndex(a => a == item.GetComponent<Item>());
            Inventory.items[index] = null;
        }
        inventoryManager.UpdateInventory();
        item.transform.parent = null;
        if(item.GetComponent<Weapon>() != null) item.GetComponent<Weapon>().isEquiped = false;
        item.SetActive(true);
    }

    public static void UseItem(GameObject item)
    {
        item.GetComponent<DrinkSpell>().Effect();
        Inventory.items.Remove(item.GetComponent<Item>());
        inventoryManager.UpdateInventory();
        Destroy(item.gameObject);
    }

    private void SwapWeapon()
    {
        if(equipedweapon.GetComponent<Weapon>().number == 1)
        {
            SetWeapon(Inventory.weapons[1].gameObject);
        }
        else
        {
            SetWeapon(Inventory.weapons[0].gameObject);
        }
    }
}
