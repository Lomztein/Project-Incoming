using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedFire {

    // TODO, possibly change linked objects to IAimables instead.
    public List<ILinkable> linkables = new List<ILinkable> ();
    public float linkedFireRate = 0;

    private int linkIndex = 0;
    private float readyTime = 0;

    public LinkedFire() { }

    public LinkedFire(ILinkable startingLinkeable) {
        AddLinkable (startingLinkeable);
    }

    public static void Link(params ILinkable[] newLinkables) {
        LinkedFire newLink = new LinkedFire ();
        foreach (ILinkable linkable in newLinkables) {
            newLink.AddLinkable (linkable);
        }
    }

    public void AddLinkable(ILinkable newLinkable) {
        newLinkable.Link = this;
        linkables.Add (newLinkable);
        linkedFireRate = newLinkable.GetFirerate () / linkables.Count;
    }

    public bool Fire() {
        if (Time.time > readyTime) {
            if (linkables [ linkIndex ].Weapon.Fire ()) {
                linkables [ linkIndex ].OnFire ();

                readyTime = Time.time + linkedFireRate;

                linkIndex++;
                linkIndex = linkIndex % linkables.Count;

                return true;
            }
        }
        return false;
    }
}