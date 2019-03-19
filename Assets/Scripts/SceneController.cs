using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public GameObject gameObject4;
    public GameObject gameObject5;
    public GameObject gameObject6;
    public GameObject gameOverText;
    public GameObject playAgainButton;
    public GameObject exitButton;
    public GameObject highScoreText;
    public GameObject restHighScoreButton;
    public GameObject exitButtonWhileGameIsOn;
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject gameSound;
    public GameObject gameSound2;
    public GameObject background3;
    public GameObject background4;

    public GameObject changeBackgroundText;
    public GameObject background1Button;
    public GameObject background2Button;


    public bool isPaused = false;

    private AudioSource gameOverSoundSource;
    private float currentTimeScale;

    private float deltaTime = 0.0f;
    public Text fpsDisplay;
    private int frame = 0;

    public int trackBackground;

    private void Start()
    {
        gameObject3.SetActive(false);
        gameObject4.SetActive(false);
        gameObject5.SetActive(false);
        gameObject6.SetActive(false);
        gameOverText.SetActive(false);
        playAgainButton.SetActive(false);
        exitButton.SetActive(false);
        highScoreText.SetActive(false);
        restHighScoreButton.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(false);
        background3.SetActive(false);
        background4.SetActive(false);
        background1Button.SetActive(false);

        gameOverSoundSource = GetComponent<AudioSource>();
        
        trackBackground = 1;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        frame++;
        if(frame == 1 || frame%100 == 0) setFrameRate();
    }

    public void startGame()
    {
        if(trackBackground == 1)
        {
            gameObject1.GetComponent<scrollingObject>().enabled = true;
            gameObject1.GetComponent<repeatingBackground>().enabled = true;
            gameObject2.GetComponent<scrollingObject>().enabled = true;
            gameObject2.GetComponent<repeatingBackground>().enabled = true;
        }

        else if(trackBackground == 2)
        {
            background3.GetComponent<scrollingObject>().enabled = true;
            background3.GetComponent<repeatingBackground>().enabled = true;
            background4.GetComponent<scrollingObject>().enabled = true;
            background4.GetComponent<repeatingBackground>().enabled = true;
        }
        

        pauseButton.SetActive(true);

        GameObject.Find("StartButton").SetActive(false);

        gameObject3.SetActive(true);
        gameObject4.SetActive(true);
        gameObject5.SetActive(true);
        gameObject6.SetActive(true);
        changeBackgroundText.SetActive(false);

        if(trackBackground == 1)
        {
            background2Button.SetActive(false);
        }

        else if (trackBackground == 2)
        {
            background1Button.SetActive(false);
        }

    }


    public void gameOver()
    {
        int score = gameObject3.GetComponent<playerController>().score;
        int best = gameObject3.GetComponent<playerController>().best;

        if (score >= best) best = score;

        gameOverSoundSource.Play();
        gameOverText.SetActive(true);
        highScoreText.SetActive(true);
        playAgainButton.SetActive(true);
        exitButton.SetActive(true);
        restHighScoreButton.SetActive(true);
        exitButtonWhileGameIsOn.SetActive(false);


        gameObject3.GetComponent<playerController>().showHighScore(best);

        if(trackBackground == 1)
        {
            gameObject1.GetComponent<scrollingObject>().setScreenVelocity(0.0f);
            gameObject2.GetComponent<scrollingObject>().setScreenVelocity(0.0f);
            gameObject1.GetComponent<scrollingObject>().enabled = false;
            gameObject2.GetComponent<scrollingObject>().enabled = false;
            gameObject1.GetComponent<repeatingBackground>().enabled = false;
            gameObject2.GetComponent<repeatingBackground>().enabled = false;
        }
        
        else if(trackBackground == 2)
        {
            background3.GetComponent<scrollingObject>().setScreenVelocity(0.0f);
            background4.GetComponent<scrollingObject>().setScreenVelocity(0.0f);
            background3.GetComponent<scrollingObject>().enabled = false;
            background4.GetComponent<scrollingObject>().enabled = false;
            background3.GetComponent<repeatingBackground>().enabled = false;
            background4.GetComponent<repeatingBackground>().enabled = false;
        }

        GameObject[] listOfEnemy = new GameObject[10];
        GameObject[] listOfPrincess = new GameObject[10];
        GameObject[] listOfBigEnemy = new GameObject[10];

        listOfEnemy = (GameObject.FindGameObjectsWithTag("Enemy"));
        listOfBigEnemy = (GameObject.FindGameObjectsWithTag("Big Enemy"));
        listOfPrincess = (GameObject.FindGameObjectsWithTag("Princess"));

        for(int i=0; i<listOfEnemy.Length; i++)
        {
            Destroy(listOfEnemy[i]);
        }

        for (int i = 0; i < listOfBigEnemy.Length; i++)
        {
            Destroy(listOfBigEnemy[i]);
        }

        for (int i = 0; i < listOfPrincess.Length; i++)
        {
            Destroy(listOfPrincess[i]);
        }
    }

    public void doExit()
    {
        Application.Quit();
    }

    public void playAgain()
    {
        if(trackBackground == 1)
        {
            gameObject1.GetComponent<scrollingObject>().enabled = true;
            gameObject2.GetComponent<scrollingObject>().enabled = true;
            gameObject1.GetComponent<repeatingBackground>().enabled = true;
            gameObject2.GetComponent<repeatingBackground>().enabled = true;
            gameObject1.GetComponent<scrollingObject>().resetVelocity();
            gameObject2.GetComponent<scrollingObject>().resetVelocity();
        }

        else if(trackBackground == 2)
        {
            background3.GetComponent<scrollingObject>().enabled = true;
            background4.GetComponent<scrollingObject>().enabled = true;
            background3.GetComponent<repeatingBackground>().enabled = true;
            background4.GetComponent<repeatingBackground>().enabled = true;
            background3.GetComponent<scrollingObject>().resetVelocity();
            background4.GetComponent<scrollingObject>().resetVelocity();
        }


        exitButtonWhileGameIsOn.SetActive(true);

        gameObject3.GetComponent<playerController>().resetScore();
        gameObject3.GetComponent<playerController>().resetAmmo();
        gameObject3.GetComponent<playerController>().EnemyDelay = 5;

        gameOverText.SetActive(false);
        playAgainButton.SetActive(false);
        exitButton.SetActive(false);
        highScoreText.SetActive(false);
        restHighScoreButton.SetActive(false);

        gameObject3.SetActive(true);
        gameObject4.SetActive(true);
        gameObject5.SetActive(true);
        gameObject6.SetActive(true);
    }

    public void setFrameRate()
    {
        float fps = 1.0f / deltaTime;
        float msec = deltaTime * 1000.0f;

        fpsDisplay.text = Mathf.RoundToInt(fps).ToString() + " FPS (" + Mathf.RoundToInt(msec).ToString() + "ms)";
    }

    public void pauseGame()
    {
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        isPaused = true;

        if(trackBackground == 1)
        {
            gameSound.GetComponent<AudioSource>().Pause();
        }

        else if (trackBackground == 2)
        {
            gameSound2.GetComponent<AudioSource>().Pause();
        }

    }

    public void playGame()
    {
        Time.timeScale = currentTimeScale;
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        isPaused = false;

        if (trackBackground == 1)
        {
            gameSound.GetComponent<AudioSource>().Play();
        }

        else if (trackBackground == 2)
        {
            gameSound2.GetComponent<AudioSource>().Play();
        }
    }

    public void loadBackground34()
    {
        background3.SetActive(true);
        background4.SetActive(true);
        gameObject1.SetActive(false);
        gameObject2.SetActive(false);
        trackBackground = 2;

        background1Button.SetActive(true);
        background2Button.SetActive(false);

        gameSound.GetComponent<AudioSource>().Stop();
        gameSound2.GetComponent<AudioSource>().Play();
    }

    public void loadBackground12()
    {

        gameObject1.SetActive(true);
        gameObject2.SetActive(true);
        background3.SetActive(false);
        background4.SetActive(false);
        trackBackground = 1;

        background2Button.SetActive(true);
        background1Button.SetActive(false);

        gameSound2.GetComponent<AudioSource>().Stop();
        gameSound.GetComponent<AudioSource>().Play();
    }
}