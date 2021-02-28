using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] music;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        index = Random.Range(0, music.Length -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying || Input.GetKeyDown(KeyCode.N))
        {
            NextSong();
        }
    }

    public void NextSong()
    {
        if (index < music.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        source.clip = music[index];
        source.Play();
    }
}
