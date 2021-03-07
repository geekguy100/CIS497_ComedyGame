using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region ---Singleton---
    public static SFXManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

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

    private void OnEnable()
    {
        EventManager.OnGameWin += () => { source.PlayOneShot(win); };
        EventManager.OnGameLost += () => { source.PlayOneShot(lose); };
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public IEnumerator Play(AudioClip c)
    {
        int r = Random.Range(15, 60);
        yield return new WaitForSeconds(r);
        source.PlayOneShot(c);
        yield return new WaitForSeconds(r);
        StartCoroutine(Play(c));
    }
}
