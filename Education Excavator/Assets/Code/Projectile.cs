using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectile_speed= 5;

    private Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        shooting();
    }

    public void shooting()
    {
        rigidbody2.velocity = new Vector2(0, 1 * projectile_speed);//moves the laser upwards
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }//handles collisions between projectile and other game objects
}