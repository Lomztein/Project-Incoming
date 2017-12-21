using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTurretPurchaseButton : PurchaseButton {

    public override void UpdateInteractable() {
        thisButton.interactable = PlayerInput.HasCredits (cost) && !DefensiveTurret.AtCapacity ();
    }

    private void Update() {
        UpdateInteractable ();
    }

}
