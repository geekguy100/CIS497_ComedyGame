using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public bool isOnShelf;
    public bool isInPlayerCart;
    public bool isInOtherCart;
    public float distAway;
    public float reach;
    
    public PickupWindow pw;

    // Start is called before the first frame update
    void Start()
    {
        isOnShelf = true;
        isInPlayerCart = false;
        isInOtherCart = false;
        reach = 2;
        pw = GameObject.FindGameObjectWithTag("Pickup").GetComponent<PickupWindow>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    distAway = Vector3.Distance(pw.player.transform.position, transform.position);
    //    if (!isInPlayerCart && !isInOtherCart && distAway <= reach && !pw.nearbyItems.Contains(this))
    //    {
    //        pw.nearbyItems.Add(this);
    //        pw.UpdateDisplay();
    //    }

    //    if (distAway > reach && pw.nearbyItems.Contains(this))
    //    {
    //        pw.nearbyItems.Remove(this);
    //        pw.UpdateDisplay();
    //    }
    //}


}
