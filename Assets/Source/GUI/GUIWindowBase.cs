using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GUIWindowBase : MonoBehaviour {

	public static GUIWindowBase Create (GameObject origin, Transform parent) {
        GUIWindowBase window = Instantiate (origin).GetComponent<GUIWindowBase> ();
        window.transform.SetParent (parent, false);
        GUIManager.AddWindow (window);
        return window;
    }

    public abstract void Open();

    public abstract void Close();

    protected void Remove () {
        GUIManager.RemoveWindow (this);
    }

}
