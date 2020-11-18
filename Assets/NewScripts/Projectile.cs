using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : Spell
{
    private float speed = 1.5f;
    Vector3 enemyVector;

    void Start()
    {
        enemyVector = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>().position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, enemyVector, Time.deltaTime * speed);
    }

    public override void DestroySelf()
    {
        Destroy(gameObject);
    }
}
