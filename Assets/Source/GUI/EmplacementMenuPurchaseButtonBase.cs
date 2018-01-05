using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EmplacementMenuPurchaseButtonBase : PurchaseButton {

    public EmplacementMenuGUI parentGUI;

    public abstract bool IsCurrentItem();
    public abstract bool IsPurchased();

}
