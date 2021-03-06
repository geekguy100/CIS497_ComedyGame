/*****************************************************************************
// File Name :         DashSlider.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class DashSlider : MonoBehaviour
{
    [Tooltip("The character to observe the dash changes of.")]
    [SerializeField] private CharacterMovement character;

    private Slider dashSlider;

    private void Awake()
    {
        dashSlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (character == null)
            Destroy(gameObject);
        else
            character.OnDashUpdate += UpdateSlider;
    }

    private void UpdateSlider(float dashPercent)
    {
        dashSlider.value = dashPercent;
    }
}
