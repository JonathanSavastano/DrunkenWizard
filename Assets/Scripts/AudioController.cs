using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.Play();
    }
}
