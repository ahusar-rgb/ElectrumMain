using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    protected bool reachedEndOfPath = false;
    private bool facingRight = true;
    protected float timer;
    private float flipDifference;
    private const float ACTIVATING_DISTANCE = 10f;

    protected AIPath aiPath; 

    private bool isSleeping = true;
    protected GameObject player;

    AIDestinationSetter destinationSetter;

    EnemyGeneral enemyGeneral;

    protected bool isActive;

    protected bool IsReadyToAtack; 


    void Awake()
    {
        enemyGeneral = GetComponent<EnemyGeneral>();
        aiPath = GetComponent<AIPath>();
        aiPath.enabled = false;
        destinationSetter = GetComponent<AIDestinationSetter>();

        player = GameObject.Find("Player");
        StartCoroutine("Activator");
        destinationSetter.target = player.transform;
    }


    void Update()
    {
        Flipping();


        if(enemyGeneral.Health <= 0)
        {
            Dead();
        }
    }


    IEnumerator Activator () // активирование врага, когда он заметил плеера
    {
		while (isSleeping) {	
			Vector2 direction = Direction(player.transform.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, ACTIVATING_DISTANCE);
            if(hit.collider != null)
            {
                if(hit.transform.gameObject.name == "Player")
                {
                    isSleeping = false;
                    isActive = true;
                }
            }

            yield return isActive;
		}
	}


    protected void Retreat(float retreatSpeed, float minDistance) // отступление
    {
        player = GameObject.Find("Player");
        if(Vector2.Distance(player.transform.position, transform.position) <= minDistance)
        {
            Vector2 direction = Direction(player.transform.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -direction, minDistance / 2);
            if(hit.collider == null)
            {
                transform.Translate(-direction * Time.deltaTime * retreatSpeed);
            }
        }
    }


    void Flipping()
    {
        if(isActive)
        {
            flipDifference = player.transform.position.x - transform.position.x;
            if (flipDifference < 0 && !facingRight) 
            {
			    Flip();
		    } 
            else if (flipDifference > 0 && facingRight) 
            {
			    Flip();
		    }
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Dead()
    {
        //Destroy(gameObject);
    }


    public virtual void Atack()
    {   
        print("atack");
    }   


    public Vector2 Direction(Vector2 vectorTarget, Vector2 vectorStart) 
    {
        Vector2 difference = vectorTarget - vectorStart;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        return direction;
    }
}
