using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float jumpSpeed;

    public GameObject rightBullet,EnemyObject,PrincessObject;
    public GameObject background;
    public GameObject background2;
    public GameObject bigEnemyObject;
    private Transform firepos;

    public float FireWaitReset,FireRate;
    private float FireWait;

    public Text scoreText;
    public Text highScore;
    public int best;
    public int score;
	public int Ammo;
	public float ReloadDelay;
    public int magazinSize;
    private AudioSource bigEnemyLaughSoundSource;

	private float StartTime,lastFireTime,PresentTime,lastReloadTime;


	private float lastPrincessTime,lastEnemyTime;
	public float PrincessDelay, EnemyDelay;


    private int frameCount = 0;
    public int BigEnemyFrameDelay;
    private bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        FireWait = FireWaitReset;
        score = 0;
        setScoreText();
		lastFireTime=Time.time;
		lastReloadTime=Time.time;

        best = PlayerPrefs.GetInt("Score",0);
        bigEnemyLaughSoundSource = GetComponent<AudioSource>();
    }

    public void showHighScore(int best)
    {
        PlayerPrefs.SetInt("Score", best);
        highScore.text = "Best : " + best.ToString();
    }

    public void resetHighScore()
    {
        best = 0;
        showHighScore(best);
    }

    public void resetScore()
    {
        score = 0;
        setScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        transform.Rotate(0, 0, 0);

        isPaused = GameObject.Find("GameController").GetComponent<SceneController>().isPaused;

        if (score >= best) best = score;

        FireWait -= FireRate;
		PresentTime=Time.time;

		if(PresentTime-lastReloadTime>=ReloadDelay && Ammo==0){
            Ammo += magazinSize;
			lastReloadTime=PresentTime;
		}

        if(GameObject.Find("GameController").GetComponent<SceneController>().trackBackground == 1)
        {
            if (background.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 2.0f) EnemyDelay = 4;
            else if (background.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 3.0f) EnemyDelay = 3;
            else if (background.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 4.0f) EnemyDelay = 2;
            else if (background.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 5.5f) EnemyDelay = 1;
        }

        else if(GameObject.Find("GameController").GetComponent<SceneController>().trackBackground == 2)
        {
            if (background2.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 2.0f) EnemyDelay = 4;
            else if (background2.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 3.0f) EnemyDelay = 3;
            else if (background2.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 4.0f) EnemyDelay = 2;
            else if (background2.GetComponent<scrollingObject>().rateOfIncreasingVelocity == 5.5f) EnemyDelay = 1;
        }

        

        if (PresentTime-lastPrincessTime>=PrincessDelay && isPaused == false)
        {
			lastPrincessTime=PresentTime;
			float spawnYPosition = Random.Range(2.86f, -2.1f);
			float spawnXPosition = Random.Range(7.67f,8.93f);
			Vector2 targetLocation=new Vector2(spawnXPosition,spawnYPosition);
			Instantiate(PrincessObject,targetLocation, Quaternion.identity);
		}

		if(PresentTime - lastEnemyTime >= EnemyDelay && isPaused == false)
        {
			lastEnemyTime = PresentTime;
			float spawnYPosition = Random.Range(2.8f, -1.99f);
			float spawnXPosition = Random.Range(7.9f,9.0f);
			Vector2 targetLocation=new Vector2(spawnXPosition,spawnYPosition);
			Instantiate(EnemyObject,targetLocation, Quaternion.identity);
        }

        if(frameCount == BigEnemyFrameDelay && isPaused == false)
        {
            bigEnemyLaughSoundSource.Play();
            frameCount = 0;
            float spawnYPosition = Random.Range(2.8f, -1.99f);
            float spawnXPosition = Random.Range(7.9f, 9.0f);
            Vector2 targetLocation = new Vector2(spawnXPosition, spawnYPosition);
            Instantiate(bigEnemyObject, targetLocation, Quaternion.identity);
        }
    }

    public void doJump()
    {
        if (isPaused == false)
        {
            rb2d.velocity = new Vector2(0.0f, 0.0f);
            Vector2 moveVertical = new Vector2(0.0f, 1.0f);
            rb2d.AddForce(moveVertical * jumpSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Princess"))
        {
            Destroy(other.gameObject);
			score+=2;
            setScoreText();
        }

		if(other.gameObject.CompareTag("Enemy"))
		{
            GameObject.Find("GameController").GetComponent<SceneController>().gameOver();
            gameObject.SetActive(false);
		}

        if (other.gameObject.CompareTag("Big Enemy"))
        {
            GameObject.Find("GameController").GetComponent<SceneController>().gameOver();
            gameObject.SetActive(false);
        }
    }

    public void Fire()
    {
        
        if (FireWait <= 0.0f && Ammo>0 && isPaused == false)
        {
			Ammo--;
            FireWait = FireWaitReset;
            firepos = transform.Find("Fire_Position");
            Instantiate(rightBullet, firepos.position, Quaternion.identity);
            GameObject.Find("FiringSound").GetComponent<firingSoundController>().playFiringSound();
        }
    }

    public void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void resetAmmo()
    {
        Ammo = 50;
    }
}
