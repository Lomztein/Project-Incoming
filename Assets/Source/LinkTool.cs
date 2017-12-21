using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTool : Tool<ILinkable> {

    public List<ILinkable> selectedLinkables = new List<ILinkable> ();
    public float linkedFirerate;

    public override bool Place() {
        if (item as Object && !selectedLinkables.Contains (item)) {
            if (selectedLinkables.Count == 0) {
                selectedLinkables.Add (item);
                linkedFirerate = item.Weapon.GetFirerate ();

                return false;

            } else if (Mathf.Approximately (linkedFirerate, item.Weapon.GetFirerate ())) {
                selectedLinkables.Add (item);
                LinkedFire.Link (selectedLinkables.ToArray ());

                return false;
            }
        }
        Done ();
        return true;
    }
}
