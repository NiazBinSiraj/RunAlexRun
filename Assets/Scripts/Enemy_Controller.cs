using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private float velocityTrack;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(GameObject.Find("GameController").GetComponent<SceneController>().trackBackground == 1)
        {
            velocityTrack = GameObject.Find("Background-1").GetComponent<scrollingObject>().rateOfIncreasingVelocity;
        }

        else if (GameObject.Find("GameController").GetComponent<SceneController>().trackBackground == 2)
        {
            velocityTrack = GameObject.Find("Background-3").GetComponent<scrollingObject>().rateOfIncreasingVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity=new Vector2(-1.5f*velocityTrack,0.0f);
        if (gameObject.transform.position.x <= -8.0f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
		if(Other.gameObject.CompareTag("Bullet")){
			GameObject.Find("Player").GetComponent<playerController>().score++;
            GameObject.Find("Player").GetComponent<playerController>().setScoreText();
            Destroy(gameObject);
		}
        //if(Other.gameObject.CompareTag("Princess") || Other.gameObject.CompareTag("Player"))Destroy(Other.gameObject);
    }
}
