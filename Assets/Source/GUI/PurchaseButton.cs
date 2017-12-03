using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour {

    public long cost;
    public GameObject obj;

    public virtual void Purchase() {
        if (PlayerInput.TryUseCredits (cost)) {
            GameObject newObject = Instantiate (obj);
            IPlaceable placeable = newObject.GetComponent<IPlaceable> ();
            PurchaseMenu.PickUp (placeable);
        }
    }

}
