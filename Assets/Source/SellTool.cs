using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTool : Tool<ISellable> {

    public override bool Place() {
        if (item as Object) {
            Sell (item);
            Done ();
            return true;
        }

        Done ();
        return false;
    }

    public void Sell(ISellable sellable) {
        long value = sellable.GetSellValue ();
        PlayerInput.GiveCredits (value);
        sellable.Sell ();
    }
}
