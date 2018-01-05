using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmplacementTurretPurchaseButton : EmplacementMenuPurchaseButtonBase {

    public EmplacementTurret turretPrefab;

    public override void Purchase() {
        if (IsPurchased ()) {
            parentGUI.OnSelectTurret (this);
            return;
        }

        if (PlayerInput.TryUseCredits (cost)) {
            parentGUI.OnSelectTurret (this);
        }
    }

    public override void UpdateInteractable() {
        thisButton.interactable = (IsPurchased () || PlayerInput.HasCredits (cost)) && !IsCurrentItem ();
    }

    public override bool IsPurchased() {
        return parentGUI.IsTurretPurchased (turretPrefab.Name);
    }

    public override bool IsCurrentItem() {
        return parentGUI.currentTurretButton == this;
    }
}
