using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseMenu : MonoBehaviour {

    public static PurchaseMenu menu;

    public IPlaceable placeableInHand;
    public GameObject hoveringObject;

    public List<DefensiveTurret> defensiveTurrets;

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
        if (menu.placeableInHand == null) {
            menu.placeableInHand = placeable;
            placeable.PickUp ();
        }
    }

    public static void Place() {
        if (menu.placeableInHand.Place ()) {
            if (menu.placeableInHand is DefensiveTurret) {
                menu.defensiveTurrets.Add (menu.placeableInHand as DefensiveTurret);
                LinkedFire.Link (menu.defensiveTurrets.ToArray ());
            }
            menu.placeableInHand = null;
        }
    }
}
