using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmplacementProjectileSelectButton : EmplacementMenuPurchaseButtonBase {

    public override void Purchase() {
        parentGUI.OnSelectProjectile (this);
    }

    public override void UpdateInteractable() {
        thisButton.interactable = !IsCurrentItem ();
    }

    public override bool IsCurrentItem() {
        return parentGUI.currentProjectileButton == this;
    }

    public override bool IsPurchased() {
        return parentGUI.IsProjectilePurchased (obj.name);
    }
}
