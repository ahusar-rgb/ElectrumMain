using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    private float speed = 4f;
    public GameObject explosion;
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    public override void DestroySelf()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
