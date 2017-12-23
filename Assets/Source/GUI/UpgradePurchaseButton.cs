using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePurchaseButton : PurchaseButton {

    public bool fullyUpgraded;

    public override void UpdateInteractable() {
        thisButton.interactable = PlayerInput.HasCredits (cost) && !fullyUpgraded;
    }

}
