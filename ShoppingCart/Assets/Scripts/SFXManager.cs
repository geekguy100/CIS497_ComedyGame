using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip error;
    public AudioClip drop;
    public AudioClip pickup;
    public AudioClip ram;
    public AudioClip hello;
    public AudioClip hit;
    public AudioClip huh;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Play(error));
    }

    public IEnumerator Play(AudioClip c)
    {
        int r = Random.Range(10, 30);
        yield return new WaitForSeconds(r);
        source.PlayOneShot(c);
        yield return new WaitForSeconds(r);
        StartCoroutine(Play(c));
    }
}
