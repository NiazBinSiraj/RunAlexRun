using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float rateOfIncreasingVelocity;
    public float scrollingDelay;
    private float lastVelocityChange;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(-2.0f, 0);
    }

    public void setScreenVelocity(float value)
    {
        rb2d.velocity = new Vector2(value, 0);
    }

    public void resetVelocity()
    {
        rb2d.velocity = new Vector2(-2.0f, 0);
        rateOfIncreasingVelocity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        if(currentTime - lastVelocityChange >= scrollingDelay && rateOfIncreasingVelocity <= 5.0f)
        {
            lastVelocityChange = currentTime;
            rateOfIncreasingVelocity += 0.5f;
            rb2d.velocity = new Vector2(-1.5f * rateOfIncreasingVelocity, 0);
        }
    }
}
