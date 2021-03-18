using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCart : MonoBehaviour
{
    private bool inCheckout;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && inCheckout)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckoutLocation"))
        {
            inCheckout = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckoutLocation"))
        {
            inCheckout = false;
        }
    }

    
}
