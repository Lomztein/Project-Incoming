using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmplacementPurchaseButton : PurchaseButton {

    public bool purchased;

    public EmplacementTurretSelectionGUI parentGUI;

    public override void Purchase() {
        if (purchased) {
            parentGUI.OnPurchase (this);
            return;
        }

        if (PlayerInput.TryUseCredits (cost)) {
            purchased = true;
            parentGUI.OnPurchase (this);
        }
    }

    public override void UpdateInteractable() {
        thisButton.interactable = (purchased || PlayerInput.HasCredits (cost)) && !IsCurrentTurret ();
    }

    private bool IsCurrentTurret() {
        return parentGUI.currentTurretButton == this;
    }
}
