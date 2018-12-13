using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_musicManager : MonoBehaviour
{

    public AudioSource audioPlayer;
    public float loopFinish, loopStart;
    public float audioStart = 0;
    // Use this for initialization
    void Start()
    {
        audioPlayer.time = audioStart;
        audioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioPlayer.time > loopFinish)
        {
            Debug.Log(audioPlayer.time);
            audioPlayer.time = loopStart;
        }
    }
}
