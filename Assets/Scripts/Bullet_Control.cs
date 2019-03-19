using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Control : MonoBehaviour
{

    public Vector2 speed;
    Rigidbody2D rb2d;
    public float Damage, delay;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(speed);
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.AddForce(speed * 100);
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.CompareTag("Big Enemy") || Other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
