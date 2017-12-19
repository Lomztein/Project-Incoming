using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmplacementTurretPurchaseButton : EmplacementMenuPurchaseButtonBase {

    public override void Purchase() {
        if (purchased) {
            parentGUI.OnSelectTurret (this);
            return;
        }

        if (PlayerInput.TryUseCredits (cost)) {
            purchased = true;
            parentGUI.OnSelectTurret (this);
        }
    }

    public override void UpdateInteractable() {
        thisButton.interactable = (purchased || PlayerInput.HasCredits (cost)) && !IsCurrentItem ();
    }

    public override bool IsCurrentItem() {
        return parentGUI.currentTurretButton == this;
    }
}
