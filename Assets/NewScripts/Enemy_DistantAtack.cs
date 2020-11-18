using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_DistantAtack : EnemyGeneral
{

    [SerializeField] GameObject arrowPref;

    float atackAddForceSpeed = 400f;  // скорость пули

    
    void Start()
    {
        aiPath = GetComponent<AIPath>();
    }


    void Update()
    {
        reachedEndOfPath = aiPath.reachedDestination;

        if(isActive)
        {   
            aiPath.enabled = true;
            Vector2 direction = Direction(player.transform.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5f);
            float distance  = Vector2.Distance(player.transform.position, transform.position);
            if(hit.collider != null)
            {
                if(hit.transform.gameObject.name == "Player" && distance <= atackDistance || reachedEndOfPath)
                {
                   IsReadyToAtack = true;
                   isActive = false;
                   aiPath.enabled = false;
                } 
            }
        }

        if(IsReadyToAtack)
        {
            timer += Time.deltaTime;
            Vector2 direction = Direction(player.transform.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5f);
            float distance  = Vector2.Distance(player.transform.position, transform.position);
            if(hit.collider != null)
            {
                if(hit.transform.gameObject.name == "Player" && distance <= atackDistance || reachedEndOfPath)
                {
                    if(timer >= 1f)
                    {
                        Atack();
                        timer = 0f;
                    }
                }
                else
                {
                    isActive = true;
                    IsReadyToAtack = false;
                    aiPath.enabled = true;
                }
            }
        }

        Retreat(retreatSpeed, minDistance);
    }

    public override void Atack()
    {
        Vector3 direction = GetComponent<EnemyScript>().Direction(player.transform.position, transform.position);
        GameObject arrow = Instantiate(arrowPref, transform.position, Quaternion.identity);
        arrow.transform.up = direction;
        Rigidbody2D rbArrow = arrow.GetComponent<Rigidbody2D>();
        rbArrow.AddForce(direction * atackAddForceSpeed);
    }
}
