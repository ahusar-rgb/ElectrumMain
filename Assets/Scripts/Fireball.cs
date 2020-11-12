using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    private float speed = 4f;
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}
