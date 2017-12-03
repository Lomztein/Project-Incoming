using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseMenu : MonoBehaviour {

    public static PurchaseMenu menu;

    public IPlaceable placeableInHand;
    public GameObject hoveringObject;

    // Use this for initialization
    private void Awake() {
        menu = this;
    }

    // Update is called once per frame
    void Update () {
        if (placeableInHand != null) {
            placeableInHand.ToPosition (PlayerInput.mouseWorldPosition, Quaternion.identity);

            if (Input.GetButtonDown ("Fire1"))
                Place ();
        }
	}

    public static void PickUp(IPlaceable placeable) {
        if (menu.placeableInHand == null)
            menu.placeableInHand = placeable;
    }

    public static void Place() {
        menu.placeableInHand.Place ();
        menu.placeableInHand = null;
    }
}
