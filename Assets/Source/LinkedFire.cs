﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedFire {

    public List<ILinkable> linkables = new List<ILinkable> ();
    public float linkedFireRate = 0;

    private int linkIndex = 0;
    private float readyTime = 0;

    public LinkedFire() { }

    public LinkedFire(ILinkable startingLinkeable) {
        AddLinkable (startingLinkeable);
    }

    public static void Link(params ILinkable[] newLinkables) {
        foreach (ILinkable linkable in newLinkables) {
            if (linkable.Link != null)
                linkable.Link.Destroy ();
        }

        LinkedFire newLink = new LinkedFire ();
        foreach (ILinkable linkable in newLinkables) {
            newLink.AddLinkable (linkable);
        }
        newLink.linkIndex = 0;
    }

    public void AddLinkable(ILinkable newLinkable) {
        if (newLinkable.CanLink ()) {
            newLinkable.Link = this;
            linkables.Add (newLinkable);
            linkedFireRate = newLinkable.GetFirerate () / linkables.Count;
            newLinkable.Weapon.Reload ();
        }
    }

    public void RemoveLinkable (ILinkable linkable) {
        linkables.Remove (linkable);
        linkedFireRate = linkable.GetFirerate () / linkables.Count;
    }

    public static void ClearLinkable (ILinkable linkable) {
        if (linkable.Link != null)
            linkable.Link.RemoveLinkable (linkable);
        linkable.Link = null;
    }

    public void Destroy () {
        List<ILinkable> toRemove = new List<ILinkable> (linkables);
        foreach (ILinkable linkable in toRemove) {
            ClearLinkable (linkable);
        }
    }

    public bool Fire() {
        if (Time.time >= readyTime) {
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