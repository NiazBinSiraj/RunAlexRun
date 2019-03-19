using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firingSoundController : MonoBehaviour
{
    private AudioSource firingSound;
    public AudioClip firing;
    // Start is called before the first frame update
    void Start()
    {
        firingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playFiringSound()
    {
        firingSound.PlayOneShot(firing, 0.3f);
    }

}
