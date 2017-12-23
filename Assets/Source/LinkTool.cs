using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTool : Tool<ILinkable> {

    public LineRenderer linkLine;
    public List<Transform> linkTransforms;

    public List<ILinkable> selectedLinkables = new List<ILinkable> ();
    public float linkedFirerate;

    public override bool Place() {
        if (item as Object && !selectedLinkables.Contains (item)) {
            if (selectedLinkables.Count == 0) {
                selectedLinkables.Add (item);
                linkedFirerate = item.Weapon.GetFirerate ();

                linkTransforms = new List<Transform> ();
                linkTransforms.Add (lastTransform);

                return false;

            } else if (Mathf.Approximately (linkedFirerate, item.Weapon.GetFirerate ())) {
                selectedLinkables.Add (item);
                LinkedFire.Link (selectedLinkables.ToArray ());

                linkTransforms.Add (lastTransform);
                UpdateLinkLine ();

                return false;
            }
        }
        Done ();
        return true;
    }

    private void UpdateLinkLine () {
        linkLine.positionCount = linkTransforms.Count + 1;
        for (int i = 0; i < linkTransforms.Count; i++) {
            linkLine.SetPosition (i, linkTransforms [ i ].position + Vector3.up * 2f);
        }
        // Loop that shiznat.
        linkLine.SetPosition (linkTransforms.Count, linkTransforms [ 0 ].position + Vector3.up * 2f);
    }
}
