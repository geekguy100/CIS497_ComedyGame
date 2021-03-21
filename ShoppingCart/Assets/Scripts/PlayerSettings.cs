/*****************************************************************************
// File Name :         PlayerSettings.cs
// Author :            Kyle Grenier
// Creation Date :     03/20/2021
//
// Brief Description : The player's game settings.
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings instance;



    [SerializeField] private Slider sensitivitySlider;

    [Tooltip("The max sensitivity available to the player.")]
    [SerializeField] private float maxSensitivity = 100f;

    [Tooltip("The default sensitivity setting.")]
    [SerializeField] private float defaultSensitivty = 50f;

    private float currentSensitivity;
    /// <summary>
    /// The current mouse sensitivty.
    /// </summary>
    public float Sensitivity { get { return currentSensitivity; } }

    void Awake()
    {
        #region --- Singleton ---
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        #endregion
    }

    private void Start()
    {
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.value = defaultSensitivty;
        currentSensitivity = defaultSensitivty;
    }

    /// <summary>
    /// Invoked when the sensitivty slider changes;
    /// Updates the current sensitivity to the changed value.
    /// </summary>
    /// <param name="value">The new sensitivity.</param>
    public void OnSensitivitySliderChanged(float value)
    {
        currentSensitivity = value;
    }
}