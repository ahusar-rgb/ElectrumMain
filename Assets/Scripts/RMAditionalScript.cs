using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMAditionalScript : MonoBehaviour
{

void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }    
    }
}
