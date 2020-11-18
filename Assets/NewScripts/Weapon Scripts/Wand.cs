using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    private Transform shotPoint;
    [SerializeField]private GameObject fireball;

    void Start()
    {
        attackRange = 0.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        shotPoint = transform.Find("ShotPoint").GetComponent<Transform>();
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }

    public override void Strike()
    {
        if (/*растояние до ближайшего врага больше длины посоха*/true)
        {
            Instantiate(fireball, shotPoint.position, Quaternion.Euler(0, 0, Mathf.Atan2(joystickSecond.Vertical2, joystickSecond.Horizontal2) * Mathf.Rad2Deg));
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemiesLayer);
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                //Наносим урон врагу
            }
        }
    }
}
