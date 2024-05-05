using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    VideoPlayer vidplayer;   
    // Start is called before the first frame update
    void Start()
    {
        vidplayer = GetComponent<VideoPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Play();
        }
    }

    void Play()
    {
        vidplayer.Play();
        vidplayer.isLooping = true;
    }
}
