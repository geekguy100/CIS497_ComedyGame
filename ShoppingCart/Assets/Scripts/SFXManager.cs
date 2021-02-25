using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] sfx;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Play());
    }

    public IEnumerator Play()
    {
        int r = Random.Range(1, 5);
        int i = Random.Range(0, sfx.Length - 1);
        yield return new WaitForSeconds(r);
        //source.clip = sfx[i];
        source.PlayOneShot(source.clip = sfx[i]);
    }
}
