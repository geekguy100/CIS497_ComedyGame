/*****************************************************************************
// File Name :         AdditiveSceneLoader.cs
// Author :            Kyle Grenier
// Creation Date :     02/28/2021
//
// Brief Description : Loads scenes additively on top of each other synchronously.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoader : MonoBehaviour
{
    [SerializeField] private string[] scenesToLoad;

    private void Start()
    {
        foreach(string scene in scenesToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
