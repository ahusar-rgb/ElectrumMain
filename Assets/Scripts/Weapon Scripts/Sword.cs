using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{  

    void Start()
    {
        attackRange = 0.5f;
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public override void Strike()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemiesLayer);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            //Наносим урон врагу
        }
    }

   
}
