using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    //creating an array of sound effects
    public AudioSource[] soundEffects;
    public AudioSource background, end;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSounds(int soundPlayed)
    {

        soundEffects[soundPlayed].Stop();

        soundEffects[soundPlayed].Play();
        
    }
}
