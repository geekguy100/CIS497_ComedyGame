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
    public AudioClip notif;
    public AudioClip hey;
    public AudioClip another;

    private void OnEnable()
    {
        EventManager.OnGameWin += OnGameWin;
        EventManager.OnGameLost += OnGameLost;
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= OnGameWin;
        EventManager.OnGameLost -= OnGameLost;
    }

    private void OnGameWin()
    {
        source.PlayOneShot(win);
    }

    private void OnGameLost()
    {
        source.PlayOneShot(lose);
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public IEnumerator Play()
    {
        int r = Random.Range(15, 60);
        int i = Random.Range(0, 5);
        yield return new WaitForSeconds(r);
        switch (i)
        {
            case 0:
                source.PlayOneShot(huh);
                break;
            case 1:
                source.PlayOneShot(hello);
                break;
            case 2:
                source.PlayOneShot(notif, .1f);
                break;
            case 3:
                source.PlayOneShot(hey);
                break;
            case 4:
                source.PlayOneShot(another, .5f);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(r);
        StartCoroutine(Play());
    }
}
