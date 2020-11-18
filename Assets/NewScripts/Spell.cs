using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    int damage;
    private GameObject player; 

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<EnemyGeneral>() != null)
        {
            col.GetComponent<EnemyGeneral>().Health -= damage;
        }
        if(col.gameObject != player && col.gameObject.transform.parent.gameObject != player)
        {
            DestroySelf();
        }
    }

    public virtual void DestroySelf()
    {

    }
}
